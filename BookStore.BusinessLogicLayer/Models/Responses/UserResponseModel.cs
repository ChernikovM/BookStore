using BookStore.BusinessLogicLayer.Models.Base;

namespace BookStore.BusinessLogicLayer.Models.Responses
{
    public class UserResponseModel : BaseModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
    }
}
