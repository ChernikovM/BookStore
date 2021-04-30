using BookStore.AdminPanel.Models;
using BookStore.AdminPanel.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace BookStore.AdminPanel.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IAccountService _accountService;
        private readonly IAuthorizationService _authorizationService;

        public JwtPairModel jwtModel { get; set; }

        public LoginModel(IAccountService accountService, IAuthorizationService authorizationService)
        {
            _accountService = accountService;
            _authorizationService = authorizationService;
            jwtModel = new JwtPairModel()
            {
                AccessToken = _authorizationService.GetClaimValue(_authorizationService.AccessToken),
                RefreshToken = _authorizationService.GetClaimValue(_authorizationService.RefreshToken),
            };
        }

        public void OnGet()
        {

        }

        public async Task OnPost()
        {
            var model = new SignInModel()
            {
                UserName = Request.Form["username"],
                Password = Request.Form["password"]
            };

            var response = await _accountService.LoginAsync(model);

            if(response.Errors.Count > 0)
            {
                return;
            }

            await _authorizationService.SignInAsync(response);

            

            jwtModel = response;
            //Navigate
            RedirectToPage("/UsersManagement");
        }

        public async Task OnGetSignOut()
        {
            await _authorizationService.SignOutAsync();
            RedirectToPage("/Login");
        }

        public async Task OnGetSetInvalidToken()
        {
            var invalidPair = jwtModel;
            invalidPair.AccessToken = "invalid";
            jwtModel = invalidPair;
            await _authorizationService.SignInAsync(invalidPair);
        }

        public async Task OnGetSetInvalidRefreshToken()
        {
            var invalidPair = jwtModel;
            invalidPair.RefreshToken = "invalid";
            jwtModel = invalidPair;
            await _authorizationService.SignInAsync(invalidPair);
        }
    }
}
