using BookStore.BusinessLogicLayer.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace BookStore.BusinessLogicLayer.Models.User
{
    public class UserEmailConfirmationModel : BaseModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string Token { get; set; }
    }
}
