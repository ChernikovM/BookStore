using BookStore.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using static BookStore.DataAccessLayer.Enums.Enums;

namespace BookStore.DataAccessLayer.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static IList<IdentityRole> InitialRoles(this ModelBuilder modelBuilder)
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
            return roles;
        }

        public static User InitialAdminUser(this ModelBuilder modelBuilder)
        {
            var admin = new User()
            {
                UserName = "admin",
                Email = "storeanager45@gmail.com",
                EmailConfirmed = true,
                FirstName = "BookStore",
                LastName = "Administrator",
                Id = Guid.NewGuid().ToString(),
            };

            var hasher = new  PasswordHasher<IdentityUser>();
            admin.PasswordHash = hasher.HashPassword(admin, "12_OneTwo");

            admin.NormalizedUserName = admin.UserName.ToUpper();
            admin.NormalizedEmail = admin.Email.ToUpper();

            modelBuilder.Entity<User>().HasData(admin);
            return admin;
        }

        public static void InitialUserRole(this ModelBuilder modelBuilder, User user, IdentityRole role)
        {
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>()
            {
                RoleId = role.Id,
                UserId = user.Id,

            });
        }
    }
}
