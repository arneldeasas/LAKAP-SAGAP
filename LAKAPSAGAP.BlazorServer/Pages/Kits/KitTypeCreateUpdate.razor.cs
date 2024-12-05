

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
        [Parameter] public KitViewModel KitVM { get; set; }
        KitComponentViewModel ToAdd { get; set; } = new();
        public List<KitComponentViewModel> KitsComponentList { get; set; } = new();

        bool EditMode { get; set; } = false;
        protected override async Task OnInitializedAsync()
        {
            StockItemList = new();
            if (KitVM is not null && !String.IsNullOrEmpty(KitVM.Id))
            {
                EditMode = true;
                foreach (var item in KitVM.KitComponentList)
                {
                    KitsComponentList.Add(item);
                }
                StateHasChanged();
            }
            else
            {
                EditMode = false;
                KitVM = new();
            }
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                //await InsertRow();

                //KitsComponentsDG.Data.Append(ToAdd);
                //KitsComponentsDG.EditRow(ToAdd);
                StockItemList = await KittingRepo.GetAllStockItemsAsync();
                StateHasChanged();
            }
        }

        void AddComponent()
        {
            KitsComponentList.Add(new KitComponentViewModel
            {
                StockItemId = ToAdd.StockItemId,
                ItemName = StockItemList.FirstOrDefault(x => x.Id == ToAdd.StockItemId).Name,
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
				if (!await _jSRuntime.InvokeAsync<bool>("Confirmation")) return;
				if (KitsComponentList.Count == 0)
                {
                    throw new Exception("Please add at least one component to the kit.");
                }
                

                KitVM.KitComponentList = KitsComponentList;

                Kit kit;
                if (!EditMode)
                {
                    kit =await KittingRepo.CreateKit(KitVM);

                }
                else
                {
                   kit =  await KittingRepo.UpdateKit(KitVM);

                }
                await _jSRuntime.InvokeVoidAsync("Toast", "success", "Kit successfully created!");
                _dialogService.Close(kit.Id);
            }
            catch (Exception e)
            {
                await _jSRuntime.InvokeVoidAsync("Toast", "error", e.Message);
                _dialogService.Close(false);
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
