using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LAKAPSAGAP.Models.Models;

namespace LAKAPSAGAP.Models.ViewModels
{
	public class CommonViewModel
	{
		public string Id { get; set; }
		public DateTime DateCreated { get; set; }
		public DateTime DateUpdated { get; set; }
		public string? AddedById { get; set; } 
		public UserInfo? AddedBy { get; set; }
		public string? LastModifiedById { get; set; }
		public UserInfo? LastModifiedBy { get; set; }
		public bool IsDeleted { get; set; }
		public bool isArchived { get; set; }
	}
}
