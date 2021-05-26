using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BookStore.DataAccessLayer.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string RefreshToken { get; set; }

        public string PasswordResetToken { get; set; }

        public List<Order> Orders { get; set; }

        public User()
        {
            Orders = new List<Order>();
        }
    }
}
