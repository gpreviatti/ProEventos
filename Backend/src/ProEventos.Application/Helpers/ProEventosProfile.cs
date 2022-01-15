using AutoMapper;
using ProEventos.Domain;
using ProEventos.Domain.Dtos;
using ProEventos.Domain.Identity;

namespace ProEventos.Application.Helpers
{
    public class ProEventosProfile : Profile
    {
        public ProEventosProfile()
        {
            CreateMap<Evento, EventoDto>().ReverseMap();
            CreateMap<Lote, LoteDto>().ReverseMap();
            CreateMap<RedeSocial, RedeSocialDto>().ReverseMap();
            CreateMap<Palestrante, PalestranteDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserCreateDto>().ReverseMap();
            CreateMap<User, UserUpdateDto>().ReverseMap();
            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<UserRole, UserRoleDto>().ReverseMap();
        }
    }
}
