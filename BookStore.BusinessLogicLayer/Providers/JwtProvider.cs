using BookStore.BusinessLogicLayer.Configurations.Interfaces;
using BookStore.BusinessLogicLayer.Extensions;
using BookStore.BusinessLogicLayer.Models.ResponseModels;
using BookStore.BusinessLogicLayer.Providers.Interfaces;
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
    public class JwtProvider : IJwtProvider
    {
        private readonly IJwtConfiguration _config;
        private readonly UserManager<User> _userManager;

        public JwtProvider(IJwtConfiguration config, UserManager<User> userManager)
        {
            _config = config;
            _userManager = userManager;
        }

        public string GenerateToken(IList<Claim> claims, int lifeTimeMinutes, string secret)
        {
            var secretBytes = Encoding.ASCII.GetBytes(secret);

            var expiredDate = DateTime.UtcNow.AddMinutes(lifeTimeMinutes);

            var jwtToken = new JwtSecurityToken(
                _config.Issuer,
                _config.Audience,
                claims,
                expires: expiredDate,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(secretBytes), SecurityAlgorithms.HmacSha256Signature)
                );

            var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return token;
        }

        //TODO: bug when user.role == null
        private async Task<IList<Claim>> SetClaims(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, _userManager.GetRolesAsync(user).Result.First()),
                new Claim(ClaimTypes.UserData, user.Id),

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

            var accessToken = GenerateToken(claims, _config.AccessTokenExpiration, _config.SecretAccessToken);
            var refreshToken = GenerateToken(claims, _config.RefreshTokenExpiration, _config.SecretRefreshToken);

            JwtPairResponse result = new JwtPairResponse() { AccessToken = accessToken, RefreshToken = refreshToken };

            return result;
        }

        public IEnumerable<Claim> GetClaimsFromToken(string token)
        {
            IEnumerable<Claim> result;
            try
            {
                result = new JwtSecurityTokenHandler().ReadJwtToken(token).Claims;
            }
            catch(Exception )
            {
                throw new Exceptions.CustomException(System.Net.HttpStatusCode.BadRequest, Constants.Constants.ErrorMessage.InvalidToken.GetDescription());
            }

            return result;
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
