using ProEventos.Domain.Dtos;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProEventos.Tests.Integration.Helpers
{
    public class ApiHelper
    {
        protected static string _hostApi;
        protected readonly HttpClient _httpClient;

        public ApiHelper(string hostApi, HttpClient httpClient)
        {
            _hostApi = hostApi;
            _httpClient = httpClient;
        }

        public async Task AddBearerTokenHeader(string userName, string email, string password)
        {
            var loginDto = new UserLoginDto
            {
                UserName = userName,
                Email = email,
                Password = password
            };

            var response = await PostAsync("account/loginAsync", loginDto);
            var resultLogin = await DeserializeResponse<UserLoginResultDto>(response);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer",
                resultLogin.Token
            );
        }

        public async Task<HttpResponseMessage> GetAsync(string url) => 
            await _httpClient.GetAsync(_hostApi + url);

        public async Task<HttpResponseMessage> PostAsync(string url, dynamic dataclass) =>
            await _httpClient.PostAsync(_hostApi + url, SerializeRequest(dataclass));

        public async Task<HttpResponseMessage> PutAsync(string url, dynamic data) =>
            await _httpClient.PutAsync(_hostApi + url, SerializeRequest(data));

        public async Task<HttpResponseMessage> DeleteAsync(string url) => 
            await _httpClient.DeleteAsync(_hostApi + url);

        public StringContent SerializeRequest(dynamic data) 
        {
            var json = JsonSerializer.Serialize(data);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        public async Task<T> DeserializeResponse<T>(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<T>(content, options);
        }
    }
}
