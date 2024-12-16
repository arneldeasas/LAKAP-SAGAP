namespace LAKAPSAGAP.Models.ViewModels;

public class RackViewModel
{
    public string Id { get; set; }
	public string Name { get; set; }
    public string? FloorId { get; set; }
    public bool isArchived { get; set; }

	public RackViewModel() => isArchived = false;
}
