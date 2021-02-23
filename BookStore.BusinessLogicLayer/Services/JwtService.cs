using BookStore.BusinessLogicLayer.Configurations.Interfaces;
using BookStore.BusinessLogicLayer.Models.Responses;
using BookStore.BusinessLogicLayer.Services.Interfaces;
using BookStore.DataAccessLayer.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookStore.BusinessLogicLayer.Services
{
    public class JwtService : IJwtService
    {
        private readonly IJwtConfiguration _config;

        public JwtService(IJwtConfiguration config)
        {
            _config = config;
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

        public JwtPairResponse GenerateTokenPair(IList<Claim> claims)
        {
            var accessToken = GenerateToken(claims, _config.AccessTokenExpiration);
            var refreshToken = GenerateToken(claims, _config.RefreshTokenExpiration);

            JwtPairResponse result = new JwtPairResponse() { AccessToken = accessToken, RefreshToken = refreshToken};

            return result;
        }

        public IEnumerable<Claim> GetClaims(string token)
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
                

            }, out SecurityToken validatedToken);

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

            if (validTo < DateTime.Now)
            {
                return false;
            }

            return true;
        }
    }
}
