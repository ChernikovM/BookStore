using BookStore.AdminPanel.Models;
using BookStore.AdminPanel.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookStore.AdminPanel.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public string AccessToken => "AccessToken";
        public string RefreshToken => "RefreshToken";

        public AuthorizationService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task SignInAsync(JwtPairModel jwtPair)
        {
            var claims = new List<Claim>
            {
                new Claim(AccessToken, jwtPair.AccessToken),
                new Claim(RefreshToken, jwtPair.RefreshToken)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties();

            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
        }

        public string GetClaimValue(string type)
        {
            return _httpContextAccessor.HttpContext.User.FindFirstValue(type);
        }

        public bool IsAuthorized()
        {
            return _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public async Task SignOutAsync()
        {
            await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
