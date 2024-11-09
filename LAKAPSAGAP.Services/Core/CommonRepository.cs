using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAKAPSAGAP.Services.Core
{
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

    public static class DbSetExtensions{
        public static async Task<T> Create<T>(this MyDbContext context, T item) where T: class
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

        public static async Task<T?> Delete<T>(this MyDbContext context, T item) where T : class
        {
            context.Set<T>().Remove(item);
            await context.SaveChangesAsync();
            return item;
        }

        public static async Task<T?> GetById<T>(this MyDbContext context, string Id) where T : class
        {
            var item = await context.Set<T>().FindAsync(Id);
            return item;
        }

        public static async Task<List<T>> GetAll<T>(this MyDbContext context) where T : class
        {
            var itemList = await context.Set<T>().ToListAsync();
            return itemList;
        }

        public static async Task<int> GetCount<T>(this MyDbContext context) where T : class
        {
            return await context.Set<T>().CountAsync();
        }
    }
}
