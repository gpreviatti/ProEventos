using System;
using System.Threading.Tasks;
using AutoMapper;
using ProEventos.Domain;

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

        public async Task<EventoDto> AddEventos(EventoDto model)
        {
            var evento = _mapper.Map<Evento>(model);

            var result = await _eventoRepository.Add(evento);

            if (result)
            {
                var eventoRetorno = await _eventoRepository.GetEventoByIdAsync(evento.Id, true);

                return _mapper.Map<EventoDto>(eventoRetorno);
            }
            return null;
        }

        public async Task<EventoDto> UpdateEvento(int eventoId, EventoDto model)
        {
            var evento = await _eventoRepository.GetEventoByIdAsync(eventoId, false);
            if (evento == null) return null;

            model.Id = evento.Id;

            _mapper.Map(model, evento);

            var result = await _eventoRepository.Update(evento);

            if (result)
            {
                var eventoRetorno = await _eventoRepository.GetEventoByIdAsync(evento.Id, false);

                return _mapper.Map<EventoDto>(eventoRetorno);
            }
            return null;
        }

        public async Task<bool> DeleteEvento(int eventoId)
        {
            var evento = await _eventoRepository.GetEventoByIdAsync(eventoId, false);
            if (evento == null) throw new Exception("Evento para delete não encontrado.");

            return await _eventoRepository.Delete(evento);
        }

        public async Task<EventoDto[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            var eventos = await _eventoRepository.GetAllEventosAsync(includePalestrantes);
            if (eventos == null) return null;

            var resultado = _mapper.Map<EventoDto[]>(eventos);

            return resultado;
        }

        public async Task<EventoDto[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            var eventos = await _eventoRepository.GetAllEventosByTemaAsync(tema, includePalestrantes);
            if (eventos == null) return null;

            var resultado = _mapper.Map<EventoDto[]>(eventos);

            return resultado;
        }

        public async Task<EventoDto> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false)
        {
            var evento = await _eventoRepository.GetEventoByIdAsync(eventoId, includePalestrantes);
            if (evento == null) return null;

            var resultado = _mapper.Map<EventoDto>(evento);

            return resultado;
        }
    }
}
