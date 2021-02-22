using BookStore.BusinessLogicLayer.Models.User;
using BookStore.BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.PresentationLayer.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AccountController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register([FromForm] UserRegistrationModel model)
        {
            var response = await _accountService.Register(model);

            return new OkObjectResult(response);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromForm] UserLoginModel model)
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
        public async Task<IActionResult> RefreshTokens([FromQuery] UserRefreshTokensModel model, [FromHeader] string authorization)
        {
            var accessToken = authorization.Split(" ").Last();
            var response = await _accountService.RefreshTokens(model, accessToken);

            return new OkObjectResult(response);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ResetPassword([FromBody] UserResetPasswordModel model)
        {
            var response = await _accountService.ResetPassword(model);

            return new OkObjectResult(response);
        }
    }
}
