using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAKAPSAGAP.Models.Models
{
	public class Kit:CommonModel
	{
		public string Name { get; set; }
		public string? Description { get; set; }
		public KitType KitType { get; set; }
		public List<KitComponent> Components { get; set; }
	}

	// component of a Kit
	public class KitComponent : CommonModel
	{
		public string ItemId { get; set; }
		[ForeignKey(nameof(ItemId))]
		public StockItem StockItem { get; set; }
		public int Quantity  { get; set; }
	}

	public enum KitType
	{
		FoodKit = 0,
		NonFoodKit = 1
	}
}
