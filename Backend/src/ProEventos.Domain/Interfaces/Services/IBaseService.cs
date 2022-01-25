using ProEventos.Domain.Dtos;
using System.Threading.Tasks;

namespace ProEventos.Domain.Interfaces
{
    public interface IBaseService<T> where T : Entity
    {
        Task<D> GetByIdAsync<D>(int id) where D : EntityDto;
        Task<D> SalvarAsync<D>(D dto) where D : EntityDto;
        Task<bool> DeletarAsync(int id);
    }
}
