using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ProEventos.Domain;

namespace ProEventos.Application
{
    public class LoteService : ILoteService
    {
        private readonly ILoteRepository _loteRepository;
        private readonly IMapper _mapper;

        public LoteService(
            ILoteRepository loteRepository,
            IMapper mapper
        )
        {
            _loteRepository = loteRepository;
            _mapper = mapper;
        }

        public async Task<bool> AddLote(int eventoId, LoteDto model)
        {
            var lote = _mapper.Map<Lote>(model);
            lote.EventoId = eventoId;

            return await _loteRepository.Add(lote);
        }

        public async Task<LoteDto[]> Salvar(int eventoId, LoteDto lote)
        {
            if (lote.Id == 0)
            {
                await AddLote(eventoId, lote);
            }
            else
            {
                var lotes = await _loteRepository
                    .GetLotesByEventoIdAsync(eventoId);
                if (lotes == null) return null;

                var loteEntity = lotes.FirstOrDefault(lote => lote.Id == lote.Id);
                lote.EventoId = eventoId;

                loteEntity = _mapper.Map<Lote>(lote);

                await _loteRepository.Update(loteEntity);
            }

            var loteRetorno = await _loteRepository.GetLotesByEventoIdAsync(eventoId);

            return _mapper.Map<LoteDto[]>(loteRetorno);
        }

        public async Task<bool> Deletar(int eventoId, int loteId)
        {
            var lote = await _loteRepository.GetLoteByIdsAsync(eventoId, loteId);
            if (lote == null) throw new Exception("Lote para delete não encontrado.");

            return await _loteRepository.Delete(lote);
        }

        public async Task<LoteDto[]> GetLotesByEventoIdAsync(int eventoId)
        {
            var lotes = await _loteRepository.GetLotesByEventoIdAsync(eventoId);
            if (lotes == null) return null;

            var resultado = _mapper.Map<LoteDto[]>(lotes);

            return resultado;
        }

        public async Task<LoteDto> GetLoteByIdsAsync(int eventoId, int loteId)
        {
            var lote = await _loteRepository.GetLoteByIdsAsync(eventoId, loteId);
            if (lote == null) return null;

            var resultado = _mapper.Map<LoteDto>(lote);

            return resultado;
        }
    }
}
