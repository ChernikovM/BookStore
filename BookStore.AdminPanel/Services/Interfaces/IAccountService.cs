using BookStore.AdminPanel.Models;
using BookStore.AdminPanel.Services.Base;
using System.Threading.Tasks;

namespace BookStore.AdminPanel.Services.Interfaces
{
    public interface IAccountService: IApiService
    {
        Task<JwtPairModel> LoginAsync(SignInModel model);

        Task<JwtPairModel> RefreshTokenPairAsync(string refreshToken);

    }
}
