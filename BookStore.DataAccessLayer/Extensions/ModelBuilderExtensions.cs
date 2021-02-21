using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using static BookStore.DataAccessLayer.Enums.Enums;

namespace BookStore.DataAccessLayer.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void InitialRoles(this ModelBuilder modelBuilder)
        {
            IList<IdentityRole> roles = new List<IdentityRole>();

            foreach (Roles role in Enum.GetValues(typeof(Roles)))
            {
                roles.Add( new IdentityRole() { 
                    Id = Guid.NewGuid().ToString(), 
                    Name = role.ToString(), 
                    NormalizedName = role.ToString().ToUpper()
                });
            }

            modelBuilder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
