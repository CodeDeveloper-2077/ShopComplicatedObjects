using Shop.Data;
using Shop.Models;

namespace Shop.Services
{
    public class OrdersRepository
    {
        private readonly ShopDb _context;

        public OrdersRepository(ShopDb context)
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
                            DisplayOrders();
                            break;
                        }
                    case 2:
                        {
                            DisplayOrderDetails();
                            break;
                        }
                    case 3:
                        {
                            CreateOrder();
                            break;
                        }
                    case 4:
                        {
                            UpdateOrder();
                            break;
                        }
                    case 5:
                        {
                            CancelOrder();
                            break;
                        }
                    case 6:
                        {
                            Console.Clear();
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
            Console.WriteLine("1.See all orders");
            Console.WriteLine("2.See order details");
            Console.WriteLine("3.Create new order");
            Console.WriteLine("4.Update order");
            Console.WriteLine("5.Cancel order");
            Console.WriteLine("6.Clear screen");
            Console.WriteLine("7.Exit");
        }

        private void DisplayOrders()
        {
            Console.WriteLine("Id|OrderDate|OrderNumber");
            foreach (var order in _context.Orders)
            {
                Console.WriteLine($"{order.Id}|{order.OrderDate}|{order.OrderNumber}");
            }
        }

        private void DisplayOrderDetails()
        {
            Console.WriteLine("Enter OrderNumber");
            string orderNumber = Console.ReadLine();

            var order = _context.Orders.FirstOrDefault(o => o.OrderNumber == orderNumber);
            if (order is null)
            {
                Console.WriteLine("Current order doesn't exist");
                return;
            }

            Console.WriteLine("Id|ProductName|Price|Quantity|PositionId|OrderNumber");
            IEnumerable<OrderDetails> orderDetails = order.OrderDetails;
            foreach (var orderDetail in orderDetails)
            {
                Console.WriteLine($"{orderDetail.Id}|{orderDetail.ProductName}|{orderDetail.Price}|{orderDetail.Quantity}|{orderDetail.PositionId}|{order.OrderNumber}");
            }
        }

        private void CreateOrder()
        {
            try
            {
                var order = new Order();
                SetProperty((o, func) => o.OrderDate = func(), order, () => { Console.WriteLine("Set Date in format yyyy-mm-dd"); return DateTime.Parse(Console.ReadLine()); }, "OrderDate");
                SetProperty((o, func) => o.OrderNumber = func(), order, () => Console.ReadLine(), "OrderNumber");

                int action = 1;
                while (true)
                {
                    if (action == 0)
                    {
                        break;
                    }

                    AddPosition(order);
                    Console.WriteLine("0.Exit");
                    Console.WriteLine("1.Add New Line");
                    action = int.Parse(Console.ReadLine());
                }

                _context.Orders.Add(order);
                _context.SaveChanges();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Exception occurred: {ex.Message}");
            }
        }

        private void UpdateOrder()
        {
            try
            {
                Console.WriteLine("Enter number of order which you would like to update");
                string orderNumber = Console.ReadLine();

                var order = _context.Orders.FirstOrDefault(o => o.OrderNumber == orderNumber);
                if (order is null)
                {
                    Console.WriteLine("Current order doesn't exist");
                    return;
                }

                //End it later
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
            }
        }

        private void CancelOrder()
        {
            Console.WriteLine("Enter number of the order you would like to remove");
            string orderNumber = Console.ReadLine();

            var order = _context.Orders.FirstOrDefault(o => o.OrderNumber == orderNumber);
            if (order is null)
            {
                Console.WriteLine("Current order doesn't exist");
                return;
            }

            _context.Orders.Remove(order);
            _context.SaveChanges();
        }

        private void AddPosition(Order order)
        {
            try
            {
                var orderDetail = new OrderDetails();
                SetProperty((od, func) => od.ProductName = func(), orderDetail, () => Console.ReadLine(), "ProductName");
                SetProperty((od, func) => od.Price = func(), orderDetail, () => decimal.Parse(Console.ReadLine()), "Price");
                SetProperty((od, func) => od.Quantity = func(), orderDetail, () => decimal.Parse(Console.ReadLine()), "Quantity");
                orderDetail.PositionId++;

                order.OrderDetails.Add(orderDetail);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
            }
        }

        private void SetProperty<E, T>(Action<E, Func<T>> action, E od, Func<T> func, string fieldName)
        {
            Console.WriteLine($"Enter {fieldName}");
            action(od, func);
        }
    }
}
