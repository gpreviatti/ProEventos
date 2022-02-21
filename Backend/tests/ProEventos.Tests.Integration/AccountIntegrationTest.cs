using Microsoft.AspNetCore.Http;
using ProEventos.Domain.Dtos;
using ProEventos.Tests.Common.Generators;
using Xunit;

namespace ProEventos.Tests.Integration
{
    public class AccountIntegrationTest : TestFixture
    {
        [Theory]
        [InlineData("admin", "admin@admin.com", "mudar@123")]
        public async void Should_Get_User_By_Name_Async_With_Success(string userName, string email, string password)
        {
            // arrange
            await _apiHelper.AddBearerTokenHeader(userName, email, password);

            // act
            var response = await _apiHelper.GetAsync("account/getUserByNameAsync");
            var result = await _apiHelper.DeserializeResponse<UserDto>(response);

            // assert
            Assert.NotNull(response);
            Assert.Equal(StatusCodes.Status200OK, (int)response.StatusCode);
            Assert.NotNull(result.UserName);
        }

        [Fact(Skip = "Rodar para criar o admin")]
        public async void Should_Register_Admin_With_Success()
        {
            // arrange
            var userCreateDto = new UserCreateDto
            {
                UserName = "admin",
                Email = "admin@admin.com",
                Password = "mudar@123",
                FirstName = "Admin",
                LastName = "System"
            };

            // act
            var response = await _apiHelper.PostAsync("account/registerAsync", userCreateDto);
            var result = await _apiHelper.DeserializeResponse<UserDto>(response);

            // assert
            Assert.NotNull(response);
            Assert.Equal(StatusCodes.Status201Created, (int)response.StatusCode);
            Assert.Equal(userCreateDto.FirstName, result.FirstName);
        }

        [Fact]
        public async void Should_Register_User_Async_With_Success()
        {
            // arrange
            var userCreateDto = DtoGenerator.UserCreateDto.Generate();

            // act
            var response = await _apiHelper.PostAsync("account/registerAsync", userCreateDto);
            var result = await _apiHelper.DeserializeResponse<UserDto> (response);

            // assert
            Assert.NotNull(response);
            Assert.Equal(StatusCodes.Status201Created, (int) response.StatusCode);
            Assert.Equal(userCreateDto.FirstName, result.FirstName);
        }

        [Fact]
        public async void Should_Login_User_Async_With_Success()
        {
            // arrange
            var userLoginDto = new UserLoginDto
            {
                UserName = "admin",
                Email = "admin@admin.com",
                Password = "mudar@123"
            };

            // act
            var response = await _apiHelper.PostAsync("account/loginAsync", userLoginDto);
            var result = await _apiHelper.DeserializeResponse<UserLoginResultDto>(response);

            // assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, (int)response.StatusCode);
            Assert.NotNull(result.Token);
            Assert.Equal(userLoginDto.UserName, result.UserName);
            Assert.NotNull(result.FirstName);
        }

        [Fact]
        public async void Should_Update_User_Async_With_Success()
        {
            // arrange
            var userCreateDto = DtoGenerator.UserCreateDto.Generate();
            var responseCreate = await _apiHelper.PostAsync("account/registerAsync", userCreateDto);
            var resultCreate = await _apiHelper.DeserializeResponse<UserDto>(responseCreate);

            await _apiHelper.AddBearerTokenHeader(userCreateDto.UserName, userCreateDto.Email, userCreateDto.Password);

            var userUpdateDto = new UserUpdateDto
            {
                Id = resultCreate.Id,
                FirstName = "Primeiro Nome Atualizado",
                LastName = "Segundo Nome Atualizado"
            };

            // act
            var response = await _apiHelper.PutAsync("account/updateAsync", userUpdateDto);
            var result = await _apiHelper.DeserializeResponse<UserDto>(response);

            // assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, (int)response.StatusCode);
            Assert.Equal(userUpdateDto.FirstName, result.FirstName);
            Assert.Equal(userUpdateDto.LastName, result.LastName);
            Assert.Equal(userCreateDto.UserName, result.UserName);
            Assert.NotNull(result.FirstName);
        }

        [Fact]
        public async void Should_Delete_User_Async_With_Success()
        {
            // arrange
            var userCreateDto = DtoGenerator.UserCreateDto.Generate();
            await _apiHelper.PostAsync("account/registerAsync", userCreateDto);

            await _apiHelper.AddBearerTokenHeader(userCreateDto.UserName, userCreateDto.Email, userCreateDto.Password);

            // act
            var response = await _apiHelper.DeleteAsync("account/deleteAsync");
            var result = await response.Content.ReadAsStringAsync();

            // assert
            Assert.NotNull(response);
            Assert.Equal(StatusCodes.Status200OK, (int)response.StatusCode);
            Assert.Equal("Usuário removido com sucesso!", result);
        }
    }
}
