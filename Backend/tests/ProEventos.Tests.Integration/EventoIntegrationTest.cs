using Microsoft.AspNetCore.Http;
using ProEventos.Domain.Dtos;
using ProEventos.Tests.Common.Generators;
using System;
using Xunit;

namespace ProEventos.Tests.Integration
{
    public class EventoIntegrationTest : TestFixture
    {
        const string RESOURCE_URL = "eventos";
        public EventoIntegrationTest()
        {
            _apiHelper
                .AddBearerTokenHeader("admin", "admin@admin.com", "mudar@123")
                .GetAwaiter()
                .GetResult();
        }

        [Fact]
        public async void Should_Get_Evento_Async_With_Success()
        {
            // arrange
            await _apiHelper.PostAsync(RESOURCE_URL, DtoGenerator.EventoDto.Generate());

            // act
            var response = await _apiHelper.GetAsync(RESOURCE_URL);
            var result = await _apiHelper.DeserializeResponse<EventoDto>(response);

            // assert
            Assert.NotNull(response);
            Assert.Equal(StatusCodes.Status200OK, (int)response.StatusCode);
        }

        [Fact]
        public async void Should_Create_Evento_Async_With_Success()
        {
            // arrange
            var eventoDto = DtoGenerator.EventoDto.Generate();

            // act
            var response = await _apiHelper.PostAsync(RESOURCE_URL, eventoDto);

            // assert
            Assert.NotNull(response);
            Assert.Equal(StatusCodes.Status201Created, (int) response.StatusCode);
        }

        [Fact]
        public async void Should_Update_Evento_Async_With_Success()
        {
            // arrange
            var eventoCreateDto = DtoGenerator.EventoDto.Generate();
            var responseCreate = await _apiHelper.PostAsync(RESOURCE_URL, eventoCreateDto);
            var resultCreate = await _apiHelper.DeserializeResponse<EventoDto>(responseCreate);

            var userUpdateDto = new EventoDto
            {
                Id = resultCreate.Id,
                Tema = "Sustentabilidade",
                DataEvento = DateTime.Now.ToString()
            };

            // act
            var response = await _apiHelper.PutAsync("Eventos", userUpdateDto);
            var result = await _apiHelper.DeserializeResponse<EventoDto>(response);

            // assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, (int)response.StatusCode);
            Assert.Equal(userUpdateDto.Tema, result.Tema);
            Assert.Equal(userUpdateDto.DataEvento, result.DataEvento);
        }

        [Fact]
        public async void Should_Delete_Evento_Async_With_Success()
        {
            // arrange
            var eventoCreateDto = DtoGenerator.EventoDto.Generate();
            await _apiHelper.PostAsync(RESOURCE_URL, eventoCreateDto);

            // act
            var response = await _apiHelper.DeleteAsync("account/deleteAsync");
            var result = await response.Content.ReadAsStringAsync();

            // assert
            Assert.NotNull(response);
            Assert.Equal(StatusCodes.Status200OK, (int)response.StatusCode);
            Assert.Equal("Evento removido com sucesso!", result);
        }
    }
}
