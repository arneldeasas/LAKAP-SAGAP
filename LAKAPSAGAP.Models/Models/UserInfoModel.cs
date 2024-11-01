
namespace LAKAPSAGAP.Models.Models
{
    [Table("UserInfo")]
    public class UserInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UserAuthId { get; set; }
        public string UserRole { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Barangay { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public ICollection<Attachment> AttachmentList { get; set; }

        public class Attachment
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }
            public int UserId { get; set; }
            public string Url { get; set; }
            [ForeignKey(nameof(UserId))]
            public UserInfo User { get; set; }
        }

    }
    
}
