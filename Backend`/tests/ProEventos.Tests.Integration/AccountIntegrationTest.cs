using ProEventos.Domain.Dtos;
using ProEventos.Tests.Common.Generators.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ProEventos.Tests.Integration
{
    public class AccountIntegrationTest : TestFixture
    {
        [Fact]
        public async void Should_Register_User_Async_With_Success()
        {
            // arrange
            var userCreateDto = UserCreateDtoGenerator.Create().Generate();

            // act
            var result = await PostAsync<UserDto>(userCreateDto, "account/registerAsync");

            // assert
            Assert.NotNull(result);
            Assert.NotEqual(default, userCreateDto);
        }

        [Fact]
        public async void Should_Login_User_Async_With_Success()
        {
            // arrange
            var userCreateDto = UserCreateDtoGenerator.Create().Generate();
            var resultCreate = await PostAsync<UserDto>(userCreateDto, "account/registerAsync");

            // act
            var result = await PostAsync<UserLoginResultDto>(resultCreate, "account/loginAsync");

            // assert
            Assert.NotNull(result);
            Assert.NotEqual(default, userCreateDto);
        }
    }
}
