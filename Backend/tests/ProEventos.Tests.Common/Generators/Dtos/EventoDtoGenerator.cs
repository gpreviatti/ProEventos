using Bogus;
using ProEventos.Domain.Dtos;
using ProEventos.Domain.Enum;
using System.Collections.Generic;

namespace ProEventos.Tests.Common.Generators.Dtos
{
    public static class EventoDtoGenerator
    {
        public static Faker<EventoDto> Create()
        {
            return new Faker<EventoDto>("pt_BR").StrictMode(false)
                .RuleFor(c => c.Local, f => f.Address.Locale)
                .RuleFor(c => c.Email, f => f.Person.Email)
                .RuleFor(c => c.Email, f => f.Person.Email)
                .RuleFor(c => c.Email, f => f.Person.Email);
        }
    }
}
