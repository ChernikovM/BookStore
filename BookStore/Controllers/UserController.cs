using BookStore.BusinessLogicLayer.Models.RequestModels;
using BookStore.BusinessLogicLayer.Models.RequestModels.User;
using BookStore.BusinessLogicLayer.Services.Interfaces;
using BookStore.DataAccessLayer.Entities;
using BookStore.PresentationLayer.Controllers.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.PresentationLayer.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetMyProfile()
        {
            HttpContext.Request.Headers.TryGetValue("Authorization", out var value);
            var accessToken = value.ToString().Split(" ").Last();
            var response = await _userService.GetMyProfile(accessToken);

            return new OkObjectResult(response);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Update([FromBody] UserUpdateModel model)
        {
            HttpContext.Request.Headers.TryGetValue("Authorization", out var value);
            var accessToken = value.ToString().Split(" ").Last();
            var response = await _userService.UpdateMyProfile(model, accessToken);

            return new OkObjectResult(response);
        }

        [Authorize("AdminOnly")]
        [HttpGet]
        public async Task<IActionResult> GetUserProfile([FromQuery] string email)
        {
            var response = await _userService.GetUserProfile(email);
            return new OkObjectResult(response);
        }

        [Authorize("AdminOnly")]
        [HttpPost]
        public async Task<IActionResult> EditProfile([FromBody]UserUpdateModel model)
        {
            HttpContext.Request.Headers.TryGetValue("Authorization", out var value);
            var accessToken = value.ToString().Split(" ").Last();

            var response = await _userService.EditUserProfile(model, accessToken);
            return new OkObjectResult(response);
        }

        [Authorize("AdminOnly")]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers([FromBody]IndexRequestModel model)
        {
            var response = await _userService.GetAllUsers(model);

            return new OkObjectResult(response);
        }

        [Authorize("AdminOnly")]
        [HttpGet]
        public async Task<IActionResult> BlockUser([FromBody] UserLockoutModel model)
        {
            var response = await _userService.BlockUser(model);

            return new OkObjectResult(response);
        }

        [Authorize("AdminOnly")]
        [HttpGet]
        public async Task<IActionResult> UnblockUser([FromBody] UserLockoutModel model)
        {
            var response = await _userService.UnblockUser(model);

            return new OkObjectResult(response);
        }
    }
}
