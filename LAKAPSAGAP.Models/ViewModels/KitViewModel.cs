﻿using LAKAPSAGAP.Models.Models;

namespace LAKAPSAGAP.Models.ViewModels;

public class KitViewModel
{
    public string? Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
     public KitType KitType { get; set; } // Kit Type is either Food or Non-Food
     public List<KitComponentViewModel> KitComponentList { get; set; } // Items sa loob ng isang Kit
    public DateTime ExpiryDate { get; set; }
    public DateTime ManufactureDate { get; set; }
    public RackViewModel Location { get; set; }
   
    public int Quantity { get; set; }
   
}

// component of a Kit <== kung anu man yung nasa Item Master Data
public class KitComponentViewModel
{
    public string? Id { get; set; }
    public string KitId { get; set; }
	public string StockItemId { get; set; }
    public string ItemName { get; set; }
    public int Quantity { get; set; }
    public int QuantityOnHand { get; set; }
    public int Status { get; set; }
}

public class KitAssemblyViewModel
{
    public string? Id { get; set; }

}