using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAKAPSAGAP.Models.Models
{
    public class CommonModel
    {
        [Key]
        public string Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public string UserId { get; set; } //userid of the user who added the relief
        [ForeignKey(nameof(UserId))]
        public UserInfo LastModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
        public bool isArchived { get; set; }
    }
}
