using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Domain.Interfaces;

namespace ProEventos.Persistence.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : Entity
    {
        protected readonly ProEventosContext _context;
        public BaseRepository(ProEventosContext context)
        {
            _context = context;
        }

        public async Task<T> GetByIdAsync(int id) =>
            await _context.Set<T>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);

        public async Task<int> GetAllCountAsync() => 
            await _context.Set<T>()
                .AsNoTracking()
                .CountAsync();

        public async Task<bool> AddAsync(T entity) 
        {
            _context.Add(entity);

            return await SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            _context.Update(entity);

            return await SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(T entity) 
        {
            _context.Remove(entity);

            return await SaveChangesAsync();
        } 

        public async Task<bool> DeleteRangeAsync(T[] entityArray)
        {
            _context.RemoveRange(entityArray);

            return await SaveChangesAsync();
        }

        private async Task<bool> SaveChangesAsync() => 
            (await _context.SaveChangesAsync()) > 0;
    }
}
