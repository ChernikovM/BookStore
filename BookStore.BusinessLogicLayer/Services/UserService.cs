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

        public async Task<UserResponseModel> GetMyProfile(string accessToken)
        {
            var user = await _accountService.FindByTokenAsync(accessToken);

            var response = _mapper.Map<UserResponseModel>(user);

            return response;
        }

        public async Task<UserResponseModel> GetUserProfile(string id, string accessToken)
        {
            var claims = _jwtService.GetClaimsFromToken(accessToken);

            var requesterId = claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.UserData)).Value;
            var requesterRole = claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Role)).Value;

            if (!(requesterRole.Equals(Enums.Roles.Admin) || requesterId.Equals(id)))
            {
                throw new CustomException(HttpStatusCode.Forbidden);
            }

            User userProfile = await _accountService.FindByIdAsync(id);

            return _mapper.Map<UserResponseModel>(userProfile);
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

        public async Task<MessageResponse> Update(string id, UserUpdateModel model, string accessToken)
        {            
            var claims = _jwtService.GetClaimsFromToken(accessToken);
            var requesterId = claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.UserData)).Value;
            var requesterRole = claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Role)).Value;
            var requester = await _accountService.FindByIdAsync(requesterId);

            if (id is null)
            {
                id = requesterId;
            }

            var user = await _accountService.FindByIdAsync(id);

            if (!(requesterRole.Equals(Enums.Roles.Admin.ToString()) || requesterId.Equals(id)))
            {
                throw new CustomException(HttpStatusCode.Forbidden);
            }

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
