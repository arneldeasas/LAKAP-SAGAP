namespace LAKAPSAGAP.Models.ViewModel
{
	public class WarehouseViewModel
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string Location { get; set; }
		public List<FloorViewModel> FloorList { get; set; } = new();
		
	}
}
