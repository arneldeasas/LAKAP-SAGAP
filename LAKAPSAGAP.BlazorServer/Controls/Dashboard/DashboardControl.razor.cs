using System.Globalization;

namespace LAKAPSAGAP.BlazorServer.Controls.Dashboard;

public partial class DashboardControl
{
    List<PendingReliefRequest> _pendingReliefRequestsToday { get; set; }
    List<ReleasedOfReliefGoods> _releasedOfReliefGoodsPerMonth { get; set; }

    int _selectedYear { get; set; }

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

        _releasedOfReliefGoodsPerMonth ??= [
            new(){
				ReleasedDate = new DateTime(2023, 1, 1),
				Total = 69,
            },
            new(){
                ReleasedDate = new DateTime(2023, 2, 1),
                Total = 32,
            },
            new(){
                ReleasedDate = new DateTime(2023, 3, 1),
                Total = 109,
            },
            new(){
                ReleasedDate = new DateTime(2023, 4, 1),
                Total = 37,
            },
            new(){
                ReleasedDate = new DateTime(2023, 5, 1),
                Total = 45,
            },
            new(){
                ReleasedDate = new DateTime(2023, 6, 1),
                Total = 91,
            },
            new(){
                ReleasedDate = new DateTime(2023, 7, 1),
                Total = 60,
            },
            new(){
                ReleasedDate = new DateTime(2023, 8, 1),
                Total = 116,
            },
            new(){
                ReleasedDate = new DateTime(2023, 9, 1),
                Total = 73,
            },
            new(){
                ReleasedDate = new DateTime(2023, 10, 1),
                Total = 67,
            },
            new(){
                ReleasedDate = new DateTime(2023, 11, 1),
                Total = 82,
            },
            new(){
                ReleasedDate = new DateTime(2023, 12, 1),
                Total = 62,
            },
            new(){
                ReleasedDate = new DateTime(2024, 1, 1),
                Total = 62,
            }];

		_selectedYear = _releasedOfReliefGoodsPerMonth.DistinctBy(x => x.ReleasedDate.Year).First().ReleasedDate.Year;
		base.OnInitialized();
	}

	string FormatAsMonthsName(object value)
	{
		return ((DateTime)value).ToString("MMM");
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

    class ReleasedOfReliefGoods
    {
        public DateTime ReleasedDate { get; set; }
        public double Total { get; set; }
    }

}