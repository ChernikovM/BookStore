using BookStore.AdminPanel.Models;
using BookStore.AdminPanel.Models.RequestModels;
using BookStore.AdminPanel.Services.Interfaces;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace BookStore.AdminPanel.Services
{
    public class AccountService : IAccountService
    {
        private readonly IHttpService _http;

        public string ApiUrl => "https://localhost:44366/Accounts";

        public AccountService(IAuthorizationService authService, IHttpService http)
        {
            _http = http;
        }

        public async Task<JwtPairModel> LoginAsync(SignInModel model)
        {
            var response = await _http.SendAsync(HttpMethod.Post, $"{ApiUrl}/Login", model);
            var cont = await response.Content.ReadAsStringAsync();
            var t = JsonConvert.DeserializeObject<object>(cont);
            
            return JsonConvert.DeserializeObject<JwtPairModel>(await response.Content.ReadAsStringAsync());
        }

        public async Task<JwtPairModel> RefreshTokenPairAsync(string refreshToken)
        {
            var response = await _http.SendAsync(
                                        HttpMethod.Post, 
                                        $"{ApiUrl}/RefreshTokens",
                                        new JwtRefreshRequestModel() { RefreshToken = refreshToken });

            return JsonConvert.DeserializeObject<JwtPairModel>(await response.Content.ReadAsStringAsync());
        }
    }
}
