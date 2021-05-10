using BookStore.BusinessLogicLayer.Models.ResponseModels;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookStore.BusinessLogicLayer.Services.Interfaces
{
    public interface IAuthService
    {
        Task SignInAsync(HttpContext context, List<Claim> claims, string schema = default!);

        Task SignOutAsync(HttpContext context, string schema = default!);

        Task<bool> RefreshTokenPairAsync(HttpContext context, string refreshToken, string schema = default!);
    }
}
