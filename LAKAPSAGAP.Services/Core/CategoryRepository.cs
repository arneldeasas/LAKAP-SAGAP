using LAKAPSAGAP.Services.Core.Helpers;
using Microsoft.EntityFrameworkCore.Storage;

namespace LAKAPSAGAP.Services.Core;

public class CategoryRepository(MyDbContext context) : ICategoryRepository
{
	readonly MyDbContext _context = context;

	public async Task<string?> CreateCategory(CategoryViewModel categoryVM)
	{
		using IDbContextTransaction transaction = _context.Database.BeginTransaction();
		string? newCategoryId = null;

		try
		{
			int categoryCount = await _context.GetCount<Category>();
			string categoryId = IdGenerator.GenerateId(IdGenerator.PFX_CATEGORY, categoryCount);

			await _context.Create<Category>(new()
			{
				Id = categoryId,
				Code = categoryVM.Code,
				Name = categoryVM.Name,
				isArchived = categoryVM.isArchived
			});

			await transaction.CommitAsync();
			newCategoryId = categoryId;
		}
		catch (Exception)
		{
			await transaction.RollbackAsync();
		}

		return newCategoryId;
	}

	public async Task<List<Category>> GetAllCategory()
	{
		try
		{
			List<Category> categoryList = [];
			categoryList = await _context.GetAll<Category>();
			return categoryList;
		}
		catch (Exception)
		{
			return [];
		}
	}
	
	public async Task<List<Category>> GetAllActiveCategories()
	{
		try
		{
			List<Category> categoryList = [];
			categoryList = await _context.GetAllNotDeleted<Category>();
			return categoryList;
		}
		catch (Exception)
		{
			return [];
		}
	}

	public async Task<Category?> GetCategory(string categoryID)
	{
		try
		{
			Category? category = null;
			category = await _context.GetByIdIncludeArchivedsOnly<Category>(categoryID);
			return category;
		}
		catch (Exception)
		{
			return null;
		}
	}

	public async Task<bool> UpdateCategory(CategoryViewModel categoryVM)
	{
		using IDbContextTransaction transaction = _context.Database.BeginTransaction();
		try
		{
			Category? category = await _context.GetByIdIncludeArchivedsOnly<Category>(categoryVM.Id);
			if (category is null) return false;

			category.Code = categoryVM.Code;
			category.Name = categoryVM.Name;
			category.isArchived = categoryVM.isArchived;

			await _context.UpdateItem(category);

			await transaction.CommitAsync();
			return true;
		}
		catch (Exception)
		{
			await transaction.RollbackAsync();
			return false;
		}
	}
}
