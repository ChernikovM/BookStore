using BookStore.BusinessLogicLayer.Models.Base;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BookStore.BusinessLogicLayer.Models.User
{
    public class UserChangePasswordModel : BaseModel
    {
        [Required]
        [FromBody]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [FromBody]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "The password and confirmation password dont match.")]
        public string ConfirmNewPassword { get; set; }
    }
}
