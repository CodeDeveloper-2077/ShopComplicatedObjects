using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;

namespace Shop
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var optionsBuilder = new DbContextOptionsBuilder<ShopDb>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Shop;Integrated Security=True;");

            using (var context = new ShopDb(optionsBuilder.Options))
            {
                var order = new Order()
                {
                    OrderDate = DateTime.Now
                };

                var products = new Product[]
                {
                    new Product()
                    {
                        Name = "Apple",
                        Count = 3,
                        PricePerOne = 0.63m,
                    },

                    new Product()
                    {
                        Name = "Banana",
                        PricePerOne = 0.92m
                    }
                };


                var orderDetails = new OrderDetails()
                {
                    Order = order
                };

                orderDetails.Products?.AddRange(products);

                context.Orders.Add(order);
                context.Products.AddRange(products);
                context.OrderDetails.Add(orderDetails);

                context.SaveChanges();
            }
        }
    }
}