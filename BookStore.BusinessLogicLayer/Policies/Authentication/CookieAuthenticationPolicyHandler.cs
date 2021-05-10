using BookStore.BusinessLogicLayer.Providers.Interfaces;
using BookStore.BusinessLogicLayer.Services.Interfaces;
using BookStore.DataAccessLayer.Enums;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Threading.Tasks;

namespace BookStore.BusinessLogicLayer.Policies.Authentication
{
    public class CookieAuthenticationPolicyHandler : CookieAuthenticationEvents
    {
        private readonly IJwtProvider _jwtProvider;
        private readonly IAuthService _authService;

        private readonly string Scheme;

        public CookieAuthenticationPolicyHandler(IJwtProvider jwtProvider, IAuthService authService)
        {
            _jwtProvider = jwtProvider;
            _authService = authService;

            Scheme = CookieAuthenticationDefaults.AuthenticationScheme;
        }

        public override async Task ValidatePrincipal(CookieValidatePrincipalContext context)
        {
            var userPrincipal = context.Principal;

            var accessToken = userPrincipal.FindFirst(x => x.Type == "AccessToken").Value;

            if (!_jwtProvider.ValidateToken(accessToken))
            {
                var refreshToken = userPrincipal.FindFirst(x => x.Type == "RefreshToken").Value;

                //refresh token pair
                if (refreshToken is null || !(await _authService.RefreshTokenPairAsync(context.HttpContext, refreshToken, Scheme)))
                {
                    context.RejectPrincipal();
                    await _authService.SignOutAsync(context.HttpContext, Scheme);
                    return;
                }
            }
        }
    }
}
