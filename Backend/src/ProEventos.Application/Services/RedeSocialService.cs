using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ProEventos.Domain;

namespace ProEventos.Application
{
    public class RedeSocialService : IRedeSocialService
    {
        private readonly IRedeSocialRepository _redeSocialRepository;
        private readonly IMapper _mapper;

        public RedeSocialService(
            IRedeSocialRepository redeSocialRepository,
            IMapper mapper
        )
        {
            _redeSocialRepository = redeSocialRepository;
            _mapper = mapper;
        }

        public async Task<RedeSocialDto[]> GetRedesSociaisByPalestranteIdAsync(int palestranteId, int redeSocialId)
        {
            var redeSociais = await _redeSocialRepository
                .GetRedesSociaisByPalestranteIdAsync(palestranteId, redeSocialId);

            var redeSociaisDto = _mapper.Map<IEnumerable<RedeSocialDto>>(redeSociais);

            return redeSociaisDto.ToArray();
        }

        public async Task<RedeSocialDto> GetRedeSocialByIdAsync(int redeSocialId)
        {
            var redeSocial = await _redeSocialRepository
                .GetRedeSocialByIdAsync(redeSocialId);

            return _mapper.Map<RedeSocialDto>(redeSocial);
        }

        public async Task<RedeSocialDto> Salvar(int palestranteId, RedeSocialDto redeSocialDto)
        {
            if (redeSocialDto.Id == 0)
            {
                var redeSocialModel = _mapper.Map<RedeSocial>(redeSocialDto);
                redeSocialModel.PalestranteId = palestranteId;

                await _redeSocialRepository.Add(redeSocialModel);
            }
            else
            {
                var redeSocialEntity = await _redeSocialRepository.GetById(redeSocialDto.Id);
                if (redeSocialEntity == null) return null;

                redeSocialEntity.PalestranteId = palestranteId;

                redeSocialEntity = _mapper.Map<RedeSocial>(redeSocialDto);

                await _redeSocialRepository.Update(redeSocialEntity);
            }

            return redeSocialDto;
        }

        public async Task<bool> Deletar(int id)
        {
            var redeSocial = await _redeSocialRepository.GetById(id);

            if (redeSocial == null) 
                throw new Exception("Rede social não encontrada.");

            return await _redeSocialRepository.Delete(redeSocial);
        }
    }
}
