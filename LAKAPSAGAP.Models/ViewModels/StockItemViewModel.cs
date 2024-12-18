using LAKAPSAGAP.Models.Models;

namespace LAKAPSAGAP.Models.ViewModels;

public class StockItemViewModel
{
    public string Id { get; set; }
    public string Name { get; set; }
	public string CategoryId { get; set; }
	public string? CategoryName { get; set; }
	public string UoMId { get; set; }
    public string? UoMName { get; set; }
    public bool isArchived { get; set; }
    public int? Quantity { get; set; }

    public int? ReceivedQty { get; set; } //lifetime total
    public int? StockQty { get; set; } //current
    public int? SentQty { get; set; }

}
