using System;

namespace BookStore.BusinessLogicLayer.Models.ResponseModels.User
{
    public class UserResponseModelForAdmin : UserResponseModel
    {
        public bool LockoutEnabled { get; set; }

        public DateTime? LockoutEnd { get; set; }
    }
}
