using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAKAPSAGAP.Models.Models
{
    [Table("ReliefReceived")]
    public class ReliefReceivedModel
    {
       
        public ReliefReceivedModel()
        {

        
        }
        string Id { get; set; }
        string ReliefType { get; set; }
        
    }
}
