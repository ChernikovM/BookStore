using BookStore.BusinessLogicLayer.Models.RequestModels;
using BookStore.BusinessLogicLayer.Models.RequestModels.User;
using BookStore.BusinessLogicLayer.Models.ResponseModels;
using BookStore.BusinessLogicLayer.Models.ResponseModels.User;
using System.Security.Principal;
using System.Threading.Tasks;

namespace BookStore.BusinessLogicLayer.Services.Interfaces
{
    public interface IUserService
    {
        public Task<UserResponseModel> GetMyProfile(string username);

        public Task<MessageResponse> UpdateMyProfile(UserUpdateModel model, string username);

        public Task<MessageResponse> UpdateUserProfile(string id, UserUpdateModel model, IIdentity userIdentity);

        public Task<UserResponseModel> GetUserProfile(string id);

        public Task<DataCollectionModel<UserResponseModel>> GetAllUsers(IndexRequestModel model);

        public Task<MessageResponse> BlockUser(string id, int? days);

        public Task<MessageResponse> UnblockUser(string id);
    }
}
