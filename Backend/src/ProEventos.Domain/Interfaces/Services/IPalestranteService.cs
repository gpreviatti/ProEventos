using System.Threading.Tasks;

namespace ProEventos.Domain
{
    public interface IPalestranteService
    {
        Task<PalestranteDto[]> GetPalestrantesAsync();
        Task<PalestranteDto> GetPalestranteByIdAsync(int palestranteId);
        Task<PalestranteDto> SalvarAsync(PalestranteDto palestrante);
        Task<bool> DeletarAsync(int palestranteId);
    }
}
