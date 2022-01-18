using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ProEventos.API;
using ProEventos.Domain.Dtos;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Tests.Integration
{
    public abstract class TestFixture
    {
        protected static HttpClient _client;
        protected static string _hostApi;
        private readonly string _environment = "Development";

        public TestFixture()
        {
            _hostApi = "http://localhost/";

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.{_environment}.json")
                .Build();

            var builder = new WebHostBuilder()
               .UseEnvironment(_environment)
               .UseConfiguration(configuration)
               .UseStartup<Startup>();

            var server = new TestServer(builder);

            _client = server.CreateClient();
        }

        #region "Set Api Communication"
        public async Task AdicionarToken()
        {
            var loginDto = new UserLoginDto()
            {
                Email = "admin@admin.com",
                Password = "mudar@123"
            };

            var resultLogin = await PostAsync<UserLoginResultDto>(loginDto, "account/loginAsync");

            //Add default authorization in each request
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer",
                resultLogin.Token
            );
        }

        public static async Task<HttpResponseMessage> GetAsync(string url) => await _client.GetAsync(_hostApi + url);

        public static async Task<T> PostAsync<T>(object dataclass, string url)
        {
            var response = await _client.PostAsync(
                _hostApi + url,
                new StringContent(JsonConvert.SerializeObject(dataclass), Encoding.UTF8, "application/json")
            );
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(result);
        }

        public static async Task<HttpResponseMessage> PutAsync(object dataclass, string url)
        {
            return await _client.PutAsync(
                _hostApi + url,
                new StringContent(JsonConvert.SerializeObject(dataclass), Encoding.UTF8, "application/json")
            );
        }

        public static async Task<HttpResponseMessage> DeleteAsync(string url) => await _client.DeleteAsync(_hostApi + url);

        #endregion
    }
}
