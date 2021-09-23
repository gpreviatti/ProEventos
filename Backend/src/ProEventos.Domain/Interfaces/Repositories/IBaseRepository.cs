using System.Threading.Tasks;

namespace ProEventos.Domain
{
    public interface IBaseRepository<T> where T : class
    {
        Task Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void DeleteRange(T[] entity);
        Task<bool> SaveChangesAsync();
    }
}
