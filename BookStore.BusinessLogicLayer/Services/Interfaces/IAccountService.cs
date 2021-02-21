using BookStore.BusinessLogicLayer.Models.Responses;
using BookStore.BusinessLogicLayer.Models.User;
using System.Threading.Tasks;

namespace BookStore.BusinessLogicLayer.Services.Interfaces
{
    public interface IAccountService
    {
        public Task<RegistrationResponse> Register(UserRegistrationModel model);

        public Task<LoginResponse> Login(UserLoginModel model);

        public Task<EmailConfirmationResponse> ConfirmEmail(UserEmailConfirmationModel model);
    }
}
