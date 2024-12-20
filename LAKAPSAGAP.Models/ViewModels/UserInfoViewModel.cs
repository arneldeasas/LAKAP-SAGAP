﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAKAPSAGAP.Models.ViewModel
{
    public class UserInfoViewModel 
    {
		public string Id { get; set; }
		public string RoleId { get; set; }
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string LastName { get; set; }
		public string Barangay { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
        public string RoleName { get; set; }
		public ICollection<Attachment> Attachments { get; set; }

		public static List<string> UserRoles { get; set; } = new()
        {
            "Representative",
            "Office Head",
            "Admin Staff"
        };

        public class Attachment
        {
            public string Id { get; set; }
            public string UserId { get; set; }
            public string Url { get; set; }
        }

    }
 
    
}
