using Bogus;
using ProEventos.Domain.Dtos;
using ProEventos.Domain.Enum;
using System.Collections.Generic;

namespace ProEventos.Tests.Common.Generators
{
    public static class DtoGenerator
    {
        public static Faker<UserCreateDto> UserCreateDto { get; set; } = new Faker<UserCreateDto>("pt_BR")
                .StrictMode(false)
                .RuleFor(c => c.UserName, f => f.Internet.UserName())
                .RuleFor(c => c.Password, f => f.Internet.Password())
                .RuleFor(c => c.Email, f => f.Person.Email)
                .RuleFor(c => c.FirstName, f => f.Person.FirstName)
                .RuleFor(c => c.LastName, f => f.Person.LastName)
                .RuleFor(c => c.PhoneNumber, f => f.Person.Phone)
                .RuleFor(c => c.ImageUrl, f => f.Person.Avatar)
                .RuleFor(c => c.Title, f => f.Random.ListItem(new List<Titulo>() { Titulo.Bacharel, Titulo.Tecnologo, Titulo.PosDoutorado }))
                .RuleFor(c => c.Function, f => f.Random.ListItem(new List<Funcao>() { Funcao.Palestrante, Funcao.Participante, Funcao.NaoInformado }))
                .RuleFor(c => c.Email, f => f.Person.Email);

        public static Faker<EventoDto> EventoDto { get; set; } = new Faker<EventoDto>("pt_BR")
                .StrictMode(false)
                .RuleFor(c => c.Id, f => f.IndexFaker)
                .RuleFor(c => c.Local, f => f.Address.Locale)
                .RuleFor(c => c.Email, f => f.Person.Email)
                .RuleFor(c => c.DataEvento, f => f.Date.Recent().ToString())
                .RuleFor(c => c.QtdPessoas, f => f.Random.Int(1, 1000))
                .RuleFor(c => c.Tema, f => f.Name.JobTitle())
                .RuleFor(c => c.Telefone, f => f.Person.Phone)
                .RuleFor(c => c.ImagemURL, f => f.Image.LoremFlickrUrl() + ".jpg");

        public static Faker<RedeSocialDto> RedeSocialDto { get; set; } = new Faker<RedeSocialDto>("pt_BR")
                .StrictMode(false)
                .RuleFor(c => c.Id, f => f.IndexFaker)
                .RuleFor(c => c.Nome, f => f.Random.ListItem(new List<string> { "Facebook", "Instagram", "Linkedin", "Orkut" }))
                .RuleFor(c => c.URL, f => f.Internet.Url())
                .RuleFor(c => c.PalestranteId, f => f.IndexFaker);

        public static Faker<PalestranteDto> PalestranteDto { get; set; } = new Faker<PalestranteDto>("pt_BR")
                .StrictMode(false)
                .RuleFor(c => c.Id, f => f.IndexFaker)
                .RuleFor(c => c.Nome, f => f.Person.FullName)
                .RuleFor(c => c.Email, f => f.Person.Email)
                .RuleFor(c => c.ImagemURL, f => f.Internet.Avatar())
                .RuleFor(c => c.Telefone, f => f.Person.Phone)
                .RuleFor(c => c.MiniCurriculo, f => f.Name.JobDescriptor())
                .RuleFor(c => c.RedesSociais, f => RedeSocialDto.Generate(3));
    }
}
