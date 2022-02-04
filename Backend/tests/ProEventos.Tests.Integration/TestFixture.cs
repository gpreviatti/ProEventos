using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using ProEventos.API;
using ProEventos.Domain.Dtos;
using ProEventos.Tests.Common.Generators;
using ProEventos.Tests.Integration.Helpers;
using System.IO;
using System.Threading.Tasks;
using System;

namespace ProEventos.Tests.Integration
{
    public abstract class TestFixture
    {
        protected ApiHelper _apiHelper;
        private readonly string _environment = "Development";
        private readonly string _apiUrl = "http://localhost/";

        public TestFixture()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.{_environment}.json")
                .Build();

            Environment
            .SetEnvironmentVariable("DB_CONNECTION", "Host=localhost;Port=5432;Database=ProEventos;Username=postgres;Password=admin");

            var builder = new WebHostBuilder()
               .UseEnvironment(_environment)
               .UseConfiguration(configuration)
               .UseStartup<Startup>();

            var server = new TestServer(builder);

            _apiHelper = new ApiHelper(_apiUrl, server.CreateClient());
        }

        public async Task<PalestranteDto> CreatePalestrante()
        {
            var dto = DtoGenerator.PalestranteDto.Generate();
            dto.RedesSociais = null;
            var responseCreate = await _apiHelper.PostAsync("palestrantes", dto);
            return await _apiHelper.DeserializeResponse<PalestranteDto>(responseCreate);
        }
    }
}
