
using Microsoft.AspNetCore.Identity;

namespace LAKAPSAGAP.Models.Models;

public class UserInfo : CommonModel
{
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string LastName { get; set; }
    public string Barangay { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string RoleId { get; set; }
    public IdentityRole Role { get; set; }
	public string UserAuthId { get; set; }
	public UserAuth UserAuth { get; set; }

	public List<UserInfo> AddedUsers { get; set; }
	public List<UserInfo> LasModifiedByList { get; set; }

	public List<Attachment> UserAttachments { get; set; }
	public List<Attachment> AddedAttachments { get; set; }
    public List<Category> CategoriesCreated { get; set; }
}

public class Attachment : CommonModel
{
    public string Url { get; set; }
    public string UserId { get; set; }
    public UserInfo User { get; set; }
}
