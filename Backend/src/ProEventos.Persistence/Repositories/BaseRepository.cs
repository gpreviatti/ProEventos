using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Persistence
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly ProEventosContext _context;
        public BaseRepository(ProEventosContext context)
        {
            _context = context;
        }

        public async Task Add(T entity) => await _context.AddAsync(entity);

        public void Update(T entity) => _context.Update(entity);

        public void Delete(T entity) => _context.Remove(entity);

        public void DeleteRange(T[] entityArray) => _context.RemoveRange(entityArray);

        public async Task<bool> SaveChangesAsync() => await _context.SaveChangesAsync() > 0;

    }
}
