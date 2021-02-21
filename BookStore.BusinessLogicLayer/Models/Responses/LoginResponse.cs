using BookStore.BusinessLogicLayer.Models.Base;

namespace BookStore.BusinessLogicLayer.Models.Responses
{
    public class LoginResponse : BaseModel
    {
        public string AccessToken { get; set; }
        
        public string RefreshToken { get; set; }

    }
}
