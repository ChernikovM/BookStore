using BookStore.BusinessLogicLayer.Models.ResponseModels;
using BookStore.DataAccessLayer.Entities;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookStore.BusinessLogicLayer.Services.Interfaces
{
    public interface IJwtService
    {
        public Task<JwtPairResponse> GenerateTokenPairAsync(User user);
        public bool ValidateRefreshToken(User user, string token);
        public ClaimsPrincipal ValidateAccessToken(string token);
        public string GenerateToken(IList<Claim> claims, int lifeTimeMinutes);
        public IEnumerable<Claim> GetClaimsFromToken(string token);
        public Task ClearClaims(User user);
    }
}
