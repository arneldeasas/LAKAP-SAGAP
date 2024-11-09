using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAKAPSAGAP.Models.Models
{
    public class Warehouse : CommonModel
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public List<ReliefReceived> ReliefReceivedList { get; set; } = new();
        public List<Floor> FloorList { get; set; } = new();
    }
}
