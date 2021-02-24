﻿using BookStore.BusinessLogicLayer.Models.Responses;
using BookStore.BusinessLogicLayer.Models.User;
using System.Threading.Tasks;

namespace BookStore.BusinessLogicLayer.Services.Interfaces
{
    public interface IUserService
    {
        public Task<UserResponseModel> GetMyProfile(string accessToken);

        public Task<MessageResponse> UpdateMyProfile(UserUpdateModel model, string accessToken);

        public Task<UserResponseModel> GetUserProfile(string email);

        public Task<MessageResponse> EditUserProfile(UserUpdateModel model, string accessToken);
    }
}
