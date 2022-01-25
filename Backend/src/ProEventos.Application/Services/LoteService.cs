using System.Threading.Tasks;
using AutoMapper;
using ProEventos.Domain;
using ProEventos.Domain.Dtos;
using ProEventos.Domain.Interfaces;

namespace ProEventos.Application
{
    public class LoteService : BaseService<Lote>, ILoteService
    {
        private readonly ILoteRepository _loteRepository;

        public LoteService(
            IBaseRepository<Lote> baseRepository,
            ILoteRepository loteRepository,
            IMapper mapper
        ) : base(baseRepository, mapper)
        {
            _loteRepository = loteRepository;
        }

        public async Task<LoteDto[]> GetLotesByEventoIdAsync(int eventoId)
        {
            var lotes = await _loteRepository.GetByEventoIdAsync(eventoId);
            if (lotes == null) return null;

            var resultado = _mapper.Map<LoteDto[]>(lotes);

            return resultado;
        }
    }
}
