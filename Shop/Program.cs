using LoggerService;
using Shop.Data;
using Shop.Services;

namespace Shop
{
    internal class Program
    {
        private static ShopDb _context = new ShopDb();
        private static ILoggerManager _logger = new LoggerManager();
        private static ProductRepository _productRepository = new ProductRepository(_context, _logger);
        private static OrdersRepository _ordersRepository = new OrdersRepository(_context, _logger, _productRepository);

        static void Main(string[] args)
        {
            int action = 0;
            while (true)
            {
                try
                {
                    ShowCommands();
                    action = int.Parse(Console.ReadLine());
                    switch (action)
                    {
                        case 1:
                            {
                                _productRepository.ExecuteProductOperations();
                                break;
                            }
                        case 2:
                            {
                                _ordersRepository.ExecuteOrderOperations();
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
                    Console.WriteLine($"Error occurred! See inner log");
                    _logger.LogError($"Exception: {ex.Message}");
                }
            }
        }

        private static void ShowCommands()
        {
            Console.WriteLine("0.Exit");
            Console.WriteLine("1.Product Operations");
            Console.WriteLine("2.Order Operations");
        }
    }
}