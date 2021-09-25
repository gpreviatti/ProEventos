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

        public async Task<bool> Add(T entity) 
        {
            await _context.AddAsync(entity);

            return await SaveChangesAsync();
        } 
            

        public async Task<bool> Update(T entity) 
        {
            await Task.Run(() => _context.Update(entity));

            return await SaveChangesAsync();
        }

        public async Task<bool> Delete(T entity) 
        {
            await Task.Run(() =>_context.Remove(entity));

            return await SaveChangesAsync();
        } 

        public async Task<bool> DeleteRange(T[] entityArray)
        {
            await Task.Run(() => _context.RemoveRange(entityArray));

            return await SaveChangesAsync();
        } 


        private async Task<bool> SaveChangesAsync() => await _context.SaveChangesAsync() > 0;

    }
}
