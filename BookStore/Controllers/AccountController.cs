using BookStore.BusinessLogicLayer.Models.RequestModels.User;
using BookStore.BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookStore.PresentationLayer.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserRegistrationModel model)
        {
            var response = await _accountService.Register(model);

            return new OkObjectResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLoginModel model)
        {
            var response = await _accountService.Login(model);

            return new OkObjectResult(response);
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail([FromQuery] UserEmailConfirmationModel model)
        {
            var response = await _accountService.ConfirmEmail(model);

            return new OkObjectResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> RefreshTokens([FromBody] UserRefreshTokensModel model)
        {
            var response = await _accountService.RefreshTokensAsync(model);

            return new OkObjectResult(response);
        }

        //[HttpPost]
        //public async Task<IActionResult> CheckEmail([FromBody] UserResetPasswordModel model)
        //{
        //    var response = await _accountService.CheckEmail(model);
        //    return new OkObjectResult(response);
        //}

        //[HttpPost]
        //public async Task<IActionResult> ResetPassword([FromBody] UserChangePasswordModel model)
        //{
        //    var response = await _accountService.ResetPassword(model);

        //    return Ok(response);
        //}

        //[HttpGet]
        //public async Task<IActionResult> ChangePassword([FromQuery] string userId, [FromQuery] string token, [FromQuery]string password)
        //{
        //    var response = await _accountService.ChangePassword(userId, token, password);

        //    return Ok(response);
        //}

        [HttpPost]
        public async Task<IActionResult> SendPasswordResetMail([FromBody] UserPasswordResetModel model)
        {
            var response = await _accountService.SendPasswordResetMail(model);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> CheckPasswordResetToken(string id, string token)
        {
            var response = await _accountService.CheckPasswordResetToken(id, token);

            return Ok(response);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> SetNewPassword(string id, [FromBody] UserChangePasswordModel model)
        {
            var response = await _accountService.SetNewPassword(id, model);

            return Ok(response);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            var name = User.Identity.Name;
            var response = await _accountService.Logout(name);

            return Ok(response);
        }
    }
}
