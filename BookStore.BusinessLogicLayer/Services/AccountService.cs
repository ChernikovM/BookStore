﻿using AutoMapper;
using BookStore.BusinessLogicLayer.Exceptions;
using BookStore.BusinessLogicLayer.Models.Responses;
using BookStore.BusinessLogicLayer.Models.User;
using BookStore.BusinessLogicLayer.Services.Interfaces;
using BookStore.DataAccessLayer.Entities;
using BookStore.DataAccessLayer.Enums;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
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
        private readonly IEmailSenderService _emailSenderService;
        private readonly IJwtService _jwtService;

        public AccountService(
            UserManager<User> userManager,
            IMapper mapper,
            IEmailSenderService emailSenderService,
            IJwtService jwtService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _emailSenderService = emailSenderService;
            _jwtService = jwtService;
        }

        public async Task<MessageResponse> Register(UserRegistrationModel model)
        {
            var newUser = _mapper.Map<User>(model);

            var result = await _userManager.CreateAsync(newUser, model.Password);

            if (!result.Succeeded)
            {
                throw new CustomException(HttpStatusCode.BadRequest, result);
            }

            await _userManager.AddToRoleAsync(newUser, Enums.Roles.User.ToString());

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

            await SetClaims(user);

            return new MessageResponse() { Message = "Registration successfully completed." };
        }

        private async Task SetClaims(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, _userManager.GetRolesAsync(user).Result.First()),

            };

            await _userManager.AddClaimsAsync(user, claims);
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

            var checkEmailConfirmation = await _userManager.IsEmailConfirmedAsync(user);
            if (!checkEmailConfirmation)
            {
                throw new CustomException(System.Net.HttpStatusCode.Unauthorized, "Email not confirmed.");
            }

            var userClaims = await _userManager.GetClaimsAsync(user);

            var tokenPair = _jwtService.GenerateTokenPair(userClaims);

            user.RefreshToken = tokenPair.RefreshToken;
            await _userManager.UpdateAsync(user);

            return tokenPair;
        }

        public async Task<JwtPairResponse> RefreshTokens(UserRefreshTokensModel model, string accessToken)
        {
            var claims = _jwtService.GetClaims(accessToken);
            var userName = claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            var user = await _userManager.FindByNameAsync(userName);

            if (user is null)
            {
                throw new CustomException(HttpStatusCode.Unauthorized, "User was not found.");
            }

            if (string.IsNullOrWhiteSpace(model.RefreshToken))
            {
                throw new CustomException(HttpStatusCode.BadRequest, "Invalid token.");
            }

            var refreshTokenIsValid = _jwtService.ValidateRefreshToken(user, model.RefreshToken);

            if (!refreshTokenIsValid)
            {
                throw new CustomException(HttpStatusCode.Unauthorized, "Invalid refreshToken.");
            }

            var userClaims = await _userManager.GetClaimsAsync(user);
            var response = _jwtService.GenerateTokenPair(userClaims);

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

        public async Task<MessageResponse> ChangePassword(UserChangePasswordModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);

            if (user is null)
            {
                throw new CustomException(HttpStatusCode.BadRequest, "User was not found.");
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);

            if (!result.Succeeded)
            {
                throw new CustomException(HttpStatusCode.BadRequest, result);
            }

            return new MessageResponse() { Message = "Password was successfully changed." };
        }

        public async Task<MessageResponse> Logout(string accessToken)
        {
            var claims = _jwtService.GetClaims(accessToken);
            var userName = claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            var user = await _userManager.FindByNameAsync(userName);

            if (user is null)
            {
                throw new CustomException(HttpStatusCode.BadRequest, "User was not found.");
            }

            user.RefreshToken = null;
            await _userManager.UpdateAsync(user);

            return new MessageResponse() {Message = "Successfully logged out."};
        }
    }
}
