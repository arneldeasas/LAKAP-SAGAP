namespace LAKAPSAGAP.Services.Repositories;

public interface ICategoryRepository
{
	Task<string?> CreateCategory(CategoryViewModel categoryVM);
	Task<Category?> GetCategory(string categoryID);
	Task<List<Category>> GetAllCategory();
	Task<List<Category>> GetAllActiveCategories();
	Task<bool> UpdateCategory(CategoryViewModel categoryVM);
	//Task DeleteCategory(Category category); // No Deletion of Data Please
}
