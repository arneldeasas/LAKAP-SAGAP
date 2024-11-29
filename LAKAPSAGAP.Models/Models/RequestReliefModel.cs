﻿using System;
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
		public List<Request> RequestList { get; set; }
		public DateTime TargetDateToReceive { get; set; }
		public string ReceiverAddress { get; set; } //Address of evacuation post
		public string AdditionalNotes { get; set; } //for the statement of specific reason
		public string ReceiverName { get; set; } //Name of the person who will receive the relief goods
		public int ContactNumber { get; set; } //Contact number of the person who will receive the relief goods
		public List<RequestAttachment> AttachmentList { get; set; }
	}

	public enum RequestReason
	{
		Calamity = 1,
		Noncalamity =2
	}

	public class Request:CommonModel
	{
		public string ReliefRequestId { get; set; }
		public string UnitId { get; set; } // id of either kit or item
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
}
