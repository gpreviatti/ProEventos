using System.Threading.Tasks;

namespace ProEventos.Domain
{
    public interface IPalestranteService
    {
        Task<PalestranteDto> Salvar(PalestranteDto palestrante);
        Task<bool> Deletar(int palestranteId);
        Task<PalestranteDto[]> GetPalestrantesAsync();
        Task<PalestranteDto> GetPalestranteByIdAsync(int palestranteId);
    }
}
