using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ProEventos.Domain;

namespace ProEventos.Application
{
    public class PalestranteService : IPalestranteService
    {
        private readonly IPalestranteRepository _palestranteRepository;
        private readonly IMapper _mapper;

        public PalestranteService(
            IPalestranteRepository palestranteRepository,
            IMapper mapper
        )
        {
            _palestranteRepository = palestranteRepository;
            _mapper = mapper;
        }

        public async Task<bool> AddPalestrante(int eventoId, PalestranteDto model)
        {
            var Palestrante = _mapper.Map<Palestrante>(model);
            Palestrante.EventoId = eventoId;

            return await _palestranteRepository.Add(Palestrante);
        }

        public async Task<PalestranteDto> Salvar(int eventoId, PalestranteDto Palestrante)
        {
            if (Palestrante.Id == 0)
            {
                await AddPalestrante(eventoId, Palestrante);
            }
            else
            {
                var Palestrantes = await _palestranteRepository
                    .GetPalestrantesByEventoIdAsync(eventoId);
                if (Palestrantes == null) return null;

                var PalestranteEntity = Palestrantes.FirstOrDefault(Palestrante => Palestrante.Id == Palestrante.Id);
                Palestrante.EventoId = eventoId;

                PalestranteEntity = _mapper.Map<Palestrante>(Palestrante);

                await _palestranteRepository.Update(PalestranteEntity);
            }

            return Palestrante;
        }

        public async Task<bool> Deletar(int eventoId, int PalestranteId)
        {
            var Palestrante = await _palestranteRepository.GetPalestranteByIdsAsync(eventoId, PalestranteId);
            if (Palestrante == null) throw new Exception("Palestrante para delete não encontrado.");

            return await _palestranteRepository.Delete(Palestrante);
        }

        public async Task<PalestranteDto[]> GetPalestrantesByEventoIdAsync(int eventoId)
        {
            var Palestrantes = await _palestranteRepository.GetPalestrantesByEventoIdAsync(eventoId);
            if (Palestrantes == null) return null;

            var resultado = _mapper.Map<PalestranteDto[]>(Palestrantes);

            return resultado;
        }

        public async Task<PalestranteDto> GetPalestranteByIdsAsync(int eventoId, int PalestranteId)
        {
            var Palestrante = await _palestranteRepository.GetPalestranteByIdsAsync(eventoId, PalestranteId);
            if (Palestrante == null) return null;

            var resultado = _mapper.Map<PalestranteDto>(Palestrante);

            return resultado;
        }

        public Task<PalestranteDto> Salvar(PalestranteDto palestrante)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Deletar(int palestranteId)
        {
            throw new NotImplementedException();
        }

        public Task<PalestranteDto[]> GetPalestrantesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PalestranteDto> GetPalestranteByIdAsync(int palestranteId)
        {
            throw new NotImplementedException();
        }
    }
}
