using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ProEventos.Domain;
using ProEventos.Domain.Dtos;
using ProEventos.Domain.Interfaces;

namespace ProEventos.Application
{
    public class RedeSocialService : BaseService<RedeSocial>, IRedeSocialService
    {
        private readonly IRedeSocialRepository _redeSocialRepository;

        public RedeSocialService(
            IBaseRepository<RedeSocial> baseRepository,
            IRedeSocialRepository redeSocialRepository,
            IMapper mapper
        ) : base(baseRepository, mapper)
        {
            _redeSocialRepository = redeSocialRepository;
        }

        public async Task<RedeSocialDto[]> GetRedesSociaisByPalestranteIdAsync(int palestranteId)
        {
            var redeSociais = await _redeSocialRepository
                .GetRedesSociaisByPalestranteIdAsync(palestranteId);

            var redeSociaisDto = _mapper.Map<IEnumerable<RedeSocialDto>>(redeSociais);

            return redeSociaisDto.ToArray();
        }
    }
}
