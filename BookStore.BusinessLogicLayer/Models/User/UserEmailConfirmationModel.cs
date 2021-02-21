using BookStore.BusinessLogicLayer.Models.Base;

namespace BookStore.BusinessLogicLayer.Models.User
{
    public class UserEmailConfirmationModel : BaseModel
    {
        public string UserId { get; set; }

        public string Token { get; set; }
    }
}
