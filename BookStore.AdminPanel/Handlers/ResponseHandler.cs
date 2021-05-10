using BookStore.AdminPanel.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.AdminPanel.Handlers
{
    public class ResponseHandler : DelegatingHandler
    {
        private readonly IHttpService _http;
        private readonly IAuthorizationService _authService;
        private readonly IAccountService _accountService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private bool IsRefreshing;

        public ResponseHandler(IAuthorizationService authService, IHttpService http, IAccountService accountService, IHttpContextAccessor httpContextAccessor)
        {
            _http = http;
            _authService = authService;
            _accountService = accountService;
            _httpContextAccessor = httpContextAccessor;

            IsRefreshing = false;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);

            if (response.IsSuccessStatusCode) // 200 OK
            {
                return response;
            }

            if(response.StatusCode == HttpStatusCode.Unauthorized) //401 Unauthorized
            {
                return await HandleUnathorized(request, response);
            }

            //await RedirectToErrorPage(response);

            if (response.StatusCode == HttpStatusCode.BadRequest) //400 BadRequest
            {
                //var c = JsonConvert.DeserializeObject<ValidationResult>(await response.Content.ReadAsStringAsync());
            }

            return response;
        }

        private void RedirectToLoginPage()
        {
            IsRefreshing = false;

            var url = _httpContextAccessor.HttpContext.Request.Path;

            _httpContextAccessor.HttpContext.Response.Redirect($"/Login?ReturnUrl={url}", false, false);
        }

        private async Task RedirectToErrorPage(HttpResponseMessage response)
        {
            IsRefreshing = false;

            await _httpContextAccessor.HttpContext.Response.WriteAsync(await response.Content.ReadAsStringAsync());

            _httpContextAccessor.HttpContext.Response.Redirect($"/ErrorPage?code={response.StatusCode}", false, false);
        }

        private async Task<HttpResponseMessage> RepeatRequestWithNewToken(HttpRequestMessage request, string token)
        {
            IsRefreshing = false;

            var newRequest = new HttpRequestMessage(request.Method, request.RequestUri);
            newRequest.Content = request.Content;
            newRequest.Headers.Authorization = new AuthenticationHeaderValue("bearer", token);

            return await _http.SendAsync(newRequest);
        }

        private async Task<HttpResponseMessage> HandleUnathorized(HttpRequestMessage request, HttpResponseMessage response)
        {
            if (IsRefreshing)
            {
                RedirectToLoginPage();
                return response;
            }

            if (!_authService.IsAuthorized())
            {
                RedirectToLoginPage();
                return response;
            }

            IsRefreshing = true;

            var refreshToken = _authService.GetClaimValue(_authService.RefreshToken);
            var newTokenPair = await _accountService.RefreshTokenPairAsync(refreshToken);

            if (newTokenPair.AccessToken is null || newTokenPair.RefreshToken is null || newTokenPair is null)
            {
                RedirectToLoginPage();
                return response;
            }

            //await _authService.SignInAsync(newTokenPair);

            return await RepeatRequestWithNewToken(request, newTokenPair.AccessToken);
        }
    }
}
