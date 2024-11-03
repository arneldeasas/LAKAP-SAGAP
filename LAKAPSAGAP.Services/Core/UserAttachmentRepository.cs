using System.IO;
using LAKAPSAGAP.Models.Models;
using LAKAPSAGAP.Services.Repositories;
using Microsoft.AspNetCore.Components.Forms;

namespace LAKAPSAGAP.Services.Core
{
    public class UserAttachmentRepository : CommonRepository<Attachment>, IUserAttachmentRepository
    {
        public static readonly string _uploadPath = Path.Combine("wwwroot", "attachments");
        public static readonly int maxFileSize = 1024 * 1024 * 5;

        public UserAttachmentRepository(MyDbContext context):base(context)
        {
            
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

            List<Attachment> attachmentList = attachmentUrlList.Select(attachmentUrl => new Attachment
            {
                AddedById = userId,
                Url = attachmentUrl
            }).ToList();

            await CreateMany(attachmentList);

         
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
                await Update(attachment);
                return attachment;
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