namespace LAKAPSAGAP.BlazorServer.Pages.Stocks;

public partial class ReleasedStocksDataGrid
{

    [Inject] IReliefRequestRepository _reliefRequestRepo { get; set; }

    public List<ReliefRequestDetailViewModel> TableData { get; set; } = new();

    [Parameter] public bool ReloadTable { get; set; }
    [Parameter] public EventCallback<bool> OnValueChanged { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {

        if (firstRender)
        {
            await LoadRequestData();
            StateHasChanged();
        }// To be enhanced;

        async Task LoadRequestData()
        {
            List<ReliefRequestDetail> _requests = await _reliefRequestRepo.GetDoneRequests();
            TableData = _requests.Select(x => new ReliefRequestDetailViewModel
            {
                Id = x.Id,
                RequestedById = x.RequestedById,
                RequestedBy = new UserInfoViewModel
                {
                    FirstName = x.RequestedBy.FirstName,
                    LastName = x.RequestedBy.LastName,
                },
                Reason = x.Reason,
                SpecificReason = x.SpecificReason,
                NumberOfRecipients = x.NumberOfRecipients,
                Organization = x.Organization,
                TargetDateToReceive = x.TargetDateToReceive,
                RequestList = x.RequestList.Select(x => new RequestViewModel
                {
                    Id = x.Id,
                    UnitId = x.UnitId,
                    UnitName = x.UnitName,
                    Quantity = x.Quantity,
                }).ToList()

                
            }).ToList();
        }
    }
}
