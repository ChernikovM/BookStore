using BookStore.BusinessLogicLayer.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace BookStore.BusinessLogicLayer.Models.RequestModels.User
{
    public class UserRefreshTokensModel : BaseErrorModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Refresh Token field is required.")]
        public string RefreshToken { get; set; }
    }
}
