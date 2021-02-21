using AutoMapper;
using BookStore.BusinessLogicLayer.Exceptions;
using BookStore.BusinessLogicLayer.Models.Responses;
using BookStore.BusinessLogicLayer.Models.User;
using BookStore.BusinessLogicLayer.Services.Interfaces;
using BookStore.DataAccessLayer.Entities;
using BookStore.DataAccessLayer.Enums;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookStore.BusinessLogicLayer.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IEmailSenderService _emailSenderService;
        private readonly IJwtService _jwtService;

        public AccountService(
            UserManager<User> userManager, 
            IMapper mapper, 
            IEmailSenderService emailSenderService, 
            IJwtService jwtService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _emailSenderService = emailSenderService;
            _jwtService = jwtService;
        }

        public async Task<RegistrationResponse> Register(UserRegistrationModel model)
        {
            var newUser = _mapper.Map<User>(model);

            var result = await _userManager.CreateAsync(newUser, model.Password);

            if (!result.Succeeded)
            {
                throw new CustomException(HttpStatusCode.BadRequest, result);
            }

            await _userManager.AddToRoleAsync(newUser, Enums.Roles.User.ToString());

            await _emailSenderService.SendEmailConfirmationLinkAsync(newUser);

            return new RegistrationResponse() { Response = "Please confirm your email."};
        }
        
        public async Task<EmailConfirmationResponse> ConfirmEmail(UserEmailConfirmationModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);

            if (user is null)
            {
                throw new CustomException(HttpStatusCode.BadRequest, "User was not found.");
            }

            var confirmationResult = await _userManager.ConfirmEmailAsync(user, model.Token);

            if (!confirmationResult.Succeeded)
            {
                throw new CustomException(HttpStatusCode.BadRequest, "Invalid token.");
            }

            await SetClaims(user);

            return new EmailConfirmationResponse() { Response = "Registration successfully completed." };
        }

        private async Task SetClaims(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, _userManager.GetRolesAsync(user).Result.First()),

            };

            await _userManager.AddClaimsAsync(user, claims);
        }

        public async Task<LoginResponse> Login(UserLoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user is null)
            {
                throw new CustomException(HttpStatusCode.BadRequest, "Invalid credentials.");
            }

            bool checkPassword = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!checkPassword)
            {
                throw new CustomException(HttpStatusCode.BadRequest, "Invalid credentials.");
            }

            var checkEmailConfirmation = await _userManager.IsEmailConfirmedAsync(user);
            if (!checkEmailConfirmation)
            {
                throw new CustomException(System.Net.HttpStatusCode.Unauthorized, "Email not confirmed.");
            }

            var userClaims = await _userManager.GetClaimsAsync(user);

            var tokenPair = _jwtService.GenerateTokenPair(userClaims);

            //_userManager.SetAuthenticationTokenAsync<>();

            return tokenPair;
        }

    }
}
