using BookStore.BusinessLogicLayer.Models.Responses;
using BookStore.BusinessLogicLayer.Models.User;
using System.Threading.Tasks;

namespace BookStore.BusinessLogicLayer.Services.Interfaces
{
    public interface IAccountService
    {
        public Task<RegistrationResponse> Register(UserRegistrationModel model);

        public Task<LoginResponse> Login(UserLoginModel model); //TODO: сделать другое возвращ знач - JwtPairResponse 

        public Task<EmailConfirmationResponse> ConfirmEmail(UserEmailConfirmationModel model);

        public Task<LoginResponse> RefreshTokens(UserRefreshTokensModel model, string accessToken); //TODO: сделать другое возвращ знач - JwtPairResponse 
    }
}
