﻿

namespace LAKAPSAGAP.BlazorServer.Pages.Kits
{
	public partial class KitTypeCreateUpdate
	{
		[Inject] DialogService _dialogService { get; set; }
		[Inject] protected IJSRuntime _jSRuntime { get; set; } = default!;
		[Inject] IKittingRepository KittingRepo { get; set; }
		List<KitViewModel> Kits { get; set; } = new();
		
		public List<ReliefReceivedViewModel> Items { get; set; } = new();

		//Initial Data
		List<StockItem> StockItemList = new();

		//form state
		KitViewModel KitVM { get; set; } = new();
		KitComponentViewModel ToAdd { get; set; } = new();
		public List<KitComponentViewModel> KitsComponentList { get; set; } = new();
		
		protected override async Task OnInitializedAsync()
		{
			StockItemList = await KittingRepo.GetAllStockItemsAsync();
		}
		protected override async Task OnAfterRenderAsync(bool firstRender)
		{
            if (firstRender)
            {
				//await InsertRow();

				//KitsComponentsDG.Data.Append(ToAdd);
				//KitsComponentsDG.EditRow(ToAdd);
			}
        }

		void AddComponent()
		{
			KitsComponentList.Add(new KitComponentViewModel { 
				StockItemId = ToAdd.StockItemId,
				ItemName = StockItemList.FirstOrDefault(x=>x.Id == ToAdd.StockItemId).Name,
				Quantity = ToAdd.Quantity,
			});
			ToAdd = new KitComponentViewModel();
		}
		void ResetComponentForm()
		{
			ToAdd = new KitComponentViewModel();
		}
		void RemoveComponent(int index)
		{
			KitsComponentList.RemoveAt(index);
			StateHasChanged();
		}
		async Task SubmitKit()
		{
	
			try
			{
				
				if (KitsComponentList.Count == 0)
				{
					throw new Exception("Please add at least one component to the kit.");
				}
				if (!await _jSRuntime.InvokeAsync<bool>("Confirmation")) return;

				KitVM.KitComponentList = KitsComponentList;
				await KittingRepo.CreateKit(KitVM);
				await _jSRuntime.InvokeVoidAsync("Toast", "success", "Kit successfully created!");
				_dialogService.Close(true);
			}
			catch (Exception e)
			{
				await _jSRuntime.InvokeVoidAsync("Toast", "error", e.Message);
			
			}
		}

		#region Datagrid Actions

		bool IsEditing = false;
		RadzenDataGrid<KitComponentViewModel> KitsComponentsDG { get; set; } = default!;

		
		List<KitComponentViewModel> KitsComponentsToInsert = new();
		Dictionary<KitComponentViewModel, KitComponentViewModel> KitsComponentToUpdate = new();

		async Task InsertRow()
		{
			IsEditing = true;
			var kitComponent = new KitComponentViewModel();
			KitsComponentsToInsert.Add(kitComponent);
			await KitsComponentsDG.InsertRow(kitComponent);
		}

		async Task EditRow(KitComponentViewModel kitComponent)
		{
			IsEditing = true;
			var Original = kitComponent;
			KitsComponentToUpdate.Add(kitComponent, Original);
			await KitsComponentsDG.EditRow(kitComponent);
		}

		void CancelEdit(KitComponentViewModel kitComponent)
		{
			KitsComponentToUpdate[kitComponent] = kitComponent;
			Reset(kitComponent);
			KitsComponentsDG.CancelEditRow(kitComponent);
			IsEditing = false;
		}

		async Task DeleteRow(KitComponentViewModel kitComponent)
		{
			Reset(kitComponent);
			KitsComponentsDG.CancelEditRow(kitComponent);
			KitsComponentList.Remove(kitComponent);

			await KitsComponentsDG.Reload();
			await ShowInsertRow(false);
		}

		void OnCreateRow(KitComponentViewModel kitComponent)
		{
			Reset(kitComponent);
			KitsComponentList.Add(kitComponent);
		}

		void OnUpdateRow(KitComponentViewModel kitComponent)
		{
			Reset(kitComponent);
		}

		async Task SaveRow(KitComponentViewModel kitComponent)
		{
			await KitsComponentsDG.UpdateRow(kitComponent);
			IsEditing = false;
			await InsertRow();
		}
		
		void Reset()
		{
			KitsComponentsToInsert.Clear();
			KitsComponentToUpdate.Clear();
		}

		void Reset(KitComponentViewModel kitComponent)
		{
			KitsComponentsToInsert.Remove(kitComponent);
			KitsComponentToUpdate.Remove(kitComponent);
		}

		async Task ShowInsertRow(bool withCancel = true)
		{
			await Task.Delay(200);
            foreach (var item in KitsComponentsToInsert)
            {
				if (withCancel)
					KitsComponentsDG.CancelEditRow(item);
				await KitsComponentsDG.InsertRow(item);
            }
        }

		#endregion
	}
}