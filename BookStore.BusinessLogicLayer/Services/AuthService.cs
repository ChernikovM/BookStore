using BookStore.BusinessLogicLayer.Models.RequestModels.User;
using BookStore.BusinessLogicLayer.Providers.Interfaces;
using BookStore.BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookStore.BusinessLogicLayer.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAccountService _accountService;
        private readonly IJwtProvider _jwtProvider;

        private const string _schema = CookieAuthenticationDefaults.AuthenticationScheme;

        public AuthService(IAccountService accountService, IJwtProvider jwtProvider)
        {
            _accountService = accountService;
            _jwtProvider = jwtProvider;

            //_schema = CookieAuthenticationDefaults.AuthenticationScheme;
        }

        public async Task SignInAsync(HttpContext context, List<Claim> claims, string schema = _schema)
        {
            var claimsIdentity = new ClaimsIdentity(claims, schema);
            var authProperties = new AuthenticationProperties();

            await context.SignInAsync(schema, new ClaimsPrincipal(claimsIdentity), authProperties);
        }

        public async Task SignOutAsync(HttpContext context, string schema = _schema)
        {
            await context.SignOutAsync(schema);
        }

        public async Task<bool> RefreshTokenPairAsync(HttpContext context, string refreshToken, string schema = _schema)
        {
            var response = await _accountService.RefreshTokensAsync(new UserRefreshTokensModel() { RefreshToken = refreshToken });

            if(response is null)
            {
                return false;
            }

            var claims = _jwtProvider.GetClaimsFromToken(response.AccessToken).ToList();

            claims.Add(new Claim("AccessToken", response.AccessToken));
            claims.Add(new Claim("RefreshToken", response.RefreshToken));

            await SignInAsync(context, claims, schema);

            return true;
        }
    }
}
