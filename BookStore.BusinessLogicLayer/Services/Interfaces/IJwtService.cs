using BookStore.BusinessLogicLayer.Models.Responses;
using BookStore.DataAccessLayer.Entities;
using System.Collections.Generic;
using System.Security.Claims;

namespace BookStore.BusinessLogicLayer.Services.Interfaces
{
    public interface IJwtService
    {
        public LoginResponse GenerateTokenPair(IList<Claim> claims);
        public string GenerateAccessToken(IList<Claim> claims);
        public string GenerateRefreshToken(IList<Claim> claims);
        public bool ValidateRefreshToken(User user, string token);
        public ClaimsPrincipal ValidateAccessToken(string token);
    }
}
