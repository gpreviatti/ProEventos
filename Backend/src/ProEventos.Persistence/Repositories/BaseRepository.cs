using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Domain.Interfaces;

namespace ProEventos.Persistence
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : Entity
    {
        protected readonly ProEventosContext _context;
        public BaseRepository(ProEventosContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> Get() => await _context
            .Set<T>()
            .AsNoTracking()
            .OrderBy(e => e.Id)
            .ToArrayAsync();

        public async Task<int> GetAllCountAsync() => await _context
            .Set<T>()
            .AsNoTracking()
            .CountAsync();

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.
                Set<T>()
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> AddAsync(T entity) 
        {
            await _context.AddAsync(entity);

            return await SaveChangesAsync();
        } 
            

        public async Task<bool> UpdateAsync(T entity) 
        {
            await Task.Run(() => _context.Update(entity));

            return await SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(T entity) 
        {
            await Task.Run(() =>_context.Remove(entity));

            return await SaveChangesAsync();
        } 

        public async Task<bool> DeleteRangeAsync(T[] entityArray)
        {
            await Task.Run(() => _context.RemoveRange(entityArray));

            return await SaveChangesAsync();
        } 


        private async Task<bool> SaveChangesAsync() => await _context.SaveChangesAsync() > 0;
    }
}
