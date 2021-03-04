using AutoMapper;
using BookStore.BusinessLogicLayer.Exceptions;
using BookStore.BusinessLogicLayer.Models.RequestModels;
using BookStore.BusinessLogicLayer.Models.RequestModels.User;
using BookStore.BusinessLogicLayer.Models.ResponseModels;
using BookStore.BusinessLogicLayer.Models.ResponseModels.User;
using BookStore.BusinessLogicLayer.Providers.Interfaces;
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
        private readonly IJwtProvider _jwtService;
        private readonly IMapper _mapper;
        private readonly IEmailSenderProvider _emailSenderService;
        private readonly IDataCollectionAccessProvider _dataCollectionService;

        public UserService(
            UserManager<User> userManager, 
            IJwtProvider jwtService, 
            IMapper mapper, 
            IEmailSenderProvider emailSenderService,
            IDataCollectionAccessProvider dataCollectionService
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
            var userClaims = _jwtService.GetClaimsFromToken(token);
            var username = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            var user = await _userManager.FindByNameAsync(username);

            if (user is null)
            {
                throw new CustomException(HttpStatusCode.BadRequest, "User was not found.");
            }

            return user;
        }

        private async Task<User> GetUserByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

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

            string response = "Account was successfully updated.";

            if (emailUpdatedFlag == true)
            {
                response = "Confirm your email.";
            }

            return new MessageResponse() { Message = response };
        }

        public async Task<UserResponseModelForAdmin> GetUserProfile(string email)
        {
            var user = await GetUserByEmail(email);

            var response = _mapper.Map<UserResponseModelForAdmin>(user);

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
                await _emailSenderService.SendEmailConfirmationLinkAsync(user); //TODO: change text in email
            }
            await _userManager.UpdateAsync(user);

            return new MessageResponse() { Message = "Changes was successfully saved."};
        }

        public async Task<DataCollectionModel<UserResponseModelForAdmin>> GetAllUsers(IndexRequestModel model)
        {
            var collection = _userManager.Users;

            _dataCollectionService.GetCollection<UserResponseModelForAdmin, User>(collection, model, out DataCollectionModel<UserResponseModelForAdmin> responseModel);

            return responseModel;
        }

        public async Task<MessageResponse> BlockUser(UserLockoutModel model)
        {
            var user = await GetUserByEmail(model.Email);

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
            var user = await GetUserByEmail(model.Email);

            user.LockoutEnd = null;
            await _userManager.UpdateAsync(user);

            return new MessageResponse() { Message = "User was successfully unblocked." };
        }
    }
}
