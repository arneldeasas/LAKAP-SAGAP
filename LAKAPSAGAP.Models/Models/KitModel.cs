using System.ComponentModel;

namespace LAKAPSAGAP.Models.Models;

public class Kit : CommonModel
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? PersonAssigned { get; set; }
    public int Quantity { get; set; }
    public KitType KitType { get; set; }
    public DateTime DatePacked {  get; set; }
    public Rack Location { get; set; }
    public List<KitComponent> KitComponentList { get; set; }
    public List<PackedReliefKit> PackedReliefKitList { get; set; }
}

// component of a Kit
public class KitComponent : CommonModel
{
    public string KitId { get; set; }
	public Kit Kit { get; set; }
	public string StockItemId { get; set; }
    public StockItem StockItem { get; set; }
    public int Quantity { get; set; }
}

public enum KitType
{
    [Description("Food Kit")]
    FoodKit = 0,
	[Description("Non-Food Kit")]
	NonFoodKit = 1
}
