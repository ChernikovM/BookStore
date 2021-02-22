using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BookStore.DataAccessLayer.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string RefreshToken { get; set; }

        [JsonIgnore]
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
