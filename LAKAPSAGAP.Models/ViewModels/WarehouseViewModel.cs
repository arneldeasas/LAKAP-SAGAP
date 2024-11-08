using static LAKAPSAGAP.Models.ViewModel.ReliefReceivedViewModel;

namespace LAKAPSAGAP.Models.ViewModel
{
	public class WarehouseViewModel
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string Location { get; set; }
		public List<FloorViewModel> FloorList { get; set; } = new();
		public class FloorViewModel
		{
			public string Id { get; set; }
			public string WarehouseId { get; set; }
			public string Number { get; set; }
			public List<string> Racks { get; set; } = new();
		}
	}
}
