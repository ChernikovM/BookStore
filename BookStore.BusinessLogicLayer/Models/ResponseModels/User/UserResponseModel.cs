using BookStore.BusinessLogicLayer.Models.Base;
using System;

namespace BookStore.BusinessLogicLayer.Models.ResponseModels.User
{
    public class UserResponseModel : BaseErrorModel
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public bool LockoutEnabled { get; set; }

        public DateTime? LockoutEnd { get; set; }
    }
}
