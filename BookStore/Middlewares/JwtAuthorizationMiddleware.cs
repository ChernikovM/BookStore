using BookStore.BusinessLogicLayer.Services.Interfaces;
using BookStore.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.PresentationLayer.Middlewares
{
    public class JwtAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtAuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, UserManager<User> userManager, IJwtService jwtService)
        {
            var accessToken = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (accessToken is not null)
            {
                await AuthorizeUser(context, accessToken, userManager, jwtService);
            }

            await _next.Invoke(context);
        }

        private async Task AuthorizeUser(HttpContext context, string token, UserManager<User> userManager, IJwtService jwtService)
        {
            try
            {
                var claimsPrincipal = jwtService.ValidateAccessToken(token);

                context.User = claimsPrincipal;

                //var userName = jwtToken.Claims.First(x => x.Type == ClaimTypes.Name).Value;

                //var claims = userManager.GetClaimsAsync(userManager.FindByNameAsync(userName).Result).Result;
                //записать в context.User юзера из клаймов.
                //context.User = userManager.FindByNameAsync(userName).Result;
                //context.Items["User"] = userManager.FindByNameAsync(userName);
            }
            catch
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }
    }
}
