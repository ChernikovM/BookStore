using BookStore.BusinessLogicLayer.Models.User;
using BookStore.BusinessLogicLayer.Services.Interfaces;
using BookStore.DataAccessLayer.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.PresentationLayer.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetMyProfile([FromHeader]string authorization)
        {
            var accessToken = authorization.Split(" ").Last();
            var response = await _userService.GetMyProfile(accessToken);

            return new OkObjectResult(response);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Update([FromHeader] string authorization, [FromBody] UserUpdateModel model)
        { 
            var accessToken = authorization.Split(" ").Last();
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
        public async Task<IActionResult> EditProfile([FromBody]UserUpdateModel model, [FromHeader] string authorization)
        {
            var accessToken = authorization.Split(" ").Last();

            var response = await _userService.EditUserProfile(model, accessToken);
            return new OkObjectResult(response);
        }
    }
}
