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
                Console.WriteLine("If you want to quit, write 'exit'");
                while (true)
                {
                    Console.WriteLine("Enter name of the product:");
                    string productName = Console.ReadLine();

                    int quantity = 0;
                    decimal price = 0.0m;
                    try
                    {
                        Console.WriteLine("Enter quantity of the product:");
                        quantity = int.Parse(Console.ReadLine());

                        Console.WriteLine("Enter price of the product:");
                        price = decimal.Parse(Console.ReadLine());
                    }
                    catch
                    {
                        break;
                    }

                    Console.WriteLine("Enter description of the product:");
                    string description = Console.ReadLine();

                    if (productName == "exit" || description == "exit")
                    {
                        break;
                    }

                    var product = new Product()
                    {
                        Name = productName,
                        Quantity = quantity,
                        Price = price,
                        Description = description
                    };

                    context.Products.Add(product);
                    context.SaveChanges();
                }
            }
        }
    }
}