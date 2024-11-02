using static LAKAPSAGAP.Models.ViewModel.ReliefReceivedViewModel;

namespace LAKAPSAGAP.Models.ViewModel
{
    public class WarehouseViewModel
    {
        public class Warehouse
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Location { get; set; }
            public ICollection<ReliefReceivedViewModel> ReliefReceivedList { get; set; }
        }
    }
}
