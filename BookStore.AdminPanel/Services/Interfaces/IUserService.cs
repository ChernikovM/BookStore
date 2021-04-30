using BookStore.AdminPanel.Models;
using BookStore.AdminPanel.Services.Base;
using System.Threading.Tasks;

namespace BookStore.AdminPanel.Services.Interfaces
{
    public interface IUserService : IApiService
    {
        Task<CollectionResponseModel<UserModel>> GetAllAsync(CollectionRequestModel model);
    }
}
