using BookStore.BusinessLogicLayer.Models.Base;

namespace BookStore.BusinessLogicLayer.Models.RequestModels.User
{
    public class UserRefreshTokensModel : BaseErrorModel
    {
        public string RefreshToken { get; set; }
    }
}
