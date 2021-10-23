using System.Threading.Tasks;

namespace ProEventos.Domain
{
    public interface IPalestranteService
    {
        Task<PalestranteDto[]> GetPalestrantesAsync();
        Task<PalestranteDto> GetPalestranteByIdAsync(int palestranteId);
        Task<PalestranteDto> Salvar(PalestranteDto palestrante);
        Task<bool> Deletar(int palestranteId);
    }
}
