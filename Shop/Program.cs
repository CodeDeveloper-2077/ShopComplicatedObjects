using Shop.Data;
using Shop.Models;

namespace Shop
{
    internal class Program
    {
        private static ShopDb context = new ShopDb();

        static void Main(string[] args)
        {
            int action = 0;
            while (true)
            {
                ShowOperation();

                action = int.Parse(Console.ReadLine());
                switch (action)
                {
                    case 1:
                        {
                            DisplayProducts();
                            break;
                        }
                    case 2:
                        {
                            AddProduct();
                            break;
                        }
                    case 3:
                        {
                            UpdateProduct();
                            break;
                        }
                    case 4:
                        {
                            RemoveProduct();
                            break;
                        }
                    default:
                        {
                            return;
                        }
                }
            }
        }

        private static void ShowOperation()
        {
            Console.WriteLine("1.See all products");
            Console.WriteLine("2.Create new product");
            Console.WriteLine("3.Update product");
            Console.WriteLine("4.Remove product");
            Console.WriteLine("5.Exit");
        }

        static void DisplayProducts()
        {
            Console.WriteLine("Id|Name|Description|Quantity|Price");
            foreach (var product in context.Products)
            {
                Console.WriteLine($"{product.Id}|{product.Name}|{product.Description}|{product.Quantity}|{product.Price}");
            }
        }

        static void AddProduct()
        {
            try
            {
                var product = new Product();
                SetProductName(product);
                SetQuantity(product);
                SetPrice(product);
                SetDescription(product);
                context.Products.Add(product);
                Console.WriteLine("Product has been created");
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
            }
        }

        static void UpdateProduct()
        {
            Console.WriteLine("Enter id of the product which you would like to update");

            int productId = int.Parse(Console.ReadLine());
            var product = context.Products.Find(productId);
            if (product is null)
            {
                Console.WriteLine("Current product doesn't exist");
                return;
            }

            ChangeProductProperty(product);

            context.Entry(product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            Console.WriteLine("Product has been updated");
        }

        static void RemoveProduct()
        {
            Console.WriteLine("Enter id of the product which you would like to remove");

            int productId = int.Parse(Console.ReadLine());
            var product = context.Products.Find(productId);
            if (product is null)
            {
                Console.WriteLine("Current product doesn't exist");
                return;
            }

            context.Products.Remove(product);
            context.SaveChanges();
            Console.WriteLine("Product has been removed");
        }

        static void ChangeProductProperty(Product product)
        {
            int property = 0;
            while (true)
            {
                ShowCommands();

                property = int.Parse(Console.ReadLine());
                try
                {
                    switch (property)
                    {
                        case 1:
                            {
                                SetProductName(product);
                                break;
                            }
                        case 2:
                            {
                                SetDescription(product);
                                break;
                            }
                        case 3:
                            {
                                SetQuantity(product);
                                break;
                            }
                        case 4:
                            {
                                SetPrice(product);
                                break;
                            }
                        default:
                            {
                                return;
                            }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error occurred: {ex.Message}");
                }
            }
        }

        private static void SetPrice(Product product)
        {
            Console.WriteLine("Enter new price");
            product.Price = decimal.Parse(Console.ReadLine());
        }

        private static void SetQuantity(Product product)
        {
            Console.WriteLine("Enter new quantity");
            product.Quantity = int.Parse(Console.ReadLine());
        }

        private static void SetDescription(Product product)
        {
            Console.WriteLine("Enter new description");
            product.Description = Console.ReadLine();
        }

        private static void SetProductName(Product product)
        {
            Console.WriteLine("Enter new name");
            product.Name = Console.ReadLine();
        }

        private static void ShowCommands()
        {
            Console.WriteLine("1.Name");
            Console.WriteLine("2.Description");
            Console.WriteLine("3.Quantity");
            Console.WriteLine("4.Price");
            Console.WriteLine("5.Exit");
        }
    }
}