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
        public  async Task<T> Create(T item)
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
    }
}
