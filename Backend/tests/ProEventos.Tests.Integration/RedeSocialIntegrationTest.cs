using Microsoft.AspNetCore.Http;
using ProEventos.Domain.Dtos;
using ProEventos.Tests.Common.Generators;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ProEventos.Tests.Integration
{
    public class RedeSocialIntegrationTest : TestFixture
    {
        const string RESOURCE_URL = "redeSocial";
        public RedeSocialIntegrationTest()
        {
            _apiHelper
                .AddBearerTokenHeader("admin", "admin@admin.com", "mudar@123")
                .GetAwaiter()
                .GetResult();
        }

        [Fact]
        public async void Should_Get_RedeSocial_By_Id_Async_With_Success()
        {
            // arrange
            var dto = DtoGenerator.RedeSocialDto.Generate();
            var resultCreatePalestrante = await CreatePalestrante();
            dto.PalestranteId = resultCreatePalestrante.Id;
            var responseCreate =  await _apiHelper.PostAsync(RESOURCE_URL, dto);
            var resultCreate = await _apiHelper.DeserializeResponse<RedeSocialDto>(responseCreate);

            // act
            var response = await _apiHelper.GetAsync(RESOURCE_URL + $"/{resultCreate.Id}");
            var result = await _apiHelper.DeserializeResponse<RedeSocialDto>(response);

            // assert
            Assert.NotNull(response);
            Assert.Equal(StatusCodes.Status200OK, (int)response.StatusCode);
            Assert.True(result.Id != default);
        }

        [Fact]
        public async void Should_Get_RedeSocial_By_Palestrante_Id_Async_With_Success()
        {
            // arrange
            var dto = DtoGenerator.RedeSocialDto.Generate();
            var resultCreatePalestrante = await CreatePalestrante();
            dto.PalestranteId = resultCreatePalestrante.Id;
            await _apiHelper.PostAsync(RESOURCE_URL , dto);

            // act
            var response = await _apiHelper.GetAsync(RESOURCE_URL + $"/palestrante/{resultCreatePalestrante.Id}");
            var result = await _apiHelper.DeserializeResponse<IEnumerable<RedeSocialDto>>(response);

            // assert
            Assert.NotNull(response);
            Assert.Equal(StatusCodes.Status200OK, (int)response.StatusCode);
            Assert.True(result.Any());
        }

        [Fact]
        public async void Should_Create_RedeSocial_Async_With_Success()
        {
            // arrange
            var dto = DtoGenerator.RedeSocialDto.Generate();
            var resultCreatePalestrante = await CreatePalestrante();
            dto.PalestranteId = resultCreatePalestrante.Id;

            // act
            var response = await _apiHelper.PostAsync(RESOURCE_URL, dto);

            // assert
            Assert.NotNull(response);
            Assert.Equal(StatusCodes.Status201Created, (int)response.StatusCode);
        }

        [Fact]
        public async void Should_Update_RedeSocial_Async_With_Success()
        {
            // arrange
            var dto = DtoGenerator.RedeSocialDto.Generate();
            var resultCreatePalestrante = await CreatePalestrante();
            dto.PalestranteId = resultCreatePalestrante.Id;
            var responseCreate = await _apiHelper.PostAsync(RESOURCE_URL, dto);
            var resultCreate = await _apiHelper.DeserializeResponse<RedeSocialDto>(responseCreate);

            resultCreate.Nome = "Twitter";

            // act
            var response = await _apiHelper.PostAsync(RESOURCE_URL, resultCreate);
            var result = await _apiHelper.DeserializeResponse<RedeSocialDto>(response);

            // assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status201Created, (int)response.StatusCode);
            Assert.Equal(resultCreate.Nome, result.Nome);
        }

        [Fact]
        public async void Should_Delete_RedeSocial_Async_With_Success()
        {
            // arrange
            var dto = DtoGenerator.RedeSocialDto.Generate();
            var resultCreatePalestrante = await CreatePalestrante();
            dto.PalestranteId = resultCreatePalestrante.Id;

            var responseCreate = await _apiHelper.PostAsync(RESOURCE_URL, dto);
            var resultCreate = await _apiHelper.DeserializeResponse<RedeSocialDto>(responseCreate);

            // act
            var response = await _apiHelper.DeleteAsync($"{RESOURCE_URL}/{resultCreate.Id}");
            var result = await response.Content.ReadAsStringAsync();

            // assert
            Assert.NotNull(response);
            Assert.Equal(StatusCodes.Status200OK, (int)response.StatusCode);
            Assert.Contains("RedeSocial removido com sucesso!", result);
        }
    }
}
