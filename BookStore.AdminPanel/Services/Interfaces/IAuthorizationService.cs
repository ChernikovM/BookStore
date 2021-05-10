using BookStore.AdminPanel.Models;
using BookStore.BusinessLogicLayer.Models.ResponseModels;
using System.Threading.Tasks;

namespace BookStore.AdminPanel.Services.Interfaces
{
    public interface IAuthorizationService
    {
        public string AccessToken { get; }

        public string RefreshToken { get; }


        Task SignInAsync(JwtPairResponse jwtPair);

        Task SignOutAsync();

        string GetClaimValue(string type);

        bool IsAuthorized();
    }
}
