using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAKAPSAGAP.Models.Models
{
	public class ReliefRequestDetail:CommonModel
	{
		public string RequestedById { get; set; }
		public RequestReason Reason { get; set; }
		public string SpecificReason { get; set; }
		public string Organization { get; set; } // barangay/organization
	}

	public enum RequestReason
	{
		Calamity = 1,
		Noncalamity =2
	}

	public class ReliefRequest
	{

	}
}
