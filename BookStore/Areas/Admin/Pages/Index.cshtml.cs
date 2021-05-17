using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace BookStore.PresentationLayer.Areas.Admin.Pages
{
    [Authorize(Policy = "AdminCookiePolicy")]
    public class IndexModel : PageModel
    {
        public IndexModel()
        {

        }

        public IActionResult OnGet(string returnUrl = null)
        {
            return RedirectToPage("Account/Details");
        }
    }
}
