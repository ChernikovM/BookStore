using AutoMapper;
using BookStore.BusinessLogicLayer.Exceptions;
using BookStore.BusinessLogicLayer.Models.RequestModels.User;
using BookStore.BusinessLogicLayer.Models.ResponseModels;
using BookStore.BusinessLogicLayer.Providers.Interfaces;
using BookStore.BusinessLogicLayer.Services.Interfaces;
using BookStore.DataAccessLayer.Entities;
using BookStore.DataAccessLayer.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookStore.BusinessLogicLayer.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IEmailSenderProvider _emailSenderService;
        private readonly IJwtProvider _jwtService;

        public AccountService(
            UserManager<User> userManager,
            IMapper mapper,
            IEmailSenderProvider emailSenderService,
            IJwtProvider jwtService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _emailSenderService = emailSenderService;
            _jwtService = jwtService;
        }

        public async Task<MessageResponse> Register(UserRegistrationModel model)
        {
            var newUser = _mapper.Map<User>(model);
            newUser.LockoutEnabled = true;

            var result = await _userManager.CreateAsync(newUser, model.Password);

            if (!result.Succeeded)
            {
                throw new CustomException(HttpStatusCode.BadRequest, result);
            }

            result = await _userManager.AddToRoleAsync(newUser, Enums.Roles.User.ToString());

            if (!result.Succeeded)
            {
                throw new CustomException(HttpStatusCode.BadRequest, result);
            }

            await _emailSenderService.SendEmailConfirmationLinkAsync(newUser);

            return new MessageResponse() { Message = "Please confirm your email." };
        }

        public async Task<MessageResponse> ConfirmEmail(UserEmailConfirmationModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);

            if (user is null)
            {
                throw new CustomException(HttpStatusCode.BadRequest, "User was not found.");
            }

            var confirmationResult = await _userManager.ConfirmEmailAsync(user, model.Token);

            if (!confirmationResult.Succeeded)
            {
                throw new CustomException(HttpStatusCode.BadRequest, "Invalid token.");
            }

            return new MessageResponse() { Message = "Registration successfully completed." };
        }

        public async Task<JwtPairResponse> Login(UserLoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user is null)
            {
                throw new CustomException(HttpStatusCode.BadRequest, "Invalid credentials.");
            }

            bool checkPassword = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!checkPassword)
            {
                throw new CustomException(HttpStatusCode.BadRequest, "Invalid credentials.");
            }

            if (user.LockoutEnd is not null && user.LockoutEnd > DateTime.UtcNow)
            {
                throw new CustomException(HttpStatusCode.Forbidden, "This account is blocked.");
            }

            var checkEmailConfirmation = await _userManager.IsEmailConfirmedAsync(user);
            if (!checkEmailConfirmation)
            {
                throw new CustomException(System.Net.HttpStatusCode.Unauthorized, "Email not confirmed.");
            }

            var tokenPair = await _jwtService.GenerateTokenPairAsync(user);

            user.RefreshToken = tokenPair.RefreshToken;
            await _userManager.UpdateAsync(user);

            return tokenPair;
        }

        public async Task<JwtPairResponse> RefreshTokens(UserRefreshTokensModel model, string accessToken)
        {
            var claims = _jwtService.GetClaimsFromToken(accessToken);
            var userName = claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            var user = await _userManager.FindByNameAsync(userName);

            if (user is null)
            {
                throw new CustomException(HttpStatusCode.Unauthorized, "User was not found.");
            }

            var refreshTokenIsValid = _jwtService.ValidateRefreshToken(user, model.RefreshToken);

            if (!refreshTokenIsValid)
            {
                throw new CustomException(HttpStatusCode.Unauthorized, "Invalid refreshToken.");
            }

            var response = await _jwtService.GenerateTokenPairAsync(user);

            user.RefreshToken = response.RefreshToken;
            await _userManager.UpdateAsync(user);

            return response;
        }

        public async Task<MessageResponse> ResetPassword(UserResetPasswordModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is null)
            {
                throw new CustomException(HttpStatusCode.BadRequest, "User was not found.");
            }

            await _emailSenderService.SendPasswordResettingLinkAsync(user);

            return new MessageResponse() { Message = "Email was sent." };
        }

        public async Task<MessageResponse> ChangePassword(string userId, string token, UserChangePasswordModel model)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
            {
                throw new CustomException(HttpStatusCode.BadRequest, "User was not found.");
            }

            var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);

            if (!result.Succeeded)
            {
                throw new CustomException(HttpStatusCode.BadRequest, result);
            }

            return new MessageResponse() { Message = "Password was successfully changed." };
        }

        public async Task<MessageResponse> Logout(string accessToken)
        {
            var claims = _jwtService.GetClaimsFromToken(accessToken);
            var userName = claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            var user = await _userManager.FindByNameAsync(userName);

            if (user is null)
            {
                throw new CustomException(HttpStatusCode.BadRequest, "User was not found.");
            }

            user.RefreshToken = null;
            await _jwtService.ClearClaims(user);
            await _userManager.UpdateAsync(user);

            return new MessageResponse() { Message = "Successfully logged out." };
        }
    }
}
