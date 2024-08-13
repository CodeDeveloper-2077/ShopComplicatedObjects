using Microsoft.Data.SqlClient.DataClassification;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;
using System.Threading.Channels;

namespace Shop
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var context = new ShopDb())
            {
                //var products = new Product[]
                //{
                //    new Product()
                //    {
                //        Name = "Apple",
                //        Description = "Just a green apple",
                //        Price = 0.3m,
                //        Quantity = 0
                //    },
                //    new Product()
                //    {
                //        Name = "Banana",
                //        Description = "Yellow banana",
                //        Price = 0.25m,
                //        Quantity = 0
                //    },
                //    new Product()
                //    {
                //        Name = "Watermelon",
                //        Description = "Juicy watermelon",
                //        Price = 0.633m,
                //        Quantity = 0
                //    }
                //};

                //context.Products.AddRange(products);
                //context.SaveChanges();

                while (true)
                {
                    Console.WriteLine("Enter 'start' if u want to create an order");
                    string action = Console.ReadLine().ToLowerInvariant();

                    switch (action)
                    {
                        case "start":
                            {
                                var order = new Order();
                                order.OrderDate = DateTime.Now;
                                order.OrderNumber = new Random().Next(1, 1000000);

                                context.Orders.Add(order);
                                context.SaveChanges();

                                Console.WriteLine("Enter which products you would like to add");
                                string[] productNames = Console.ReadLine().Split(' ');

                                var orderDetails = new OrderDetails();
                                orderDetails.Order = order;

                                foreach (var productName in productNames)
                                {
                                    var product = context.Products.FirstOrDefault(p => p.Name == productName);

                                    if (product is not null && !orderDetails.Products.Contains(product))
                                    {
                                        orderDetails.Products.Add(product);
                                    }

                                    product.Quantity++;
                                }

                                foreach (var product in orderDetails.Products)
                                {
                                    orderDetails.Price += product.Price * product.Quantity;
                                }

                                context.OrderDetails.Add(orderDetails);
                                context.SaveChanges();
                                break;
                            }
                        case "stop":
                            {
                                return;
                            }
                        default:
                            {
                                Console.WriteLine("Unknown command.");
                                break;
                            }
                    }
                }
            }
        }
    }
}