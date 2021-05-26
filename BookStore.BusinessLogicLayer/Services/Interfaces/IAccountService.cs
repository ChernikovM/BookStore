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

        Task<JwtPairResponse> RefreshTokensAsync(UserRefreshTokensModel model);

        Task<MessageResponse> SendPasswordResetMail(UserPasswordResetModel model);

        Task<bool> CheckPasswordResetToken(string id, string token);

        Task<MessageResponse> SetNewPassword(string id, UserChangePasswordModel model);

        Task<MessageResponse> Logout(string username);

        Task<User> FindByIdAsync(string id);

        Task<User> FindByNameAsync(string name);

        Task<User> FindByEmailAsync(string email);

        Task<User> FindByTokenAsync(string token);
    }
}
