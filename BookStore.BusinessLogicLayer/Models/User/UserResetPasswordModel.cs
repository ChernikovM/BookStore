using BookStore.BusinessLogicLayer.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace BookStore.BusinessLogicLayer.Models.User
{
    public class UserResetPasswordModel : BaseModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

    }
}
