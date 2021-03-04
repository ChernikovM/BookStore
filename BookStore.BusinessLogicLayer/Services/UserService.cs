using AutoMapper;
using BookStore.BusinessLogicLayer.Exceptions;
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

        private async Task<User> GetUserByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
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

        public async Task<UserResponseModel> GetUserProfile(string id, string accessToken)
        {
           
            var claims = _jwtService.GetClaimsFromToken(accessToken);

            var requesterId = claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.UserData)).Value;
            var requesterRole = claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Role)).Value;

            if (!(requesterRole.Equals(Enums.Roles.Admin) || requesterId.Equals(id)))
            {
                throw new CustomException(HttpStatusCode.Forbidden);
            }

            User userProfile = await GetUserByIdAsync(id);

            return _mapper.Map<UserResponseModel>(userProfile);
        }

        public async Task<DataCollectionModel<UserResponseModel>> GetAllUsers(IndexRequestModel model)
        {
            var collection = _userManager.Users;

            _dataCollectionService.GetCollection<UserResponseModel, User>(collection, model, out DataCollectionModel<UserResponseModel> responseModel);

            return responseModel;
        }

        public async Task<MessageResponse> BlockUser(string id, int? days)
        {
            var user = await GetUserByIdAsync(id);

            if (!user.LockoutEnabled)
            {
                throw new CustomException(HttpStatusCode.BadRequest, "This user cannot be blocked.");
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
            var user = await GetUserByIdAsync(id);

            user.LockoutEnd = null;
            await _userManager.UpdateAsync(user);

            return new MessageResponse() { Message = "User was successfully unblocked." };
        }

        public async Task<MessageResponse> Update(string? id, UserUpdateModel model, string accessToken)
        {            
            var claims = _jwtService.GetClaimsFromToken(accessToken);
            var requesterId = claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.UserData)).Value;
            var requesterRole = claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Role)).Value;
            var requester = await GetUserByIdAsync(requesterId);

            if (id is null)
            {
                id = requesterId;
            }

            var user = await GetUserByIdAsync(id);

            if (!(requesterRole.Equals(Enums.Roles.Admin.ToString()) || requesterId.Equals(id)))
            {
                throw new CustomException(HttpStatusCode.Forbidden);
            }

            var passwordValidationResult = await _userManager.CheckPasswordAsync(requester, model.Password);
            if (!passwordValidationResult)
            {
                throw new CustomException(HttpStatusCode.BadRequest, "Invalid password.");
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
