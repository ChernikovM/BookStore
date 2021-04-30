using BookStore.AdminPanel.Models;
using BookStore.AdminPanel.Services.Interfaces;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.AdminPanel.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpService _http;

        public UserService(IHttpService http)
        {
            _http = http;
        }

        public string ApiUrl => "https://localhost:44366/Users";

        public async Task<CollectionResponseModel<UserModel>> GetAllAsync(CollectionRequestModel model)
        {
            var result = await _http.SendAsync(HttpMethod.Post, ApiUrl, model, true);

            return JsonConvert.DeserializeObject<CollectionResponseModel<UserModel>>(await result.Content.ReadAsStringAsync());
        }
    }
}
