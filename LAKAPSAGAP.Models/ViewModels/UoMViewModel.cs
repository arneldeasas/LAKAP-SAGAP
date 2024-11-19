namespace LAKAPSAGAP.Models.ViewModels;

public class UoMViewModel
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Symbol { get; set; }
    public bool isArchived { get; set; }

	public UoMViewModel() => isArchived = false;
}
