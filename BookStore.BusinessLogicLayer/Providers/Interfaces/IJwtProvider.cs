using BookStore.BusinessLogicLayer.Models.ResponseModels;
using BookStore.DataAccessLayer.Entities;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookStore.BusinessLogicLayer.Providers.Interfaces
{
    public interface IJwtProvider
    {
        public Task<JwtPairResponse> GenerateTokenPairAsync(User user);
        public bool ValidateRefreshToken(User user, string token);
        public string GenerateToken(IList<Claim> claims, int lifeTimeMinutes, string secret);
        public IEnumerable<Claim> GetClaimsFromToken(string token);
        public bool ValidateToken(string token);
        public Task ClearClaims(User user);
    }
}
