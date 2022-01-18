using Bogus;
using ProEventos.Domain.Dtos;
using ProEventos.Domain.Enum;
using System.Collections.Generic;

namespace ProEventos.Tests.Common.Generators.Dtos
{
    public static class UserCreateDtoGenerator
    {
        public static Faker<UserCreateDto> Create()
        {
            return new Faker<UserCreateDto>("pt_BR").StrictMode(false)
                .RuleFor(c => c.UserName, f => f.Internet.UserName())
                .RuleFor(c => c.Password, f => f.Internet.Password())
                .RuleFor(c => c.Email, f => f.Person.Email)
                .RuleFor(c => c.FirstName, f => f.Person.FirstName)
                .RuleFor(c => c.LastName, f => f.Person.LastName)
                .RuleFor(c => c.PhoneNumber, f => f.Person.Phone)
                .RuleFor(c => c.ImageUrl, f => f.Person.Avatar)
                .RuleFor(c => c.Title, f => f.Random.ListItem(new List<Titulo>() { Titulo.Bacharel, Titulo.Tecnologo, Titulo.PosDoutorado}))
                .RuleFor(c => c.Function, f => f.Random.ListItem(new List<Funcao>() { Funcao.Palestrante, Funcao.Participante, Funcao.NaoInformado }))
                .RuleFor(c => c.Email, f => f.Person.Email);
        }
    }
}
