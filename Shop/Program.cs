using Shop.Data;
using Shop.Models;
using Shop.Services;
using System;

namespace Shop
{
    internal class Program
    {
        private static ShopDb context = new ShopDb();
        private static ProductRepository _productRepository = new ProductRepository(context);
        private static OrdersRepository _ordersRepository = new OrdersRepository(context);

        static void Main(string[] args)
        {
            try
            {
                int action = 0;
                while (true)
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
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
            }
            _productRepository.ExecuteProductOperations();
            _ordersRepository.ExecuteOrderOperations();
        }

        private static void ShowCommands()
        {
            Console.WriteLine("0.Exit");
            Console.WriteLine("1.Product Operations");
            Console.WriteLine("2.Order Operations");
        }
    }
}