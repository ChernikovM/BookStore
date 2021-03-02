using BookStore.BusinessLogicLayer.Models.RequestModels;
using BookStore.BusinessLogicLayer.Models.RequestModels.User;
using BookStore.BusinessLogicLayer.Models.ResponseModels;
using BookStore.BusinessLogicLayer.Models.ResponseModels.User;
using System.Threading.Tasks;

namespace BookStore.BusinessLogicLayer.Services.Interfaces
{
    public interface IUserService
    {
        public Task<UserResponseModel> GetMyProfile(string accessToken);

        public Task<MessageResponse> UpdateMyProfile(UserUpdateModel model, string accessToken);

        public Task<UserResponseModelForAdmin> GetUserProfile(string email);

        public Task<MessageResponse> EditUserProfile(UserUpdateModel model, string accessToken);

        public Task<DataCollectionModel<UserResponseModelForAdmin>> GetAllUsers(IndexRequestModel model);

        public Task<MessageResponse> BlockUser(UserLockoutModel model);

        public Task<MessageResponse> UnblockUser(UserLockoutModel model);
    }
}
