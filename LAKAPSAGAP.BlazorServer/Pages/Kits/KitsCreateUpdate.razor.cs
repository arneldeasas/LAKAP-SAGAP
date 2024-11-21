namespace LAKAPSAGAP.BlazorServer.Pages.Kits
{
	public partial class KitsCreateUpdate
	{
		[Inject] DialogService _dialogService { get; set; }
		[Inject] protected IJSRuntime _jSRuntime { get; set; } = default!;

		List<KitViewModel> Kits { get; set; } = new();
		public KitViewModel Kit { get; set; } = new();
		public List<ReliefReceivedViewModel> Items { get; set; } = new();

		protected override async Task OnAfterRenderAsync(bool firstRender)
		{
            if (firstRender)
            {
				await InsertRow();
            }
        }

		Task SubmitKit()
		{
			return Task.CompletedTask;
		}

		#region Datagrid Actions

		bool IsEditing = false;
		RadzenDataGrid<KitComponentViewModel> KitsComponentsDG { get; set; } = default!;

		public List<KitComponentViewModel> KitsComponentList { get; set; } = new();
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
