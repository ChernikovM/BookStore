using BookStore.BusinessLogicLayer.Models;
using BookStore.BusinessLogicLayer.Models.Responses;
using BookStore.BusinessLogicLayer.Models.User;
using System.Threading.Tasks;

namespace BookStore.BusinessLogicLayer.Services.Interfaces
{
    public interface IUserService
    {
        public Task<UserResponseModel> GetMyProfile(string accessToken);

        public Task<MessageResponse> UpdateMyProfile(UserUpdateModel model, string accessToken);

        public Task<UserModel> GetUserProfile(string email);

        public Task<MessageResponse> EditUserProfile(UserUpdateModel model, string accessToken);

        public Task<DataCollectionModel<UserModel>> GetAllUsers(IndexRequestModel model);

        public Task<MessageResponse> BlockUser(UserLockoutModel model);

        public Task<MessageResponse> UnblockUser(UserLockoutModel model);
    }
}
