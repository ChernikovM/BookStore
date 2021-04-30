using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Threading.Tasks;

namespace BookStore.AdminPanel.Pages
{
    public class PrintingEditionsManagementModel : PageModel
    {


        public async Task OnGet()
        {
            var t = new HttpClient();
            var response = await t.GetAsync("https://localhost:44366/PrintingEditions/15");
            var tt = Request;
        }
    }
}
