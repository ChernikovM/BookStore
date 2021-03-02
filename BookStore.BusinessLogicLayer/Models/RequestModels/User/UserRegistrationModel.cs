using BookStore.BusinessLogicLayer.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace BookStore.BusinessLogicLayer.Models.RequestModels.User
{
    public class UserRegistrationModel : BaseErrorModel
    {
        [Required( AllowEmptyStrings = false, ErrorMessage = "FirstName is required." )]
        public string FirstName { get; set; }

        [Required( AllowEmptyStrings = false, ErrorMessage = "LastName is required." )]
        public string LastName { get; set; }

        [Required( AllowEmptyStrings = false, ErrorMessage = "UserName is required." )]
        public string UserName { get; set; }

        [Required( AllowEmptyStrings = false, ErrorMessage = "Email is required." )]
        [EmailAddress]
        public string Email { get; set; }

        [Required( AllowEmptyStrings = false, ErrorMessage = "Password is required." )]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password dont match.")]
        public string ConfirmPassword { get; set; }

    }
}
