using BookStore.BusinessLogicLayer.Models.RequestModels.User;
using BookStore.BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;


namespace BookStore.PresentationLayer.Areas.Admin.Pages
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        [TempData]
        public string ErrorMessage { get; set; }

        [BindProperty]
        public UserLoginModel LoginModel { get; set; }

        public string ReturnUrl { get; set; }

        private readonly IAccountService _accountService;
        private readonly AdminPanel.Services.Interfaces.IAuthorizationService _authService;
        public IndexModel(IAccountService accountService, AdminPanel.Services.Interfaces.IAuthorizationService authService)
        {
            _accountService = accountService;
            _authService = authService;
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
                var result = await _accountService.Login(LoginModel);

                if (result is not null)
                {
                    await _authService.SignInAsync(result);

                    return LocalRedirect(returnUrl);
                }
            }
            return Page();
        }
    }
}
