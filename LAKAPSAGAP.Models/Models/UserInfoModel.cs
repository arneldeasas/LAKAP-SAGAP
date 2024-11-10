
using Microsoft.AspNetCore.Identity;

namespace LAKAPSAGAP.Models.Models
{
    [Table("UserInfo")]
    public class UserInfo:CommonModel
    {
        public string UserAuthId { get; set; }
        public string RoleId { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public string Barangay { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public List<Attachment> Attachments { get; set; } = new List<Attachment>();
        [ForeignKey(nameof(RoleId))]
        public IdentityRole Role { get; set; }
        [ForeignKey(nameof(UserAuthId))]
		public UserAuth UserAuth { get; set; }

	}
    public class Attachment : CommonModel
    {
       
        public string Url { get; set; }
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public UserInfo User { get; set; }
   
    }
}
