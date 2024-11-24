namespace LAKAPSAGAP.BlazorServer.Controls.MasterData.Category;

public partial class CreateCategoryDialog
{
	[Inject] DialogService _dialogService { get; set; }
	[Inject] ICategoryRepository _categoryRepo { get; set; }
	[Inject] protected IJSRuntime _jSRuntime { get; set; }

	CategoryViewModel _newCategory { get; set; }

	bool _isBusy { get; set; }
	bool _isActive { get; set; }

	protected override void OnInitialized()
	{
		_newCategory = new ();
		_isActive = true;
		base.OnInitialized();
	}

	async Task Submit()
	{
		if (!await _jSRuntime.InvokeAsync<bool>("Confirmation")) return;

		SetBusy(true);
		_newCategory.isArchived = !_isActive;
		string? newId = await _categoryRepo.CreateCategory(_newCategory);
		SetBusy(true);
		_dialogService.Close(newId);        

		// TODO: have the user be notified about the error

        //if (!string.IsNullOrEmpty(newId))
        //{
        //}
	}

	void SetBusy(bool busy)
	{
		_isBusy = busy;
		StateHasChanged();
	}
}