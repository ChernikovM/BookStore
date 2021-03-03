using System.ComponentModel.DataAnnotations;

namespace BookStore.BusinessLogicLayer.Models.RequestModels.User
{
    public class UserLockoutModel
    {
        [Required(AllowEmptyStrings = false)]
        public string Email { get; set; }
    }
}
