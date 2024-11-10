using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;

namespace LAKAPSAGAP.Services.Repositories
{
    
    public interface IUserAttachmentRepository
    {
        
        public Task<List<string>> UploadAttachments(List<IBrowserFile> fileList, string userId);
        public Task<string> UploadFileAsync(FileMetadata fileMetadata);
        public Task<Attachment> DeleteAttachment(Attachment attachment);
        Task<List<(string, string)>> GetUserAttachments(string userId);


	}
}

