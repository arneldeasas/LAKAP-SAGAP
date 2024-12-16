namespace LAKAPSAGAP.Models.ViewModels;

public class CategoryViewModel
{
	public string Id { get; set; }
	public string Code { get; set; }
	public string Name { get; set; }
	public bool isArchived { get; set; }

	public CategoryViewModel() => isArchived = false;
}
