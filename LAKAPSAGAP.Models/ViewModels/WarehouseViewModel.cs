using static LAKAPSAGAP.Models.ViewModels.ReliefReceivedViewModel;

namespace LAKAPSAGAP.Models.ViewModels
{
    public class WarehouseViewModel
    {
        public class Warehouse
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Location { get; set; }
            public ICollection<ReliefReceived> ReliefReceivedList { get; set; }
        }
    }
}
