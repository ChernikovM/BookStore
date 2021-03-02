using BookStore.BusinessLogicLayer.Models.Base;

namespace BookStore.BusinessLogicLayer.Models.ResponseModels
{
    public class JwtPairResponse : BaseErrorModel
    {
        public string AccessToken { get; set; }
        
        public string RefreshToken { get; set; }

    }
}
