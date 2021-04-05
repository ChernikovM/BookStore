using BookStore.BusinessLogicLayer.Models.RequestModels.User;
using BookStore.BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.PresentationLayer.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AccountsController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserRegistrationModel model)
        {
            var response = await _accountService.Register(model);

            return new OkObjectResult(response);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLoginModel model)
        {
            var response = await _accountService.Login(model);

            return new OkObjectResult(response);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail([FromQuery] UserEmailConfirmationModel model)
        {
            var response = await _accountService.ConfirmEmail(model);

            return new OkObjectResult(response);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> RefreshTokens([FromBody] UserRefreshTokensModel model)
        {
            /*
            HttpContext.Request.Headers.TryGetValue("Authorization", out var value);
            var accessToken = value.ToString().Split(" ").Last();
            */
            var response = await _accountService.RefreshTokens(model);

            return new OkObjectResult(response);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ResetPassword([FromBody] UserResetPasswordModel model)
        {
            var response = await _accountService.ResetPassword(model);

            return new OkObjectResult(response);
        }

        [AllowAnonymous]
        [HttpPatch]
        public async Task<IActionResult> ChangePassword([FromQuery] string userId, [FromQuery] string token, [FromBody]UserChangePasswordModel model)
        {
            var response = await _accountService.ChangePassword(userId, token, model);

            return new OkObjectResult(response);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Request.Headers.TryGetValue("Authorization", out var value);
            var accessToken = value.ToString().Split(" ").Last();
            var response = await _accountService.Logout(accessToken);

            //return new OkObjectResult(response);
            return Ok(response);
        }
    }
}
