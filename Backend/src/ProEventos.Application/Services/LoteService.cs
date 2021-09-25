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

        public async Task<LoteDto[]> SaveLotes(int eventoId, LoteDto[] models)
        {
            var lotes = await _loteRepository.GetLotesByEventoIdAsync(eventoId);
            if (lotes == null) return null;

            foreach (var model in models)
            {
                if (model.Id == 0)
                {
                    await AddLote(eventoId, model);
                }
                else
                {
                    var lote = lotes.FirstOrDefault(lote => lote.Id == model.Id);
                    model.EventoId = eventoId;

                    _mapper.Map(model, lote);

                    await _loteRepository.Update(lote);
                }
            }

            var loteRetorno = await _loteRepository.GetLotesByEventoIdAsync(eventoId);

            return _mapper.Map<LoteDto[]>(loteRetorno);
        }

        public async Task<bool> DeleteLote(int eventoId, int loteId)
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
