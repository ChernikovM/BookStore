using BookStore.BusinessLogicLayer.Models.Responses;
using BookStore.BusinessLogicLayer.Models.User;
using System.Threading.Tasks;

namespace BookStore.BusinessLogicLayer.Services.Interfaces
{
    public interface IAccountService
    {
        public Task<MessageResponse> Register(UserRegistrationModel model);

        public Task<JwtPairResponse> Login(UserLoginModel model);

        public Task<MessageResponse> ConfirmEmail(UserEmailConfirmationModel model);

        public Task<JwtPairResponse> RefreshTokens(UserRefreshTokensModel model, string accessToken);

        public Task<MessageResponse> ResetPassword(UserResetPasswordModel model);

        public Task<MessageResponse> ChangePassword(string userId, string token, UserChangePasswordModel model);

        public Task<MessageResponse> Logout(string accessToken);
    }
}
