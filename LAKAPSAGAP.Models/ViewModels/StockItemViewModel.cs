using LAKAPSAGAP.Models.Models;

namespace LAKAPSAGAP.Models.ViewModels;

public class StockItemViewModel
{
    public string Id { get; set; }
    public string Name { get; set; }
	public string CategoryId { get; set; }
	public string UoMId { get; set; }
	public string FloorId { get; set; }
    public bool isArchived { get; set; }
}
