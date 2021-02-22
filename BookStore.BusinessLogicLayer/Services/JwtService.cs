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
using System.Web;

namespace BookStore.BusinessLogicLayer.Services
{
    public class JwtService : IJwtService
    {
        private readonly IJwtConfiguration _config;

        public JwtService(IJwtConfiguration config)
        {
            _config = config;
        }

        public string GenerateAccessToken(IList<Claim> claims)
        {
            var secret = Encoding.ASCII.GetBytes(_config.Secret);

            var expiredDate = DateTime.UtcNow.AddMinutes(_config.AccessTokenExpiration);

            var jwtToken = new JwtSecurityToken(
                _config.Issuer,
                _config.Audience,
                claims,
                expires: expiredDate,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
                );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return HttpUtility.UrlEncode(accessToken);
        }

        public string GenerateRefreshToken(IList<Claim> claims)
        {
            //TODO: change generator RTs; add lifeTime to token
            var secret = Encoding.ASCII.GetBytes(_config.Secret);

            var expiredDate = DateTime.UtcNow.AddMinutes(_config.RefreshTokenExpiration);

            var jwtToken = new JwtSecurityToken(
                _config.Issuer,
                _config.Audience,
                claims,
                expires: expiredDate,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
                );

            var refreshToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return HttpUtility.UrlEncode(refreshToken);
        }

        public LoginResponse GenerateTokenPair(IList<Claim> claims)
        {
            var accessToken = GenerateAccessToken(claims);
            var refreshToken = GenerateRefreshToken(claims);

            LoginResponse result = new LoginResponse() { AccessToken = accessToken, RefreshToken = refreshToken};

            return result;
        }

        public IEnumerable<Claim> GetClaims(string token)
        {
            return new JwtSecurityTokenHandler().ReadJwtToken(token).Claims;
        }

        public ClaimsPrincipal ValidateAccessToken(string token) //TODO: вынести в отдельный метод генерацию токена и валидацию
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
            //TODO: check tokens lifeTime
            string userToken = user.RefreshToken;

            var result = token.Equals(userToken);

            return result;
        }
    }
}
