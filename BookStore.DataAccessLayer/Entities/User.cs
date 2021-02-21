using Microsoft.AspNetCore.Identity;

namespace BookStore.DataAccessLayer.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
