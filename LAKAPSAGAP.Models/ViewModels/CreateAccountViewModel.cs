
using Microsoft.AspNetCore.Identity;

namespace LAKAPSAGAP.Models.ViewModels
{
    public class CreateAccountViewModel
    {
        public string Id { get; set; }
        [Required]
        public string RoleId { get; set; }
		[Required(ErrorMessage = "First name is required.")]
		[StringLength(100, ErrorMessage = "First name cannot be longer than 100 characters.")]
		public string FirstName { get; set; }

		[StringLength(100, ErrorMessage = "Middle name cannot be longer than 100 characters.")]
		public string MiddleName { get; set; }

		[Required(ErrorMessage = "Last name is required.")]
		[StringLength(100, ErrorMessage = "Last name cannot be longer than 100 characters.")]
		public string LastName { get; set; }

		[Required(ErrorMessage = "Barangay is required.")]
		[StringLength(100, ErrorMessage = "Barangay name cannot be longer than 100 characters.")]
		public string Barangay { get; set; }

		[Required(ErrorMessage = "Email is required.")]
		[EmailAddress(ErrorMessage = "Invalid email address.")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Phone number is required.")]
		[Phone(ErrorMessage = "Invalid phone number.")]
		public string Phone { get; set; }

		[Required(ErrorMessage = "Username is required.")]
		[StringLength(50, ErrorMessage = "Username cannot be longer than 50 characters.")]
		public string Username { get; set; }

		[Required(ErrorMessage = "Password is required.")]
		[StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
		public string Password { get; set; }

		[Required(ErrorMessage = "At least one file is required.")]
		public List<IBrowserFile> fileList { get; set; }
		public IdentityRole UserRole { get; set; }
		public string UserAuthId { get; set; }
    }
}
