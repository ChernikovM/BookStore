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

        public Task<MessageResponse> Update(string? id, UserUpdateModel model, string accessToken);

        public Task<UserResponseModel> GetUserProfile(string id, string accessToken);

        public Task<DataCollectionModel<UserResponseModel>> GetAllUsers(IndexRequestModel model);

        public Task<MessageResponse> BlockUser(string id, int? days);

        public Task<MessageResponse> UnblockUser(string id);
    }
}
