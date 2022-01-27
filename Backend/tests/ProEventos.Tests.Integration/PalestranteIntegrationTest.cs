using Microsoft.AspNetCore.Http;
using ProEventos.Domain.Dtos;
using ProEventos.Tests.Common.Generators;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ProEventos.Tests.Integration
{
    public class PalestranteIntegrationTest : TestFixture
    {
        const string RESOURCE_URL = "palestrantes";
        public PalestranteIntegrationTest()
        {
            _apiHelper
                .AddBearerTokenHeader("admin", "admin@admin.com", "mudar@123")
                .GetAwaiter()
                .GetResult();
        }

        [Fact]
        public async void Should_Get_Palestrante_Async_With_Success()
        {
            // arrange
            var dto = DtoGenerator.PalestranteDto.Generate();
            await _apiHelper.PostAsync(RESOURCE_URL, dto);

            // act
            var response = await _apiHelper.GetAsync(RESOURCE_URL);
            var result = await _apiHelper.DeserializeResponse<IEnumerable<PalestranteDto>>(response);

            // assert
            Assert.NotNull(response);
            Assert.Equal(StatusCodes.Status200OK, (int)response.StatusCode);
            Assert.True(result.Any());
        }

        [Fact]
        public async void Should_Get_Palestrante_Paginated_Async_With_Success()
        {
            // arrange
            var dto = DtoGenerator.PalestranteDto.Generate();
            await _apiHelper.PostAsync(RESOURCE_URL, dto);

            // act
            var response = await _apiHelper.GetAsync(RESOURCE_URL);
            var result = await _apiHelper.DeserializeResponse<IEnumerable<PalestranteDto>>(response);

            // assert
            Assert.NotNull(response);
            Assert.Equal(StatusCodes.Status200OK, (int)response.StatusCode);
            Assert.True(result.Any());
        }

        [Fact]
        public async void Should_Get_Palestrante_By_Id_Async_With_Success()
        {
            // arrange
            var dto = DtoGenerator.PalestranteDto.Generate();
            await _apiHelper.PostAsync(RESOURCE_URL, dto);

            // act
            var response = await _apiHelper.GetAsync(RESOURCE_URL);
            var result = await _apiHelper.DeserializeResponse<IEnumerable<PalestranteDto>>(response);

            // assert
            Assert.NotNull(response);
            Assert.Equal(StatusCodes.Status200OK, (int)response.StatusCode);
            Assert.True(result.Any());
        }

        [Fact]
        public async void Should_Create_Palestrante_Async_With_Success()
        {
            // arrange
            var dto = DtoGenerator.PalestranteDto.Generate();

            // act
            var response = await _apiHelper.PostAsync(RESOURCE_URL, dto);

            // assert
            Assert.NotNull(response);
            Assert.Equal(StatusCodes.Status201Created, (int)response.StatusCode);
        }

        [Fact]
        public async void Should_Update_Palestrante_Async_With_Success()
        {
            // arrange
            var dto = DtoGenerator.PalestranteDto.Generate();
            var responseCreate = await _apiHelper.PostAsync(RESOURCE_URL, dto);
            var resultCreate = await _apiHelper.DeserializeResponse<PalestranteDto>(responseCreate);

            resultCreate.Nome = "Joaquim da Silva";
            resultCreate.Telefone = "(33) 5555-55555";

            // act
            var response = await _apiHelper.PostAsync(RESOURCE_URL, resultCreate);
            var result = await _apiHelper.DeserializeResponse<PalestranteDto>(response);

            // assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status201Created, (int)response.StatusCode);
            Assert.Equal(resultCreate.Nome, result.Nome);
            Assert.Equal(resultCreate.Telefone, result.Telefone);
        }

        [Fact]
        public async void Should_Delete_Palestrante_Async_With_Success()
        {
            // arrange
            var dto = DtoGenerator.PalestranteDto.Generate();
            var responseCreate = await _apiHelper.PostAsync(RESOURCE_URL, dto);
            var resultCreate = await _apiHelper.DeserializeResponse<PalestranteDto>(responseCreate);

            // act
            var response = await _apiHelper.DeleteAsync($"eventos/{resultCreate.Id}");
            var result = await response.Content.ReadAsStringAsync();

            // assert
            Assert.NotNull(response);
            Assert.Equal(StatusCodes.Status200OK, (int)response.StatusCode);
            Assert.Contains("Evento removido com sucesso!", result);
        }
    }
}
