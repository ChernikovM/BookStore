using BookStore.BusinessLogicLayer.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace BookStore.BusinessLogicLayer.Models.RequestModels.User
{
    public class UserRefreshTokensModel : BaseErrorModel
    {
        [Required(AllowEmptyStrings = false)]
        public string RefreshToken { get; set; }
    }
}
