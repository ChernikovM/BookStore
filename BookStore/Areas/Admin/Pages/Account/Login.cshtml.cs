using BookStore.BusinessLogicLayer.Exceptions;
using BookStore.BusinessLogicLayer.Models.RequestModels.User;
using BookStore.BusinessLogicLayer.Models.ResponseModels;
using BookStore.BusinessLogicLayer.Providers.Interfaces;
using BookStore.BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookStore.PresentationLayer.Areas.Admin.Pages.Account
{
    public class LoginModel : Microsoft.AspNetCore.Mvc.RazorPages.PageModel
    {
        
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


        public async Task<IActionResult> OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/Admin/Account/Details");
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            ReturnUrl = returnUrl;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/Admin/Account/Details");

            if (ModelState.IsValid)
            {
                JwtPairResponse result;

                try
                {
                    result = await _accountService.Login(LoginUserModel);
                }
                catch (CustomException ex)
                {
                    ErrorMessage = ex.ErrorMessages.First();
                    return await OnGetAsync(returnUrl);
                }
                //catch (Exception ex)
                //{
                //    RedirectToPage("SecuredPage");
                //    return null;
                //}

                if (result.AccessToken is not null && result.RefreshToken is not null)
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
