﻿namespace LAKAPSAGAP.Services.Core;

public class CommonRepository<T> : ICommonRepository<T> where T : class
{
    public readonly MyDbContext _context;

    public CommonRepository(MyDbContext context)
    {
        _context = context;
    }
    public async Task<T> Create(T item)
    {
        _context.Set<T>().Add(item);
        await _context.SaveChangesAsync();
        return item;
    }

    public async Task<List<T>> CreateMany(List<T> itemList)
    {
        _context.Set<T>().AddRange(itemList);
        await _context.SaveChangesAsync();
        return itemList;
    }
    public async Task<T> Update(T item)
    {
        _context.Set<T>().Update(item);
        await _context.SaveChangesAsync();
        return item;
    }
    public async Task<T?> Delete(T item)
    {
        _context.Set<T>().Remove(item);
        await _context.SaveChangesAsync();
        return item;
    }
    public async Task<T?> GetById(string Id)
    {
        var item = await _context.Set<T>().FindAsync(Id);
        return item;
    }
    public async Task<List<T>> GetAll()
    {
        var itemList = await _context.Set<T>().ToListAsync();
        return itemList;
    }
    public async Task<int> GetCount()
    {
        return await _context.Set<T>().CountAsync();
    }
}

public static class DbSetExtensions
{
    public static async Task<T> Create<T>(this MyDbContext context, T item) where T : class
    {
        context.Set<T>().Add(item);
        await context.SaveChangesAsync();
        return item;
    }

    public static async Task<List<T>> CreateMany<T>(this MyDbContext context, List<T> itemList) where T : class
    {
        context.Set<T>().AddRange(itemList);
        await context.SaveChangesAsync();
        return itemList;
    }

    public static async Task<T> UpdateItem<T>(this MyDbContext context, T item) where T : class
    {
        context.Set<T>().Update(item);
        await context.SaveChangesAsync();
        return item;
    }
    public static async Task<List<T>> UpdateMany<T>(this MyDbContext context, List<T> itemList) where T : class
    {
        context.Set<T>().UpdateRange(itemList);
        await context.SaveChangesAsync();
        return itemList;
    }

    /// <summary>
    /// This function does a fucking hard delete and detaches data in EF >:<
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="context"></param>
    /// <param name="item"></param>
    /// <returns></returns>
    public static async Task<T?> Delete<T>(this MyDbContext context, T item) where T : class
    {
        context.Set<T>().Remove(item);
        await context.SaveChangesAsync();
        return item;
    }

    public static async Task<T?> GetById<T>(this MyDbContext context, string Id, bool? includeArchived = false, bool? includeDeleted = false) where T : CommonModel
    {
        var item = await context.Set<T>().Where(x => x.isArchived == includeArchived && x.IsDeleted == includeDeleted).FirstOrDefaultAsync(x => x.Id == Id);

        return item;
    }
    
    public static async Task<T?> GetByIdIncludeArchivedsOnly<T>(this MyDbContext context, string Id) where T : CommonModel
    {
        var item = await context.Set<T>().Where(x => x.Id == Id && !x.IsDeleted).FirstOrDefaultAsync();

        return item;
    }

    public static async Task<List<T>> GetAll<T>(this MyDbContext context, bool? includeArchived = false, bool? includeDeleted = false) where T : CommonModel
    {
        var itemList = await context.Set<T>().Where(x => x.isArchived == includeArchived && x.IsDeleted == includeDeleted).ToListAsync();
        return itemList;
    }
    
    public static async Task<List<T>> GetAllNotDeleted<T>(this MyDbContext context) where T : CommonModel
    {
        var itemList = await context.Set<T>().Where(x => !x.IsDeleted).ToListAsync();
        return itemList;
    }
    
    public static async Task<List<T>> GetAllActive<T>(this MyDbContext context) where T : CommonModel
    {
        var itemList = await context.Set<T>().Where(x => !x.isArchived && !x.IsDeleted).ToListAsync();
        return itemList;
    }

    public static async Task<int> GetCount<T>(this MyDbContext context) where T : class
    {
        return await context.Set<T>().CountAsync();
    }

    public static IQueryable<T> WhereIsNotArchivedAndDeleted<T>(this DbSet<T> dbSet) where T : CommonModel
    {
        return dbSet.Where(x => !x.IsDeleted && !x.isArchived);
    }
}
