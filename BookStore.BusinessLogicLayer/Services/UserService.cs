using AutoMapper;
using BookStore.BusinessLogicLayer.Exceptions;
using BookStore.BusinessLogicLayer.Models;
using BookStore.BusinessLogicLayer.Models.Responses;
using BookStore.BusinessLogicLayer.Models.User;
using BookStore.BusinessLogicLayer.Services.Interfaces;
using BookStore.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using System.Web;

namespace BookStore.BusinessLogicLayer.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;
        private readonly IEmailSenderService _emailSenderService;

        public UserService(UserManager<User> userManager, IJwtService jwtService, IMapper mapper, IEmailSenderService emailSenderService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
            _mapper = mapper;
            _emailSenderService = emailSenderService;
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

        public async Task<UserResponseModel> GetUserProfile(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null)
            {
                throw new CustomException(HttpStatusCode.BadRequest, "User was not found.");
            }

            var response = _mapper.Map<UserResponseModel>(user);

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

        public async Task<MessageResponse> GetAllUsers()
        {
            return new MessageResponse() { Message = "List of users." };
        }

        public async Task<List<User>> TestSort(SortModel sortModel) //TODO: returnType change
        {
            var result = _userManager.Users.OrderBy($"{HttpUtility.UrlDecode(sortModel.SortBy)}");

            return result.ToList();
        }

        private List<T> SortByPropertyName<T>(IQueryable<T> collection, string str)
        {
            var sortedItems = collection.OrderBy($"{HttpUtility.UrlDecode(str)}");
            return sortedItems.ToList();
        }

    }
}
