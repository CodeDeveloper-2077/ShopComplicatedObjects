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
                int action = 0;
                while (true)
                {
                    Console.WriteLine("1.See all products");
                    Console.WriteLine("2.Create new product");
                    Console.WriteLine("3.Update product");
                    Console.WriteLine("4.Remove product");
                    Console.WriteLine("5.Exit");

                    action = int.Parse(Console.ReadLine());
                    switch (action)
                    {
                        case 1:
                            {
                                Console.WriteLine("Name|Description|Quantity|Price");
                                foreach (var product in context.Products)
                                {
                                    Console.WriteLine($"{product.Name}|{product.Description}|{product.Quantity}|{product.Price}");
                                }

                                break;
                            }
                        case 2:
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
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"Error occurred: {ex.Message}");
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
                                break;
                            }
                        case 3:
                            {
                                Console.WriteLine("Enter name of the product which you would like to update");
                                
                                string productName = Console.ReadLine();
                                var product = context.Products.FirstOrDefault(p => p.Name == productName);
                                if (product is null)
                                {
                                    Console.WriteLine("Current product doesn't exist");
                                    return;
                                }

                                int property = 0;
                                while (true)
                                {
                                    Console.WriteLine("1.Name");
                                    Console.WriteLine("2.Description");
                                    Console.WriteLine("3.Quantity");
                                    Console.WriteLine("4.Price");
                                    Console.WriteLine("5.Exit");

                                    property = int.Parse(Console.ReadLine());
                                    switch (property)
                                    {
                                        case 1:
                                            {
                                                Console.WriteLine("Enter new name");
                                                product.Name = Console.ReadLine();
                                                break;
                                            }
                                        case 2:
                                            {
                                                Console.WriteLine("Enter new description");
                                                product.Description = Console.ReadLine();
                                                break;
                                            }
                                        case 3:
                                            {
                                                try
                                                {
                                                    Console.WriteLine("Enter new quantity");
                                                    product.Quantity = int.Parse(Console.ReadLine());
                                                }
                                                catch (Exception ex)
                                                {
                                                    Console.WriteLine($"Error occurred: {ex.Message}");
                                                    return;
                                                }

                                                break;
                                            }
                                        case 4:
                                            {
                                                try
                                                {
                                                    Console.WriteLine("Enter new price");
                                                    product.Price = decimal.Parse(Console.ReadLine());
                                                }
                                                catch (Exception ex)
                                                {
                                                    Console.WriteLine($"Error occurred: {ex.Message}");
                                                    return;
                                                }

                                                break;
                                            }
                                        default:
                                            {
                                                goto endLoop;
                                            }
                                    }
                                }

                                endLoop:
                                if (product.Name == "exit" || product.Description == "exit")
                                {
                                    break;
                                }

                                context.Entry(product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                                break;
                            }
                        case 4:
                            {

                                Console.WriteLine("Enter name of the product which you would like to remove");

                                string productName = Console.ReadLine();
                                var product = context.Products.FirstOrDefault(p => p.Name == productName);
                                if (product is null)
                                {
                                    Console.WriteLine("Current product doesn't exist");
                                    return;
                                }

                                context.Products.Remove(product);
                                break;
                            }
                        default:
                            {
                                return;
                            }
                    }

                    context.SaveChanges();
                }
            }
        }
    }
}