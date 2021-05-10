using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;


namespace BookStore.PresentationLayer.Areas.Admin.Pages
{
    [Authorize(AuthenticationSchemes = ("Cookies"), Roles = "Admin")]
    public class IndexModel : PageModel
    {
        public IndexModel()
        {

        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            
        }
    }
}
