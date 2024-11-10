
using LAKAPSAGAP.Models.Models;
using LAKAPSAGAP.Models.ViewModels;
using LAKAPSAGAP.Services.Core;
using Microsoft.AspNetCore.Identity;
using System.IO;

namespace LAKAPSAGAP.BlazorServer.Pages.UserManagement
{
	public partial class UsersCreateUpdate
	{
		[Inject] private IAuthRepository? AuthRepo { get; set; }
		[Inject] private IUserRepository? UserRepo { get; set; }
		[Inject] private IUserAttachmentRepository? UserFileRepo { get; set; }
		[Inject] protected IJSRuntime _jSRuntime { get; set; } = default!;



		[Parameter]
		public DialogService dialogService { get; set; } = default!;

		[Parameter]
		public RadzenDataGrid<UserInfo> datagrid { get; set; } = default!;

		[Parameter]
		public List<UserInfo> datalist { get; set; } = new();

		[Parameter]
		public UserInfo? userData
		{
			get => new UserInfo();
			set
			{
				if (value != null)
				{
					model = new CreateAccountViewModel
					{
						Id = value.Id,
						UserAuthId = value.UserAuthId,
						RoleId = value.RoleId,
						FirstName = value.FirstName,
						MiddleName = value.MiddleName,
						LastName = value.LastName,
						Barangay = value.Barangay,
						Email = value.Email,
						Phone = value.Phone,
						UserRole = value.Role,
					};
				}
			}
		}



		private IJSObjectReference _js { get; set; } = default!;
		private UserInfo? _userData;
		public CreateAccountViewModel model { get; set; } = new();
		string id;
		public List<IdentityRole> userRoles { get; set; }
		public List<(string Id, string Url)> userFiles { get; set; } = new();
		public List<(string Id, string Url)> filesToDelete { get; set; } = new();
		private UserAuth userAuth { get; set; } = new();




		protected override async Task OnInitializedAsync()
		{
			userRoles = await UserRepo.GetUserRoles();
            if (!String.IsNullOrEmpty(model.Id)) userFiles = await UserFileRepo.GetUserAttachments(model.Id);

            if (!String.IsNullOrEmpty(model.Id))
            {
				userAuth = await AuthRepo.GetAuthUserByUserAuthId(model.UserAuthId);
				model.Username = userAuth.UserName;
			}
        }

		private async Task CreateUser()
		{
			try
			{

                if (!await _jSRuntime.InvokeAsync<bool>("Confirmation")) return;

                var user = await UserRepo.CreateUser(model);

				// NEED TO RETURN THE CREATED MODEL/USER TO ADD IT TO THE PARENT DATAGRID
				//	=> THIS IS DONE TO SIMULATE UPDATING OF LATEST VALUES

				datalist.Add(user);
				await datagrid.Reload();
				dialogService.Close(false);
				StateHasChanged();
			}
			catch (Exception)
			{
				throw;
			}
		}

		private async Task UpdateUser()
		{
			try
			{

				if (!await _jSRuntime.InvokeAsync<bool>("Confirmation")) return;

				foreach (var toDelete in filesToDelete)
                {
					var result = await UserFileRepo.DeleteAttachment(new Attachment
					{
						Id = toDelete.Id,
						Url = toDelete.Url,
						UserId = model.Id
					});
                }

				await UserRepo.UpdateUser(model);
				dialogService.Close(false);
				StateHasChanged();
			}
			catch (Exception)
			{

				throw;
			}
		}

		private async Task HandleFileSelected(InputFileChangeEventArgs e)
		{
			model.fileList = e.GetMultipleFiles().ToList();

			foreach (var file in model.fileList)
			{
				var random = new Random();
				var fileId = $"INMEMORY_{new string(Enumerable.Range(0, 10)
					.Select(_ => (char)random.Next('A', 'Z' + 1))
					.ToArray())}";

				using (var memoryStream = new MemoryStream())
				{
					await file.OpenReadStream().CopyToAsync(memoryStream);
					var fileBytes = memoryStream.ToArray();

					var fileUrl = $"data:{file.ContentType};base64,{Convert.ToBase64String(fileBytes)}";

					userFiles.Add((fileId, fileUrl));
				}
			}
		}
		private async Task RemoveFromList(string fileId)
		{
			// Remove the file from the fileList
			if (!await _jSRuntime.InvokeAsync<bool>("Confirmation", null, "warning", null)) return;

			if (fileId.Contains("INMEMORY"))
			{
				var fileToDelete = model.fileList.FirstOrDefault(f => f.Name == fileId);

				if (fileToDelete != null)
				{
					model.fileList.Remove(fileToDelete);
				}

			}

			filesToDelete.Add((fileId, userFiles.Where(x => x.Id == fileId).Single().Url));
			
			var fileToRemove = userFiles.FirstOrDefault(f => f.Item1 == fileId);
			if (fileToRemove.Url != null)
			{
				userFiles.Remove(fileToRemove);
			}
		}


	}
}
