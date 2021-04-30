using BookStore.AdminPanel.Models.Base;

namespace BookStore.AdminPanel.Models
{
    public class JwtPairModel : BaseModel
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }
    }
}
