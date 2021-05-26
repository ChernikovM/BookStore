using BookStore.BusinessLogicLayer.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace BookStore.BusinessLogicLayer.Models.RequestModels.User
{
    public class UserChangePasswordModel : BaseErrorModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "The password and confirmation password dont match.")]
        public string ConfirmNewPassword { get; set; }

        [Required]
        public string Token { get; set; }
    }
}
