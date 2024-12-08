using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAKAPSAGAP.Models.Models
{
	public class ReliefRequestDetail:CommonModel
	{
		public string RequestedById { get; set; }
		public UserInfo RequestedBy { get; set; }
		public RequestReason Reason { get; set; }
		public RequestStatus Status { get; set; }
		public string SpecificReason { get; set; }
		public int NumberOfRecipients { get; set; }
		public string Organization { get; set; } // barangay/organization
		public List<Request> RequestList { get; set; }
		public DateTime TargetDateToReceive { get; set; }
		public string ReceiverAddress { get; set; } //Address of evacuation post
		public string AdditionalNotes { get; set; } //for the statement of specific reason
		public string ReceiverName { get; set; } //Name of the person who will receive the relief goods
		public string ContactNumber { get; set; } //Contact number of the person who will receive the relief goods
		public List<RequestAttachment> AttachmentList { get; set; }

	}

	public enum RequestReason
	{
		[Description("Calamity")]
		Calamity = 0,
		[Description("Non-calamity")]
		Noncalamity =1
	}
	public enum RequestStatus
	{
		pending = 0,
		delivered = 0
	}
	public class Request:CommonModel
	{
		public string ReliefRequestId { get; set; }
		public string UnitId { get; set; } // id of either kit or item
		public string UnitName { get; set; } // name of either kit or item
		public RequestType RequestType { get; set; }
		public int Quantity { get; set; }	
		public ReliefRequestDetail ReliefRequest { get; set; }
	}
	public enum RequestType
	{
		Kit = 1,
		Item = 2
	}
	public class RequestAttachment:CommonModel
	{
		public string ReliefRequestId { get; set; }
		public string Url { get; set; }
		public ReliefRequestDetail ReliefRequest { get; set; }

	}
	public class Barangay
	{
		public string Code { get; set; }
		public string Name { get; set; }
		public string OldName { get; set; }
		public bool? SubMunicipalityCode { get; set; }
		public string CityCode { get; set; }
		public bool? MunicipalityCode { get; set; }
		public string DistrictCode { get; set; }
		public bool? ProvinceCode { get; set; }
		public string RegionCode { get; set; }
		public string IslandGroupCode { get; set; }
		public string Psgc10DigitCode { get; set; }
	}

}
