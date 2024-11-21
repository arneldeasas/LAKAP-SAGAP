using System.IO;
using LAKAPSAGAP.Models.Models;
using LAKAPSAGAP.Services.Core.Helpers;
using LAKAPSAGAP.Services.Repositories;
using Microsoft.AspNetCore.Components.Forms;

namespace LAKAPSAGAP.Services.Core
{
    public class UserAttachmentRepository : IUserAttachmentRepository
    {
        public static readonly string _uploadPath = Path.Combine("wwwroot", "attachments");
        public static readonly int maxFileSize = 1024 * 1024 * 5;
        readonly MyDbContext _context;
		public UserAttachmentRepository(MyDbContext context)
        {
            _context = context;
            if (!Directory.Exists(_uploadPath))
            {
                Directory.CreateDirectory(_uploadPath);
            }

		}
        public async Task<List<string>> UploadAttachments(List<IBrowserFile> fileList, string userId)
        {  //Makes metadata for files
            var largeFile = fileList.Find(x => x.Size > maxFileSize)  ;
            if (largeFile is not null)
            {
                throw new Exception($"File:{largeFile.Name}  size too large. Try uploading files lower than 5 mb.");
            }
            var fileMetadataList = fileList.Select(x => new FileMetadata(x, userId)).ToArray();

            var attachmentUrlList = await Task.WhenAll(fileMetadataList.Select(fileMetadata => UploadFileAsync(fileMetadata)));

            int existingRecordsCount = await _context.GetCount<Attachment>();
            List<Attachment> attachmentList = attachmentUrlList.Select((attachmentUrl) =>
            {
                existingRecordsCount++;
                return new Attachment
                {
                    Id = IdGenerator.GenerateId(IdGenerator.PFX_ATTACHMENT, existingRecordsCount),
                    UserId = userId,
                    //AddedById = userId,
                    Url = attachmentUrl
                };
            }).ToList();

            await _context.CreateMany(attachmentList);

         
            var uploadTasks = fileMetadataList.Select(file => UploadFileAsync(file)
            ).ToList();

            await Task.WhenAll(uploadTasks);

            return fileMetadataList.Select(x => x.FilePath).ToList();
        }

        public async Task<string> UploadFileAsync(FileMetadata file)
        {
            try
            {
                using (var fileStream = new FileStream(file.FilePath, FileMode.Create))
                {

                    await file.File.OpenReadStream(maxFileSize).CopyToAsync(fileStream);
                }
                return file.FilePath;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to upload {file.File.Name}: {e.Message}");
                throw;
            }
        }
        public async Task<Attachment> DeleteAttachment(Attachment attachment)
        {
            try
            {
                File.Delete(attachment.Url);
                attachment.IsDeleted = true;
                await _context.UpdateItem(attachment);
                return attachment;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public async Task<Attachment> DeleteAttachmentSoft(string Id)
        {
            try
            {
                var attachment = await _context.GetById<Attachment>(Id);
                if(attachment == null)
				{
					throw new Exception("Attachment not found");
				}
				attachment.IsDeleted = true;
				attachment = await _context.UpdateItem(attachment);
				return attachment;
			}
            catch (Exception)
            {

                throw;
            }
        }
		public async Task<List<(string, string)>> GetUserAttachments(string userId)
		{
			try
			{
				var response = await _context.Attachments.WhereIsNotArchivedAndDeleted()
					.Where(x => x.UserId == userId )
					.Select(x => new { x.Id, x.Url })
					.ToListAsync();

                return response.Select(x => (x.Id, x.Url)).ToList();
			}
			catch (Exception)
			{
				throw;
			}
		}

	}
}
public class FileMetadata
{
    public IBrowserFile File { get; set; }
    public string UserId { get; set; }
    private readonly string _filePath;
    public string FilePath => _filePath;

    public FileMetadata(IBrowserFile file, string userId)
    {
        File = file;
        UserId = userId;
        _filePath = Path.Combine(UserAttachmentRepository._uploadPath, $"{UserId}_{file.Name}");
    }
}