using BookStore.BusinessLogicLayer.Models.Base;

namespace BookStore.BusinessLogicLayer.Models.User
{
    public class UserRefreshTokensModel : BaseModel
    {
        public string RefreshToken { get; set; }
    }
}
