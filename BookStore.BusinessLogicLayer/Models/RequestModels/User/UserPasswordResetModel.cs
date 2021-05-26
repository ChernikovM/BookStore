using System.ComponentModel.DataAnnotations;

namespace BookStore.BusinessLogicLayer.Models.RequestModels.User
{
    public class UserPasswordResetModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Required]
        public string CallbackUrl { get; set; }
    }
}
