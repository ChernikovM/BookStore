using BookStore.BusinessLogicLayer.Models.Base;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BookStore.BusinessLogicLayer.Models.User
{
    public class UserChangePasswordModel : BaseModel
    {
        [Required]
        [FromQuery]
        public string UserId { get; set; }

        [Required]
        [FromQuery]
        public string Token { get; set; }

        [Required]
        [FromForm]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [FromForm]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "The password and confirmation password dont match.")]
        public string ConfirmNewPassword { get; set; }
    }
}
