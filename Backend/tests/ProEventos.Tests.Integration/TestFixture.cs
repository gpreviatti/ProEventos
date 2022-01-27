using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using ProEventos.API;
using ProEventos.Tests.Integration.Helpers;
using System.IO;

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

            var builder = new WebHostBuilder()
               .UseEnvironment(_environment)
               .UseConfiguration(configuration)
               .UseStartup<Startup>();

            var server = new TestServer(builder);

            _apiHelper = new ApiHelper(_apiUrl, server.CreateClient());
        }
    }
}
