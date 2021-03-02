using BookStore.BusinessLogicLayer.Configurations.Interfaces;
using BookStore.BusinessLogicLayer.Models.ResponseModels;
using BookStore.BusinessLogicLayer.Services.Interfaces;
using BookStore.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BusinessLogicLayer.Services
{
    public class JwtService : IJwtService
    {
        private readonly IJwtConfiguration _config;
        private readonly UserManager<User> _userManager;

        public JwtService(IJwtConfiguration config, UserManager<User> userManager)
        {
            _config = config;
            _userManager = userManager;
        }

        public string GenerateToken(IList<Claim> claims, int lifeTimeMinutes)
        {
            var secret = Encoding.ASCII.GetBytes(_config.Secret);

            var expiredDate = DateTime.UtcNow.AddMinutes(lifeTimeMinutes);

            var jwtToken = new JwtSecurityToken(
                _config.Issuer,
                _config.Audience,
                claims,
                expires: expiredDate,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
                );

            var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return token;
        }

        private async Task<IList<Claim>> SetClaims(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, _userManager.GetRolesAsync(user).Result.First()),

            };

            await _userManager.AddClaimsAsync(user, claims);
            return claims;
        }

        public async Task ClearClaims(User user)
        {
            await _userManager.RemoveClaimsAsync(user, await _userManager.GetClaimsAsync(user));
        }

        public async Task<JwtPairResponse> GenerateTokenPairAsync(User user)
        {
            await ClearClaims(user);
            var claims = await SetClaims(user);

            var accessToken = GenerateToken(claims, _config.AccessTokenExpiration);
            var refreshToken = GenerateToken(claims, _config.RefreshTokenExpiration);

            JwtPairResponse result = new JwtPairResponse() { AccessToken = accessToken, RefreshToken = refreshToken };

            return result;
        }

        public IEnumerable<Claim> GetClaimsFromToken(string token)
        {
            return new JwtSecurityTokenHandler().ReadJwtToken(token).Claims;
        }

        public ClaimsPrincipal ValidateAccessToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            
            var claimsPrincipal = tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = _config.Issuer,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config.Secret)),
                ValidAudience = _config.Audience,
                ValidateAudience = true,
                ValidateLifetime = true,
                

            }, out _);

            return claimsPrincipal;
        }

        public bool ValidateRefreshToken(User user, string token)
        {
            string userToken = user.RefreshToken;

            if (!token.Equals(userToken))
            {
                return false;
            }

            var validTo = new JwtSecurityTokenHandler().ReadJwtToken(token).ValidTo;

            if (validTo < DateTime.UtcNow)
            {
                return false;
            }

            return true;
        }
    }
}
