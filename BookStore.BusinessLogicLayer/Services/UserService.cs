using AutoMapper;
using BookStore.BusinessLogicLayer.Exceptions;
using BookStore.BusinessLogicLayer.Models;
using BookStore.BusinessLogicLayer.Models.Responses;
using BookStore.BusinessLogicLayer.Models.User;
using BookStore.BusinessLogicLayer.Services.Interfaces;
using BookStore.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookStore.BusinessLogicLayer.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;
        private readonly IEmailSenderService _emailSenderService;
        private readonly IDataCollectionAccessService _dataCollectionService;

        public UserService(
            UserManager<User> userManager, 
            IJwtService jwtService, 
            IMapper mapper, 
            IEmailSenderService emailSenderService,
            IDataCollectionAccessService dataCollectionService
            )
        {
            _userManager = userManager;
            _jwtService = jwtService;
            _mapper = mapper;
            _emailSenderService = emailSenderService;
            _dataCollectionService = dataCollectionService;
        }

        private async Task<User> GetUserByTokenAsync(string token)
        {
            var userClaims = _jwtService.GetClaims(token);

            var user = await _userManager.FindByNameAsync(userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value);

            if (user is null)
            {
                throw new CustomException(HttpStatusCode.BadRequest, "User was not found.");
            }

            return user;
        }

        public async Task<UserResponseModel> GetMyProfile(string accessToken)
        {
            var user = await GetUserByTokenAsync(accessToken);

            var response = _mapper.Map<UserResponseModel>(user);

            return response;
        }

        public async Task<MessageResponse> UpdateMyProfile(UserUpdateModel model, string accessToken)
        {
            bool emailUpdatedFlag = false;

            var user = await GetUserByTokenAsync(accessToken);

            var passwordValidationResult = await _userManager.CheckPasswordAsync(user, model.Password);

            if (!passwordValidationResult)
            {
                throw new CustomException(HttpStatusCode.BadRequest, "Invalid password.");
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            if(!user.Email.Equals(model.Email))
            {
                user.Email = model.Email;
                user.EmailConfirmed = false;
                await _emailSenderService.SendEmailConfirmationLinkAsync(user); //TODO: change email text (from registration to update)
                emailUpdatedFlag = true;
            }

            var result = await _userManager.UpdateAsync(user);
            
            if (!result.Succeeded)
            {
                throw new CustomException(HttpStatusCode.BadRequest, result);
            }

            string response;
            if (emailUpdatedFlag == true)
            {
                response = "Confirm your email.";
            }
            else
            {
                response = "Account was successfully updated.";
            }

            return new MessageResponse() { Message = response };
        }

        public async Task<UserModel> GetUserProfile(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null)
            {
                throw new CustomException(HttpStatusCode.BadRequest, "User was not found.");
            }

            var response = _mapper.Map<UserModel>(user);

            return response;
        }

        public async Task<MessageResponse> EditUserProfile(UserUpdateModel model, string accessToken)
        {
            var admin = await GetUserByTokenAsync(accessToken);

            var passwordValidationResult = await _userManager.CheckPasswordAsync(admin, model.Password);
            if(!passwordValidationResult)
            {
                throw new CustomException(HttpStatusCode.BadRequest, "Invalid password.");
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null)
            {
                throw new CustomException(HttpStatusCode.BadRequest, "User was not found.");
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            if (user.Email.Equals(model.Email) == false)
            {
                user.Email = model.Email;
                user.EmailConfirmed = false;
            }
            await _userManager.UpdateAsync(user);

            return new MessageResponse() { Message = "Changes was successfully saved."};
        }

        public async Task<DataCollectionModel<UserModel>> GetAllUsers(IndexRequestModel model)
        {
            var collection = _userManager.Users;

            var result = _dataCollectionService.GetCollection<UserModel, User>(collection, model, out DataCollectionModel<UserModel> responseModel);

            return responseModel;
        }

        public async Task<MessageResponse> BlockUser(UserLockoutModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is null)
            {
                throw new CustomException(HttpStatusCode.BadRequest, "User was not found.");
            }

            if (!user.LockoutEnabled)
            {
                throw new CustomException(HttpStatusCode.BadRequest, "This user cannot be blocked.");
            }

            user.LockoutEnd = DateTime.UtcNow.AddYears(100); //TODO: hardcode
            await _userManager.UpdateAsync(user);

            return new MessageResponse() { Message = "User was successfully blocked."};
        }

        public async Task<MessageResponse> UnblockUser(UserLockoutModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is null)
            {
                throw new CustomException(HttpStatusCode.BadRequest, "User was not found.");
            }

            user.LockoutEnd = null;
            await _userManager.UpdateAsync(user);

            return new MessageResponse() { Message = "User was successfully unblocked." };
        }
    }
}
