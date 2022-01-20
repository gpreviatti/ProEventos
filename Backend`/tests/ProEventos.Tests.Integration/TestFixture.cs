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

        #region "Api Helpers"
        public async Task AdicionarToken()
        {
            var loginDto = new UserLoginDto()
            {
                UserName = "admin",
                Email = "admin@admin.com",
                Password = "mudar@123"
            };

            var response = await PostAsync(loginDto, "account/loginAsync");
            var resultLogin = await DeserializeResponse<UserLoginResultDto>(response);

            //Add default authorization in each request
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer",
                resultLogin.Token
            );
        }

        public async Task<HttpResponseMessage> GetAsync(string url) => await _client.GetAsync(_hostApi + url);

        public async Task<HttpResponseMessage> PostAsync(dynamic dataclass, string url) => 
            await _client.PostAsync(_hostApi + url, SerializeRequest(dataclass));

        public async Task<HttpResponseMessage> PutAsync(dynamic data, string url) => 
            await _client.PutAsync(_hostApi + url, SerializeRequest(data));

        public async Task<HttpResponseMessage> DeleteAsync(string url) => await _client.DeleteAsync(_hostApi + url);

        public StringContent SerializeRequest(dynamic request) =>
            new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

        public async Task<T> DeserializeResponse<T>(HttpResponseMessage response) =>
            JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        #endregion
    }
}
