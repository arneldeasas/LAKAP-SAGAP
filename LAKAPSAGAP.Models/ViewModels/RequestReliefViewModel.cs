
using LAKAPSAGAP.Models.Models;

namespace LAKAPSAGAP.Models.ViewModels
{
	public class ReliefRequestDetailViewModel
	{
		public string? Id { get; set; }
		public string RequestedById { get; set; }
		public RequestReason Reason { get; set; }
		public RequestStatus Status { get; set; }
		public string SpecificReason { get; set; }
		public int NumberOfRecipients { get; set; }
		public string Organization { get; set; } // barangay/organization
		public List<RequestViewModel> RequestList { get; set; } = new();
		public DateTime TargetDateToReceive { get; set; }
		public string ReceiverAddress { get; set; } //Address of evacuation post
		public string AdditionalNotes { get; set; } //for the statement of specific reason
		public string ReceiverName { get; set; } //Name of the person who will receive the relief goods
		public int ContactNumber { get; set; } //Contact number of the person who will receive the relief goods
		public List<RequestAttachmentViewModel> AttachmentList { get; set; }
		public DateTime DateRequested { get; set; }
	}



	public class RequestViewModel 
	{
		public string? Id { get; set; }
		public string? ReliefRequestId { get; set; }
		public string UnitId { get; set; } // id of either kit or item
		public string UnitName { get;set; }
		public RequestType RequestType { get; set; }
		public int Quantity { get; set; }
		public ReliefRequestDetail? ReliefRequest { get; set; }
	}

	public class RequestAttachmentViewModel
	{
		public string? Id { get; set; }
		public string ReliefRequestId { get; set; }
		public string Url { get; set; }
		public ReliefRequestDetail? ReliefRequest { get; set; }

	}

	
}
