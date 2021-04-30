using BookStore.AdminPanel.Models;
using BookStore.AdminPanel.Services;
using BookStore.AdminPanel.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Threading.Tasks;

namespace BookStore.AdminPanel.Pages
{
    public class UserManagementModel : PageModel
    {
        private readonly IUserService _userService;

        public bool IsSuccessfull;

        public UserManagementModel(IUserService userService)
        {
            _userService = userService;

            IsSuccessfull = false;
        }

        public async Task OnGet()
        {
            var response = await _userService.GetAllAsync(new CollectionRequestModel() { PageRequestModel = new PageRequestModel() { PageSize = 5 } });

            IsSuccessfull = response is not null;
        }
    }
}
