﻿using Blazored.LocalStorage;

namespace LAKAPSAGAP.BlazorServer.Pages.Warehouse;

public partial class Warehouse
{
    [Parameter] required public string Id { get; set; }
    [Inject] DialogService _dialogService { get; set; }
    [Inject] IWarehouseRepository? WarehouseRepo { get; set; }
    [Inject] protected IJSRuntime _jSRuntime { get; set; } = default!;
    [Inject] NavigationManager? NavManager { get; set; }
    [Inject] ILocalStorageService _localStorageService { get; set; }


    [Parameter] public EventCallback<bool> OnValueChanged { get; set; }
    [Parameter] public bool IsKits { get; set; } = false;

    bool Loading { get; set; }

    public WarehouseViewModel model { get; set; }
    public List<LAKAPSAGAP.Models.Models.Warehouse> warehouses { get; set; }

    protected override void OnInitialized()
    {
        Loading = true;
        model ??= new();
        model.FloorList ??= [];
        warehouses ??= [];
        base.OnInitialized();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {

        if (firstRender)
        {
            ChangeValue(false);
            var res = await WarehouseRepo.GetWarehouseById(Id);

            if (res is null)
            {
                var pickedWhse = await WarehouseRepo.PickWarehouse();
                if (pickedWhse is null) { NavManager.NavigateTo("/Warehouse"); return; }
                NavManager.NavigateTo($@"/Warehouse/{pickedWhse.Id}/{(IsKits ? "Kits" : "Stocks")}", true);
                Loading = false;
                StateHasChanged();
                return;
            }

            await _localStorageService.SetItemAsync("whs", res.Id);


            model = new WarehouseViewModel
            {
                Id = res.Id,
                Name = res.Name,
                Location = res.Location
            };


            foreach (var (floor, index) in res.FloorList.Select((floor, index) => (floor, index)))
            {
                model.FloorList.Add(new FloorViewModel
                {
                    Name = floor.Name,
                });

                foreach (var rack in floor.Racks)
                {
                    model.FloorList[index].RackList.Add(new RackViewModel
                    {
                        FloorId = floor.Id,
                        Name = rack.Name
                    });
                }
            }

            warehouses = await WarehouseRepo.GetAllWarehouses();
            Loading = false;
            ChangeValue(true);
            StateHasChanged();
        }
    }

    void ChangeValue(bool initialized)
    {
        OnValueChanged.InvokeAsync(initialized);
    }

    public void NavigateToWhse(string selectedWhse)
    {
        NavManager.NavigateTo($"/Warehouse/{selectedWhse}", true);
    }


}

