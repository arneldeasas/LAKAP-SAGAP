
namespace LAKAPSAGAP.Models.Models
{
    [Table("UserInfo")]
    public class UserInfo:CommonModel
    {
       
        public string UserAuthId { get; set; }
        public string UserRole { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Barangay { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public List<Attachment> Attachments { get; set; } = new List<Attachment>();

    }
    public class Attachment : CommonModel
    {
       
        public string Url { get; set; }
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public UserInfo User { get; set; }
   
    }
}
