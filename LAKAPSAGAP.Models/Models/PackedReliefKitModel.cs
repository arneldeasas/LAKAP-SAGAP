using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAKAPSAGAP.Models.Models
{
	public class PackedReliefKit
	{
		public string Name { get; set; }
		public string KitType { get; set; } //food or non food kit
		public int Quantity { get; set; }
		public DateTime DatePacked { get; set; }
		public string FloorId { get; set; } //location in warehouse
		[ForeignKey(nameof(FloorId))]
		public Floor Floor { get; set; }
		public string RackId { get; set; }
		[ForeignKey(nameof(RackId))]
		public Rack Rack { get; set; }
		public string PackedBy { get; set; }

	}

	

}
