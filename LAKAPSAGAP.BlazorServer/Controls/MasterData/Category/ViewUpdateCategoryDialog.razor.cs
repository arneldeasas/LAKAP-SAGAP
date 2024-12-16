using CategoryModel = LAKAPSAGAP.Models.Models.Category;

namespace LAKAPSAGAP.BlazorServer.Controls.MasterData.Category;

public partial class ViewUpdateCategoryDialog
{
	[Parameter] public string Id { get; set; }

	[Inject] DialogService _dialogService { get; set; }
	[Inject] ICategoryRepository _categoryRepo { get; set; }
	[Inject] protected IJSRuntime _jSRuntime { get; set; }

	CategoryViewModel _category { get; set; }

	bool _isBusy { get; set; }
	bool _isEditing { get; set; }
	bool _isActive { get; set; }

	protected override void OnInitialized()
	{
		_category = new();
		base.OnInitialized();
	}

	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		if(firstRender)
		{
			CategoryModel? category = await _categoryRepo.GetCategory(Id);
			if(category is not null)
			{
				_category.Id = category.Id;
				_category.Code = category.Code;
				_category.Name = category.Name;
				_category.isArchived = category.isArchived;
			}
			else
			{
				//TODO: Toast an error
				// Initializes the cateogry as new to avoid errors
				_category = new();
			}
			
			_isActive = !_category.isArchived;
			StateHasChanged();
		}
		await base.OnAfterRenderAsync(firstRender);
	}

	async Task Submit()
	{
		if (!await _jSRuntime.InvokeAsync<bool>("Confirmation")) return;

		SetBusy(true);
		_category.isArchived = !_isActive;
		bool success = await _categoryRepo.UpdateCategory(_category);
		SetBusy(true);

		// TODO: have the user be notified about the process
		//if (success)
		//else

		_dialogService.Close();
	}

	void SetBusy(bool busy)
	{
		_isBusy = busy;
		StateHasChanged();
	}
}