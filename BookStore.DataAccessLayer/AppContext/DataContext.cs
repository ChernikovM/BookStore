using BookStore.DataAccessLayer.Entities;
using BookStore.DataAccessLayer.Extensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DataAccessLayer.AppContext
{
    public class DataContext : IdentityDbContext<User>
    {
        public DbSet<PrintingEdition> PrintingEditions { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Payment> Payments { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base( options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.InitialRoles();

            base.OnModelCreating(modelBuilder);
        }
    }
}
