using BookStore.BusinessLogicLayer.Models.RequestModels.User;
using BookStore.BusinessLogicLayer.Models.ResponseModels;
using BookStore.DataAccessLayer.Entities;
using System.Threading.Tasks;

namespace BookStore.BusinessLogicLayer.Services.Interfaces
{
    public interface IAccountService
    {
        Task<MessageResponse> Register(UserRegistrationModel model);

        Task<JwtPairResponse> Login(UserLoginModel model);

        Task<MessageResponse> ConfirmEmail(UserEmailConfirmationModel model);

        Task<JwtPairResponse> RefreshTokens(UserRefreshTokensModel model, string accessToken);

        Task<MessageResponse> ResetPassword(UserResetPasswordModel model);

        Task<MessageResponse> ChangePassword(string userId, string token, UserChangePasswordModel model);

        Task<MessageResponse> Logout(string accessToken);

        Task<User> FindByIdAsync(string id);

        Task<User> FindByNameAsync(string name);

        Task<User> FindByEmailAsync(string email);

        Task<User> FindByTokenAsync(string token);
    }
}
