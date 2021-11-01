using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ProEventos.Domain;
using ProEventos.Domain.Messages;

namespace ProEventos.Application
{
    public class EventoService : BaseService<Evento>, IEventoService
    {
        private readonly IEventoRespository _eventoRepository;
        public EventoService(
            IEventoRespository eventoRepository,
            IMapper mapper
        ) : base(mapper)
        {
            _eventoRepository = eventoRepository;
        }

        public async Task<EventoDto> SalvarAsync(EventoDto eventoDto)
        {
            var evento = new Evento();

            if (eventoDto.Id == 0)
            {
                evento = _mapper.Map<Evento>(eventoDto);

                await _eventoRepository.Add(evento);
            }
            else
            {
                evento = await _eventoRepository.GetById(eventoDto.Id);
                if (evento == null) 
                    return null;

                evento = _mapper.Map<Evento>(eventoDto);

                await _eventoRepository.Update(evento);
            }

            return _mapper.Map<EventoDto>(evento);
        }

        public async Task<bool> DeleteEvento(int eventoId)
        {
            var evento = await _eventoRepository.GetByIdAsync(eventoId, false);
            if (evento == null) throw new Exception("Evento para delete não encontrado.");

            return await _eventoRepository.Delete(evento);
        }

        public async Task<EventoDto[]> GetAllEventosAsync()
        {
            var eventos = await _eventoRepository.GetAllAsync();
            if (eventos == null) return null;

            var resultado = _mapper.Map<EventoDto[]>(eventos);

            return resultado;
        }

        public async Task<PaginatedListResponse<EventoDto>> GetAllEventosPaginatedAsync(int skip, int take)
        {
            var eventos = await _eventoRepository.GetAllPaginatedAsync(skip, take);
            if (eventos == null) return null;

            var total = await _eventoRepository.GetAllAsync();

            var resultado = _mapper.Map<EventoDto[]>(eventos);

            return new PaginatedListResponse<EventoDto>{
                Valid = true,
                RecordsTotal = total.Count(),
                RecordsFiltered = resultado.Count(),
                Skip = skip,
                Take = take,
                Data = resultado
            };
        }

        public async Task<EventoDto[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            var eventos = await _eventoRepository.GetAllByTemaAsync(tema, includePalestrantes);
            if (eventos == null) return null;

            var resultado = _mapper.Map<EventoDto[]>(eventos);

            return resultado;
        }

        public async Task<EventoDto> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false)
        {
            var evento = await _eventoRepository.GetByIdAsync(eventoId, includePalestrantes);
            if (evento == null) return null;

            var resultado = _mapper.Map<EventoDto>(evento);

            return resultado;
        }
    }
}
