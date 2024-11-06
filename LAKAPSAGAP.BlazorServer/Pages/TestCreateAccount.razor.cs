
namespace LAKAPSAGAP.BlazorServer.Pages
{
    public partial class TestCreateAccount
    {
        CreateAccountViewModel createAccountViewModel = new();
        [Inject] NavigationManager? navManager { get; set; }
        [Inject] IUserRepository? userRepository { get; set; } 

        string? errorMessage = string.Empty;
        private async Task HandleCreate()
        {
            try
            {
                await userRepository.CreateUser(createAccountViewModel);

                navManager.NavigateTo("/");
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
                throw;
            }
        }

        private void HandleFileSelected(InputFileChangeEventArgs e)
        {
            createAccountViewModel.fileList = e.GetMultipleFiles().ToList();
        }
    }
}