﻿
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
				await UserRepo.CreateUser(model);

			}
			catch (Exception e)
			{
				throw;
			}
		}

	}
}
