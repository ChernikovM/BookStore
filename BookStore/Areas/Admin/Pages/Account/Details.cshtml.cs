using BookStore.BusinessLogicLayer.Exceptions;
using BookStore.BusinessLogicLayer.Models.RequestModels.User;
using BookStore.BusinessLogicLayer.Models.ResponseModels.User;
using BookStore.BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.PresentationLayer.Areas.Admin.Pages.Account
{
    [Authorize(Policy = "AdminCookiePolicy")]
    public class DetailsModel : PageModel
    {
        public string ErrorMessage { get; set; }
        public string ResultMessage { get; set; }

        [BindProperty]
        public UserUpdateModel UpdateModel { get; set; }

        public bool MyAccount { get; set; } = true;

        private readonly IUserService _userService;

        public DetailsModel(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> OnGetAsync(string id = null)
        {
            UserResponseModel userModel;

            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            try
            {
                userModel = await (id is null? 
                    _userService.GetMyProfile(User.Identity.Name) : 
                    _userService.GetUserProfile(id));
            }
            catch (CustomException ex)
            {
                //ErrorMessage = ex.ErrorMessages.First();
                ModelState.AddModelError(string.Empty, ex.ErrorMessages.First());
                return Page();
            }
            
            UpdateModel = new UserUpdateModel() {
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                Email = userModel.Email,
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id = null)
        {
            try
            {
                var response = await (id is null ? 
                    _userService.UpdateMyProfile(UpdateModel, User.Identity.Name) : 
                    _userService.UpdateUserProfile(id, UpdateModel, User.Identity));

                if(response.Errors.Count == 0)
                {
                    ResultMessage = response.Message;
                }
            }
            catch (CustomException ex)
            {
                ErrorMessage = ex.ErrorMessages.First();
                ResultMessage = null;
                return await OnGetAsync(id);
            }

            return Page();
        }
    }
}
