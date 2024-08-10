using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;

namespace Shop
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var context = new ShopDb())
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