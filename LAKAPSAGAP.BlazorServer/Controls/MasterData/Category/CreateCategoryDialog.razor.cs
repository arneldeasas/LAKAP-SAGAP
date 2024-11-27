namespace LAKAPSAGAP.BlazorServer.Controls.MasterData.Category;

public partial class CreateCategoryDialog
{
	[Inject] DialogService _dialogService { get; set; }
	[Inject] ICategoryRepository _categoryRepo { get; set; }
	[Inject] IJSRuntime _jSRuntime { get; set; }

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

		if (!string.IsNullOrEmpty(newId)) await _jSRuntime.InvokeVoidAsync("Toast", "success", "Category added successfully!");
		else await _jSRuntime.InvokeVoidAsync("Toast", "error", "An error occured. Something went wrong!");
		
		_dialogService.Close(newId);
	}

	void SetBusy(bool busy)
	{
		_isBusy = busy;
		StateHasChanged();
	}
}