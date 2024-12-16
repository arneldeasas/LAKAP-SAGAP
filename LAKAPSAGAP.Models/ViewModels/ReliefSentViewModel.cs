using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LAKAPSAGAP.Models.Models;
namespace LAKAPSAGAP.Models.ViewModels
{
    public class ReliefSentViewModel 
    {
        public string ReliefRequestId { get; set; }
        public ReliefRequestDetailViewModel ReliefRequest { get; set; }
        public List<ReliefSentKitViewModel> SentKitList { get; set; }
        public List<ReliefSentItemViewModel> SentItemList { get; set; }
    }

    public class ReliefSentItemViewModel
    {
        public string ReliefSentId { get; set; }
        public ReliefSentViewModel ReliefSent { get; set; }
        public int Quantity { get; set; }
        public string StockItemId { get; set; }
        public StockItemViewModel StockItem { get; set; }
    }
    public class ReliefSentKitViewModel
    {
        public string ReliefSentId { get; set; }
        public ReliefSentViewModel ReliefSent { get; set; }
        public int Quantity { get; set; }
        public string KitId { get; set; }
        public KitViewModel Kit { get; set; }
    }

}
