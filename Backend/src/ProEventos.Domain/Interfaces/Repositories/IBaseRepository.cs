using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProEventos.Domain.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> Get();
        Task<T> GetByIdAsync(int id);
        Task<int> GetAllCountAsync();
        Task<bool> AddAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
        Task<bool> DeleteRangeAsync(T[] entity);
    }
}
