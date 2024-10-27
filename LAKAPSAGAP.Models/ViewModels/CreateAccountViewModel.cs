
namespace LAKAPSAGAP.Models.ViewModels
{
    public class CreateAccountViewModel
    {

        public string UserRole { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Barangay { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public ICollection<IBrowserFile> fileList { get; set; }
    }
}
