using AutoMapper;
using ProEventos.Domain;
using ProEventos.Domain.Interfaces;

namespace ProEventos.Application
{
    public abstract class BaseService<T> : IBaseService<T> where T : Entity
    {
        protected readonly IMapper _mapper;

        public BaseService(IMapper mapper) 
        {
            _mapper = mapper;
        }
    }
}
