using System.Threading.Tasks;

namespace ProEventos.Domain
{
    public interface IBaseRepository<T> where T : class
    {
        Task<bool> Add(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(T entity);
        Task<bool> DeleteRange(T[] entity);
    }
}
