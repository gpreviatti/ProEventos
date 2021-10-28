using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ProEventos.Domain;

namespace ProEventos.Application
{
    public class LoteService : BaseService<Lote>, ILoteService
    {
        private readonly ILoteRepository _loteRepository;

        public LoteService(
            ILoteRepository loteRepository,
            IMapper mapper
        ) : base(mapper)
        {
            _loteRepository = loteRepository;
        }

        public async Task<LoteDto> Salvar(LoteDto loteDto)
        {
            if (loteDto.Id == 0)
            {
                var loteEntity = _mapper.Map<Lote>(loteDto);
                loteEntity.EventoId = loteDto.EventoId;

                await _loteRepository.Add(loteEntity);
            }
            else
            {
                var lotes = await _loteRepository
                    .GetLotesByEventoIdAsync(loteDto.EventoId);
                if (lotes == null) return null;

                var loteEntity = lotes.FirstOrDefault(lote => lote.Id == lote.Id);
                loteEntity.EventoId = loteDto.EventoId;

                loteEntity = _mapper.Map<Lote>(loteDto);

                await _loteRepository.Update(loteEntity);
            }

            return _mapper.Map<LoteDto>(loteDto);
        }

        public async Task<bool> Deletar(int id)
        {
            var lote = await _loteRepository.GetById(id);
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

        public async Task<LoteDto> GetLoteByIdsAsync(int loteId)
        {
            var lote = await _loteRepository.GetById(loteId);
            if (lote == null) return null;

            var resultado = _mapper.Map<LoteDto>(lote);

            return resultado;
        }
    }
}
