﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAKAPSAGAP.Models.Models
{
    public class Warehouse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public ICollection<ReliefReceived> ReliefReceivedList { get; set; }
    }
}
