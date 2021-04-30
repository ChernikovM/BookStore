using AutoMapper;
using BookStore.BusinessLogicLayer.Exceptions;
using BookStore.BusinessLogicLayer.Extensions;
using BookStore.BusinessLogicLayer.Models.RequestModels;
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
using System.Linq.Dynamic.Core;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using static BookStore.BusinessLogicLayer.Constants.Constants;

namespace BookStore.BusinessLogicLayer.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IJwtProvider _jwtService;
        private readonly IMapper _mapper;
        private readonly IEmailSenderProvider _emailSenderService;
        private readonly IDataCollectionAccessProvider _dataCollectionService;
        private readonly IAccountService _accountService;

        public UserService(
            UserManager<User> userManager, 
            IJwtProvider jwtService, 
            IMapper mapper, 
            IEmailSenderProvider emailSenderService,
            IDataCollectionAccessProvider dataCollectionService,
            IAccountService accountService
            )
        {
            _userManager = userManager;
            _jwtService = jwtService;
            _mapper = mapper;
            _emailSenderService = emailSenderService;
            _dataCollectionService = dataCollectionService;
            _accountService = accountService;
        }

        private void CheckUserExist(User user)
        {
            if (user is null)
            {
                throw new CustomException(HttpStatusCode.BadRequest, ErrorMessage.UserNotFound.GetDescription());
            }
        }

        public async Task<UserResponseModel> GetMyProfile(string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            CheckUserExist(user);

            var response = _mapper.Map<UserResponseModel>(user);

            return response;
        }

        public async Task<UserResponseModel> GetUserProfile(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            CheckUserExist(user);

            var response = _mapper.Map<UserResponseModel>(user);

            return response;
        }

        public async Task<DataCollectionModel<UserResponseModel>> GetAllUsers(IndexRequestModel model)
        {
            var collection = _userManager.Users;

            _dataCollectionService.GetCollection(collection, model, out DataCollectionModel<UserResponseModel> responseModel);

            return responseModel;
        }

        public async Task<MessageResponse> BlockUser(string id, int? days)
        {
            var user = await _accountService.FindByIdAsync(id);

            if (!user.LockoutEnabled)
            {
                throw new CustomException(HttpStatusCode.BadRequest, ErrorMessage.UserCannotBeBlocked.GetDescription());
            }

            if (days is null)
            {
                days = (DateTime.MaxValue - DateTime.Now).Days;
            }
            user.LockoutEnd = DateTime.UtcNow.AddDays(days.Value);

            await _userManager.UpdateAsync(user);

            return new MessageResponse() { Message = "User was successfully blocked."};
        }

        public async Task<MessageResponse> UnblockUser(string id)
        {
            var user = await _accountService.FindByIdAsync(id);

            user.LockoutEnd = null;
            await _userManager.UpdateAsync(user);

            return new MessageResponse() { Message = "User was successfully unblocked." };
        }

        public async Task<MessageResponse> UpdateMyProfile(UserUpdateModel model, string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            CheckUserExist(user);

            var passwordValidationResult = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!passwordValidationResult)
            {
                throw new CustomException(HttpStatusCode.BadRequest, ErrorMessage.InvalidCredentials.GetDescription());
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            if (user.Email.Equals(model.Email) == false)
            {
                user.Email = model.Email;
                user.EmailConfirmed = false;
                await _emailSenderService.SendEmailConfirmationLinkAsync(user); //TODO: change text in email
            }

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                throw new CustomException(HttpStatusCode.BadRequest, result);
            }

            return new MessageResponse() { Message = "Changes was successfully saved." };
        }

        public async Task<MessageResponse> UpdateUserProfile(string id, UserUpdateModel model, IIdentity userIdentity)
        {
            var requesterName = userIdentity.Name;

            var requester = await _userManager.FindByNameAsync(requesterName);
            CheckUserExist(requester);

            var user = await _userManager.FindByIdAsync(id);
            CheckUserExist(user);

            var passwordValidationResult = await _userManager.CheckPasswordAsync(requester, model.Password);
            if (!passwordValidationResult)
            {
                throw new CustomException(HttpStatusCode.BadRequest, ErrorMessage.InvalidCredentials.GetDescription());
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            if (user.Email.Equals(model.Email) == false)
            {
                user.Email = model.Email;
                user.EmailConfirmed = false;
                await _emailSenderService.SendEmailConfirmationLinkAsync(user); //TODO: change text in email
            }

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                throw new CustomException(HttpStatusCode.BadRequest, result);
            }

            return new MessageResponse() { Message = "Changes was successfully saved." };
        }
    }
}
