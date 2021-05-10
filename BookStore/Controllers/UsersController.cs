using BookStore.BusinessLogicLayer.Models.RequestModels;
using BookStore.BusinessLogicLayer.Models.RequestModels.User;
using BookStore.BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookStore.PresentationLayer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetMyProfile()
        {
            var username = User.Identity.Name;
            var response = await _userService.GetMyProfile(username);

            return new OkObjectResult(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserProfile(string id)
        {
            var response = await _userService.GetUserProfile(id);

            return new OkObjectResult(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> GetAllUsers([FromBody] IndexRequestModel model)
        {
            var response = await _userService.GetAllUsers(model);

            return new OkObjectResult(response);
        }

        [Authorize]
        [HttpPatch]
        public async Task<IActionResult> UpdateMyProfile([FromBody] UserUpdateModel model)
        {
            var username = User.Identity.Name;
            
            var response = await _userService.UpdateMyProfile(model, username);

            return new OkObjectResult(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateUserProfile(string id, [FromBody] UserUpdateModel model)
        {
            var userIdentity = User.Identity;

            var response = await _userService.UpdateUserProfile(id, model, userIdentity);

            return new OkObjectResult(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("{id}/ban/{days?}")]
        public async Task<IActionResult> BlockUser(string id, int? days)
        {
            var response = await _userService.BlockUser(id, days);

            return new OkObjectResult(response);
        }

        [Authorize(AuthenticationSchemes = "AdminCookiePolicy", Roles = "Admin")]
        [HttpPost("{id}/unban")]
        public async Task<IActionResult> UnblockUser(string id)
        {
            var response = await _userService.UnblockUser(id);

            return new OkObjectResult(response);
        }
    }
}
