using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LAKAPSAGAP.Models.Models;

namespace LAKAPSAGAP.Models.ViewModels
{
	public class KitViewModel 
	{
		public string Name { get; set; }
		public string? Description { get; set; }
		public KitType KitType { get; set; }
		public List<KitComponent> KitComponentList { get; set; }
	}

	// component of a Kit
	public class KitComponentViewModel
	{
		public string ItemId { get; set; }
		[ForeignKey(nameof(ItemId))]
		public StockItem StockItem { get; set; }
		public int Quantity { get; set; }
	}
}
