using System.Net.Http;
using System.Threading.Tasks;

namespace BookStore.AdminPanel.Services.Interfaces
{
    public interface IHttpService
    {
        Task<HttpResponseMessage> SendAsync(HttpMethod method, string url, object body = null, bool addJwtHeader = false);

        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);
    }
}
