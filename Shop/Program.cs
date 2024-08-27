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
            _productRepository.ExecuteProductOperations();
            _ordersRepository.ExecuteOrderOperations();
        }
    }
}