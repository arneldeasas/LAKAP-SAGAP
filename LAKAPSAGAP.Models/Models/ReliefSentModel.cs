using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAKAPSAGAP.Models.Models
{
	public class ReliefSent : CommonModel
	{
		public string ReliefRequestId { get; set; }
		public ReliefRequestDetail ReliefRequest { get; set; }
		public List<ReliefSentKit> SentKitList { get; set; }
		public List<ReliefSentItem> SentItemList { get; set; }

	}

	public class ReliefSentItem : CommonModel
	{
		public string ReliefSentId { get; set; }
		public ReliefSent ReliefSent { get; set; }
		public int Quantity { get; set; }
		public string StockItemId { get; set; }
		public StockItem StockItem { get; set; }
	}
	public class ReliefSentKit : CommonModel
	{
		public string ReliefSentId { get; set; }
		public ReliefSent ReliefSent { get; set; }
		public int Quantity { get; set; }
		public string KitId { get; set; }
		public Kit Kit { get; set; }
	}

}
