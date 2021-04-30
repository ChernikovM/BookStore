using Microsoft.AspNetCore.Http;

namespace BookStore.AdminPanel.Services.Interfaces
{
    public interface ICookieService
    {
        public bool IsLogged { get; }

        public bool Set(HttpResponse response, string name, string value);

        public string Get(HttpRequest request, string name);

        public bool Remove(HttpResponse request, string name);
    }
}
