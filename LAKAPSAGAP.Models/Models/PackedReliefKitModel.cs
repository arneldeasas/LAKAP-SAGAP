namespace LAKAPSAGAP.Models.Models;

public class PackedReliefKit : CommonModel
{
    public string KitId { get; set; }
    public Kit Kit { get; set; }
    public int Quantity { get; set; }
    public DateTime DatePacked { get; set; }
    public string FloorId { get; set; }
    public Floor Floor { get; set; }
    public string RackId { get; set; }
    public Rack Rack { get; set; }
    public string PackedBy { get; set; }
}
