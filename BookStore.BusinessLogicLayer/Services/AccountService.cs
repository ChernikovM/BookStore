using AutoMapper;
using BookStore.BusinessLogicLayer.Exceptions;
using BookStore.BusinessLogicLayer.Extensions;
using BookStore.BusinessLogicLayer.Models.RequestModels.User;
using BookStore.BusinessLogicLayer.Models.ResponseModels;
using BookStore.BusinessLogicLayer.Models.ResponseModels.User;
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
using static BookStore.BusinessLogicLayer.Constants.Constants;

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

        public async Task<User> FindByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            CheckUserExist(user);

            return user;
        }

        public async Task<User> FindByNameAsync(string name)
        {
            var user = await _userManager.FindByNameAsync(name);

            CheckUserExist(user);

            return user;
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            CheckUserExist(user);

            return user;
        }

        public async Task<User> FindByTokenAsync(string token)
        {
            var userClaims = _jwtService.GetClaimsFromToken(token);
            var username = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;

            var user = await FindByNameAsync(username);

            return user;
        }

        private void CheckUserExist(User user)
        {
            if (user is null)
            {
                throw new CustomException(HttpStatusCode.BadRequest, ErrorMessage.AccountNotFound.GetDescription());
            }
        }

        public async Task<UserResponseModel> Register(UserRegistrationModel model)
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

            var userResponse = _mapper.Map<UserResponseModel>(newUser);

            return userResponse;
            //return new MessageResponse() { Message = "Please confirm your email." };
        }

        public async Task<MessageResponse> ConfirmEmail(UserEmailConfirmationModel model)
        {
            var user = await FindByIdAsync(model.UserId);

            var confirmationResult = await _userManager.ConfirmEmailAsync(user, model.Token);

            if (!confirmationResult.Succeeded)
            {
                throw new CustomException(HttpStatusCode.BadRequest, ErrorMessage.InvalidToken.GetDescription());
            }

            return new MessageResponse() { Message = "Registration successfully completed." };
        }

        public async Task<JwtPairResponse> Login(UserLoginModel model)
        {
            var user = await FindByNameAsync(model.UserName);

            bool checkPassword = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!checkPassword)
            {
                throw new CustomException(HttpStatusCode.BadRequest, ErrorMessage.InvalidCredentials.GetDescription());
            }

            if (user.LockoutEnd is not null && user.LockoutEnd > DateTime.UtcNow)
            {
                throw new CustomException(HttpStatusCode.Forbidden, ErrorMessage.AccountBlocked.GetDescription());
            }

            var checkEmailConfirmation = await _userManager.IsEmailConfirmedAsync(user);
            if (!checkEmailConfirmation)
            {
                throw new CustomException(HttpStatusCode.Unauthorized, ErrorMessage.EmailNotConfirmed.GetDescription());
            }

            var tokenPair = await _jwtService.GenerateTokenPairAsync(user);

            user.RefreshToken = tokenPair.RefreshToken;
            await _userManager.UpdateAsync(user);

            return tokenPair;
        }

        public async Task<JwtPairResponse> RefreshTokensAsync(UserRefreshTokensModel model)
        {
            var user = await FindByTokenAsync(model.RefreshToken);

            var refreshTokenIsValid = _jwtService.ValidateRefreshToken(user, model.RefreshToken);
            if (!refreshTokenIsValid)
            {
                throw new CustomException(HttpStatusCode.Unauthorized, ErrorMessage.InvalidToken.GetDescription());
            }

            var response = await _jwtService.GenerateTokenPairAsync(user);

            user.RefreshToken = response.RefreshToken;
            await _userManager.UpdateAsync(user);

            return response;
        }

        public async Task<MessageResponse> ResetPassword(UserChangePasswordModel model)
        {
            var user = await FindByEmailAsync(model.Email);

            await _emailSenderService.SendPasswordResettingLinkAsync(user, model.NewPassword);

            return new MessageResponse() { Message = "Email was sent." };
        }

        public async Task<MessageResponse> CheckEmail(UserResetPasswordModel model)
        {
            var user = await FindByEmailAsync(model.Email);

            return new MessageResponse() { Message = "Success" };
        }

        public async Task<MessageResponse> ChangePassword(string userId, string token, string password)
        {
            var user = await FindByIdAsync(userId);

            var result = await _userManager.ResetPasswordAsync(user, token, password);
            if (!result.Succeeded)
            {
                throw new CustomException(HttpStatusCode.BadRequest, result);
            }

            return new MessageResponse() { Message = "Password was successfully changed." };
        }

        public async Task<MessageResponse> Logout(string username)
        {
            var user = await FindByNameAsync(username);

            user.RefreshToken = null;
            await _jwtService.ClearClaims(user);
            await _userManager.UpdateAsync(user);

            return new MessageResponse() { Message = "Successfully logged out." };
        }
    }
}
