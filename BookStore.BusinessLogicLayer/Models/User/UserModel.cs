using BookStore.BusinessLogicLayer.Models.Base;
using System;

namespace BookStore.BusinessLogicLayer.Models.User
{
    public class UserModel : BaseModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public bool LockoutEnabled { get; set; }

        public DateTime? LockoutEnd { get; set; }
    }
}
