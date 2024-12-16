using LAKAPSAGAP.Services;
using LAKAPSAGAP.Services.Core;
using Microsoft.EntityFrameworkCore;
using static LAKAPSAGAP.Models.Models.Kit;

namespace LAKAPSAGAP.BlazorServer.Pages.Kits;

public partial class KitsAvailability
{
	[Inject] IKittingRepository KittingRepo { get; set; }

	[Parameter] public bool ReloadKitsTable { get; set; } = false;

	public List<KitViewModel> KitList { get; set; } = new List<KitViewModel>();
	public List<StockItem> stockItems = new List<StockItem>();


	[Parameter] public EventCallback<bool> OnValueChanged { get; set; }
	private bool isLoading = false;
	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		if (firstRender)
		{
			await LoadAllKits();
			StateHasChanged();
		}

	}
	protected override async Task OnParametersSetAsync()
	{
		if (ReloadKitsTable)
		{
			await LoadAllKits();
			ChangeValue(false);
		}
	}
	async Task LoadAllKits()
	{
		try
		{

			List<Kit> kitList = await KittingRepo.GetAllKitsAsync();
			KitList = kitList.Select(x => new KitViewModel
			{
				Id = x.Id,
				Name = x.Name,
				Description = x.Description,
				KitType = x.KitType,
				KitComponentList = x.KitComponentList.Select(y => new KitComponentViewModel
				{
					Id = y.Id,
					ItemName = y.StockItem.Name,
					Quantity = y.Quantity
				}).ToList()
			}).ToList();
		}
		catch (Exception)
		{

			throw;
		}

	}

	void ChangeValue(bool ReloadTable)
	{
		OnValueChanged.InvokeAsync(ReloadTable);
	}
}
