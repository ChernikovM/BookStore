using BookStore.AdminPanel.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;

namespace BookStore.AdminPanel.Services
{
    public class CookieService : ICookieService
    {
        private IHttpContextAccessor _httpContextAccessor;

        public bool IsLogged => IsLoggedIn();

        public CookieService(IHttpContextAccessor accessor)
        {
            _httpContextAccessor = accessor;
        }

        public string Get(HttpRequest request, string name)
        {
            string result = null;
            try
            {
                request.Cookies.TryGetValue(name, out result);
            }
            catch (Exception)
            {
                return null;
            }

            return result;
        }

        public bool Remove(HttpResponse response, string name)
        {
            try
            {
                response.Cookies.Delete(name);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool Set(HttpResponse response, string name, string value)
        {
            try 
            {
                response.Cookies.Append(name, value);
            }
            catch(Exception)
            {
                return false;
            }

            return true;
        }

        private bool IsLoggedIn()
        {
            return _httpContextAccessor.HttpContext.User.HasClaim(x => x.Type == "Authorization");
        }
    }
}
