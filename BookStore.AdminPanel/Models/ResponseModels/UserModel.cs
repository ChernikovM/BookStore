using BookStore.AdminPanel.Models.Base;
using System;

namespace BookStore.AdminPanel.Models
{
    public class UserModel : BaseModel
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public bool LockoutEnabled { get; set; }

        public DateTimeOffset? LockoutEnd { get; set; }
    }
}
