using Microsoft.EntityFrameworkCore;
using ShopApp.Data.Entities;

namespace ShopApp.Data.Context
{
    public class ShopDbContext : DbContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options)
        {
            
        }

        public DbSet<Category> Categories { get; set; }
        //public DbSet<Customer> Customers { get; set; }
    }
}
