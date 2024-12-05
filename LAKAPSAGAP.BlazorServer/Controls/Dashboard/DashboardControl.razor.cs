namespace LAKAPSAGAP.BlazorServer.Controls.Dashboard;

public partial class DashboardControl
{
    List<PendingReliefRequest> _pendingReliefRequestsToday { get; set; }

	protected override void OnInitialized()
	{
        _pendingReliefRequestsToday ??= [
            new(){
                RequestorName = "Kap. Eduardo Jr.",
                RequestorEmail = "arneldeasas@gmail.com",
                Barangay = "Karuhatan",
                KitName = "Food Pack",
                MainReason = "Fire Incident",
                RequestDate = DateTime.Now,
            },
            new(){
                RequestorName = "CSWD Admin Ernesto",
                RequestorEmail = "naian@gmail.com",
                Barangay = "Dalandanan",
                KitName = "Family Kit",
                MainReason = "Flood Incident",
                RequestDate = DateTime.Now,
            },
            new(){
                RequestorName = "CSWD Admin Soledad",
                RequestorEmail = "charlesm.herrera0700@gmail.com",
                Barangay = "Manotoc",
                KitName = "Food Pack",
                MainReason = "Bahay Kalinga",
                RequestDate = DateTime.Now,
            }];
		base.OnInitialized();
	}

	class PendingReliefRequest
	{
        public string RequestorName { get; set; }
        public string? RequestorEmail { get; set; }
        public string Image { get; set; }
        public string Barangay { get; set; }
        public string KitName { get; set; }
        public string MainReason { get; set; }
        public DateTime RequestDate { get; set; }
    }

}