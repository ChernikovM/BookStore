using BookStore.BusinessLogicLayer.Models.RequestModels.User;
using BookStore.BusinessLogicLayer.Providers.Interfaces;
using BookStore.BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookStore.PresentationLayer.Areas.Admin.Pages.Account
{
    public class LoginModel : PageModel
    {
        [TempData]
        public string ErrorMessage { get; set; }

        [BindProperty]
        public UserLoginModel LoginUserModel { get; set; }

        public string ReturnUrl { get; set; }

        private readonly IAccountService _accountService;
        private readonly IAuthService _authService;
        private readonly IJwtProvider _jwtProvider;

        public LoginModel(IAccountService accountService, IAuthService authService, IJwtProvider jwtProvider)
        {
            _accountService = accountService;
            _authService = authService;
            _jwtProvider = jwtProvider;
        }


        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/Admin/SecuredPage");

            if (ModelState.IsValid)
            {
                var result = await _accountService.Login(LoginUserModel);

                if (result is not null)
                {
                    var claims = _jwtProvider.GetClaimsFromToken(result.AccessToken).ToList();

                    claims.Add(new Claim("AccessToken", result.AccessToken));
                    claims.Add(new Claim("RefreshToken", result.RefreshToken));

                    await _authService.SignInAsync(Request.HttpContext, claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    return LocalRedirect(returnUrl);
                }
            }
            return Page();
        }
    }
}
