using BookStore.BusinessLogicLayer.Models.Base;

namespace BookStore.BusinessLogicLayer.Models.User
{
    public class UserResetPasswordModel : BaseModel
    {
        public string Email { get; set; }

    }
}
