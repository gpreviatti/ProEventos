using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ProEventos.Domain;

namespace ProEventos.Application
{
    public class PalestranteService : BaseService<Palestrante>, IPalestranteService
    {
        private readonly IPalestranteRepository _palestranteRepository;

        public PalestranteService(
            IPalestranteRepository palestranteRepository,
            IMapper mapper
        ) : base(mapper)
        {
            _palestranteRepository = palestranteRepository;
        }

        public async Task<PalestranteDto[]> GetPalestrantesAsync() 
        {
            var palestrantes = await _palestranteRepository.GetAllPalestrantesAsync();

            var palestrantesDto = _mapper.Map<IEnumerable<PalestranteDto>>(palestrantes);

            return palestrantesDto.ToArray();
        }

        public async Task<PalestranteDto> GetPalestranteByIdAsync(int palestranteId) 
        {
            var palestrante = await _palestranteRepository.GetPalestranteByIdAsync(palestranteId, false);

            return _mapper.Map<PalestranteDto>(palestrante);
        }

        public async Task<PalestranteDto> SalvarAsync(PalestranteDto palestranteDto)
        {
            var palestrante = new Palestrante();

            if (palestranteDto.Id == 0)
            {
                palestrante = _mapper.Map<Palestrante>(palestranteDto);

                await _palestranteRepository.Add(palestrante);
            }
            else
            {
                palestrante = await _palestranteRepository.GetById(palestranteDto.Id);
                if (palestrante == null) 
                    return null;

                palestrante = _mapper.Map<Palestrante>(palestranteDto);

                await _palestranteRepository.Update(palestrante);
            }

            return _mapper.Map<PalestranteDto>(palestrante);
        }

        public async Task<bool> DeletarAsync(int palestranteId)
        {
            var palestrante = await _palestranteRepository.GetById(palestranteId);
            if (palestrante == null) 
                throw new Exception("Palestrante para encontrado.");

            return await _palestranteRepository.Delete(palestrante);
        }
    }
}