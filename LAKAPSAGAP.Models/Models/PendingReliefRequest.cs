namespace LAKAPSAGAP.Models.Models;

public class PendingReliefRequest
{
	public string RequestorName { get; set; }
	public string? RequestorEmail { get; set; }
	public string Image { get; set; }
	public string Barangay { get; set; }
	public string KitName { get; set; }
	public string MainReason { get; set; }
	public DateTime RequestDate { get; set; }
}
