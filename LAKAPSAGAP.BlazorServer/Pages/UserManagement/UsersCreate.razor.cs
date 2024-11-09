
using LAKAPSAGAP.Models.ViewModels;
using LAKAPSAGAP.Services.Core;
using Microsoft.AspNetCore.Identity;

namespace LAKAPSAGAP.BlazorServer.Pages.UserManagement
{
	public partial class UsersCreate
	{

		[Inject] private IUserRepository? UserRepo { get; set; }
		public CreateAccountViewModel model { get; set; } = new();
		string id;

		public List<IdentityRole> userRoles { get; set; }

		protected override async Task OnInitializedAsync()
		{
			userRoles = await UserRepo.GetUserRoles();
		}

		private async Task CreateUser()
		{
			try
			{
				var user = await UserRepo.CreateUser(model);

				// NEED TO RETURN THE CREATED MODEL/USER TO ADD IT TO THE PARENT DATAGRID
				//	=> THIS IS DONE TO SIMULATE UPDATING OF LATEST VALUES

				datalist.Add(user);
				await datagrid.Reload();
				dialogService.Close(false);
				StateHasChanged();
			}
			catch (Exception e)
			{
				throw;
			}
		}

	}
}
