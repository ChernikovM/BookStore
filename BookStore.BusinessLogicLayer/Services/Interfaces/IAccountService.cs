using BookStore.BusinessLogicLayer.Models.RequestModels.User;
using BookStore.BusinessLogicLayer.Models.ResponseModels;
using BookStore.BusinessLogicLayer.Models.ResponseModels.User;
using BookStore.DataAccessLayer.Entities;
using System.Threading.Tasks;

namespace BookStore.BusinessLogicLayer.Services.Interfaces
{
    public interface IAccountService
    {
        Task<UserResponseModel> Register(UserRegistrationModel model);

        Task<JwtPairResponse> Login(UserLoginModel model);

        Task<MessageResponse> ConfirmEmail(UserEmailConfirmationModel model);

        Task<JwtPairResponse> RefreshTokens(UserRefreshTokensModel model);

        Task<MessageResponse> ResetPassword(UserChangePasswordModel model);

        Task<MessageResponse> CheckEmail(UserResetPasswordModel model);

        Task<MessageResponse> ChangePassword(string userId, string token, string password);

        Task<MessageResponse> Logout(string username);

        Task<User> FindByIdAsync(string id);

        Task<User> FindByNameAsync(string name);

        Task<User> FindByEmailAsync(string email);

        Task<User> FindByTokenAsync(string token);
    }
}
