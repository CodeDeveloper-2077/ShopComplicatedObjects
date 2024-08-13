using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using Shop.Models;

namespace Shop.Data
{
    public class ShopDb : DbContext
    {
        //public virtual DbSet<Order> Orders { get; set; }

        //public virtual DbSet<OrderDetails> OrderDetails { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Shop;Integrated Security=True;");
        }
    }
}
