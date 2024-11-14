using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAKAPSAGAP.Models.ViewModels
{
	public class StockTypeViewModel
	{
		public string Name { get; set; }
	}
	public class StockCategoryViewModel
	{
		public string Name { get; set; }
	}
	public class StockItemViewModel
	{
	
		public string StockCategoryId { get; set; }
		public string Name { get; set; }
		public string UoMId { get; set; }
	}
	public class UoMViewModel
	{
		public string Name { get; set; }
	}
	public class FloorViewModel
	{
		public string Name { get; set; }
		public List<RackViewModel> RackList { get; set; } = new();
	}
	public class RackViewModel
	{
		public string FloorId { get; set; }
		public string Name { get; set; }
	}
}
