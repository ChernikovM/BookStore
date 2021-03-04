using BookStore.BusinessLogicLayer.Models.RequestModels;
using BookStore.BusinessLogicLayer.Models.RequestModels.User;
using BookStore.BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.PresentationLayer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        private string GetAccessToken()
        {
            HttpContext.Request.Headers.TryGetValue("Authorization", out var value);
            var accessToken = value.ToString().Split(" ").Last();

            return accessToken;
        }

        [Authorize]
        [HttpGet("Me")]
        public async Task<IActionResult> GetMyProfile()
        {
            var accessToken = GetAccessToken();
            var response = await _userService.GetMyProfile(accessToken);

            return new OkObjectResult(response);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserProfile(string id)
        {
            var accessToken = GetAccessToken();

            var response = await _userService.GetUserProfile(id, accessToken);
            return new OkObjectResult(response);
        }

        [Authorize("AdminOnly")]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers([FromBody] IndexRequestModel model)
        {
            var response = await _userService.GetAllUsers(model);

            return new OkObjectResult(response);
        }

        [Authorize]
        [HttpPut("{id?}")]
        public async Task<IActionResult> Update(string? id, [FromBody] UserUpdateModel model)
        {
            var accessToken = GetAccessToken();
            var response = await _userService.Update(id, model, accessToken);

            return new OkObjectResult(response);
        }

        [Authorize("AdminOnly")]
        [HttpPost("{id}/ban/{days?}")]
        public async Task<IActionResult> BlockUser(string id, int? days)
        {
            var response = await _userService.BlockUser(id, days);

            return new OkObjectResult(response);
        }

        [Authorize("AdminOnly")]
        [HttpPost("{id}/unban")]
        public async Task<IActionResult> UnblockUser(string id)
        {
            var response = await _userService.UnblockUser(id);

            return new OkObjectResult(response);
        }
    }
}
