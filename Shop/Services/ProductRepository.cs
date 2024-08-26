using Shop.Data;
using Shop.Models;

namespace Shop.Services
{
    public class ProductRepository
    {
        private readonly ShopDb _context;

        public ProductRepository(ShopDb context)
        {
            _context = context;
        }

        public void Main()
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

        private void ShowOperation()
        {
            Console.WriteLine("1.See all products");
            Console.WriteLine("2.Create new product");
            Console.WriteLine("3.Update product");
            Console.WriteLine("4.Remove product");
            Console.WriteLine("5.Exit");
        }

        private void DisplayProducts()
        {
            Console.WriteLine("Id|Name|Description");
            foreach (var product in _context.Products)
            {
                Console.WriteLine($"{product.Id}|{product.Name}|{product.Description}");
            }
        }

        private void AddProduct()
        {
            try
            {
                var product = new Product();
                SetProductName(product);
                SetDescription(product);
                _context.Products.Add(product);
                Console.WriteLine("Product has been created");
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
            }
        }

        private void UpdateProduct()
        {
            Console.WriteLine("Enter id of the product which you would like to update");

            int productId = int.Parse(Console.ReadLine());
            var product = _context.Products.Find(productId);
            if (product is null)
            {
                Console.WriteLine("Current product doesn't exist");
                return;
            }

            ChangeProductProperty(product);

            _context.SaveChanges();
            Console.WriteLine("Product has been updated");
        }

        private void RemoveProduct()
        {
            Console.WriteLine("Enter id of the product which you would like to remove");

            int productId = int.Parse(Console.ReadLine());
            var product = _context.Products.Find(productId);
            if (product is null)
            {
                Console.WriteLine("Current product doesn't exist");
                return;
            }

            _context.Products.Remove(product);
            _context.SaveChanges();
            Console.WriteLine("Product has been removed");
        }

        private void SetProperty<T>(Action<Product, Func<T>> action, Product pr, Func<T> func, string fieldName)
        {
            Console.WriteLine($"Enter new {fieldName}");
            action(pr, func);
        }

        private void ChangeProductProperty(Product product)
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
                                //SetProductName(product);
                                SetProperty((p, f) => p.Name = f(), product, () => Console.ReadLine(), "Name");
                                break;
                            }
                        case 2:
                            {
                                //SetDescription(product);
                                SetProperty((p, f) => p.Description = f(), product, () => Console.ReadLine(), "Description");
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

        private void SetDescription(Product product)
        {
            Console.WriteLine("Enter new description");
            product.Description = Console.ReadLine();
        }

        private void SetProductName(Product product)
        {
            Console.WriteLine("Enter new name");
            product.Name = Console.ReadLine();
        }

        private void ShowCommands()
        {
            Console.WriteLine("1.Name");
            Console.WriteLine("2.Description");
            Console.WriteLine("3.Exit");
        }
    }
}
