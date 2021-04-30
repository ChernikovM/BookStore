using BookStore.AdminPanel.Services.Interfaces;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.AdminPanel.Services
{
    public class HttpService : IHttpService
    {
        private readonly IAuthorizationService _authService;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _http;

        public HttpService(IAuthorizationService authService, IHttpClientFactory httpClientFactory)
        {
            _authService = authService;

            _httpClientFactory = httpClientFactory;

            //_http = _httpClientFactory.CreateClient("ResponseHandler");
        }

        public async Task<HttpResponseMessage> SendAsync(HttpMethod method, string url, object body = null, bool addJwtHeader = false)
        {
            var request = CreateRequest(method, url, body, addJwtHeader);

            return await SendAsync(request);
        }

        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            //var response = await _http.SendAsync(request);

            var response = await _httpClientFactory.CreateClient("ResponseHandler").SendAsync(request);

            return response;
        }

        private HttpRequestMessage CreateRequest(HttpMethod method, string url, object body, bool addJwtHeader = false)
        {
            var result = new HttpRequestMessage(method, url);

            if (body is not null)
            {
                result.Content = new StringContent(JsonConvert.SerializeObject(body), encoding: Encoding.UTF8, mediaType: "application/json");
            }

            if (addJwtHeader)
            {
                var token = _authService.GetClaimValue(_authService.AccessToken);

                result.Headers.Authorization = new AuthenticationHeaderValue("bearer", token);
            }

            return result;
        }

    }
}
