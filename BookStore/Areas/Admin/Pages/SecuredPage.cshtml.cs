using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookStore.PresentationLayer.Areas.Admin.Pages
{
    [Authorize(AuthenticationSchemes = "Cookies", Roles = "Admin")]
    public class SecuredPageModel : PageModel
    {
        public void OnGet()
        {
        }

        public void OnPost()
        {
            throw new Exception("pizda");
        }
    }
}
