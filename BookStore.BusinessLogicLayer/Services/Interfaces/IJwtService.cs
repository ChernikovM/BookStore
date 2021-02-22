using BookStore.BusinessLogicLayer.Models.Responses;
using BookStore.DataAccessLayer.Entities;
using System.Collections.Generic;
using System.Security.Claims;

namespace BookStore.BusinessLogicLayer.Services.Interfaces
{
    public interface IJwtService
    {
        public JwtPairResponse GenerateTokenPair(IList<Claim> claims);
        public bool ValidateRefreshToken(User user, string token);
        public ClaimsPrincipal ValidateAccessToken(string token);
        public string GenerateToken(IList<Claim> claims, int lifeTimeMinutes);
        public IEnumerable<Claim> GetClaims(string token);
    }
}
