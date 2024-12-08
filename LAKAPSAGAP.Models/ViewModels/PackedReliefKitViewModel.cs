﻿using LAKAPSAGAP.Models.Models;

namespace LAKAPSAGAP.Models.ViewModels;

public class PackedReliefKitViewModel
{
    //public string Name { get; set; }
    public string KitType { get; set; }
    public int Quantity { get; set; }
    public DateTime DatePacked { get; set; }
    public string FloorId { get; set; }
    public string RackId { get; set; }
    public string PackedBy { get; set; }
}
