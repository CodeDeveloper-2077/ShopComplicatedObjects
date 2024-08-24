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
            IEnumerable<OrderDetails> orderDetails = _context.OrderDetails.Where(od => od.OrderId == order.Id);
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

                ChangeOrder(order);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
            }
        }

        private void ChangeOrder(Order order)
        {
            ShowOrder(order);

            Console.WriteLine("If you want to change the order, write '0'.\nIf you want to change position, write '1'");
            int state = int.Parse(Console.ReadLine());
            if (state == 0)
            {
                UpdateOrderProperties(order);
            }
            else
            {
                Console.WriteLine("Enter id of position in the order, you would like to change");
                int positionId = int.Parse(Console.ReadLine());

                var position = order.OrderDetails.FirstOrDefault(o => o.PositionId == positionId);
                if (position is null)
                {
                    Console.WriteLine("Current position doesn't exist");
                    return;
                }

                UpdateOrderDetailProperties(position);
            }
        }

        private void ShowOrder(Order order)
        {
            Console.WriteLine($"\nOrderDate: {order.OrderDate}\nOrderNumber: {order.OrderNumber}\n");

            IEnumerable<OrderDetails> orderDetails = _context.OrderDetails.Where(od => od.OrderId == order.Id);
            ShowOrderDetails(orderDetails);
        }

        private void ShowOrderDetails(IEnumerable<OrderDetails> orderDetails)
        {
            Console.WriteLine("PositionId|ProductName|Price|Quantity");
            foreach (var orderDetail in orderDetails)
            {
                Console.WriteLine($"{orderDetail.PositionId}|{orderDetail.ProductName}|{orderDetail.Price}|{orderDetail.Quantity}");
            }

            Console.WriteLine();
        }

        private void UpdateOrderProperties(Order order)
        {
            int property = 0;
            while (true)
            {
                ShowCommands(0);

                property = int.Parse(Console.ReadLine());
                try
                {
                    switch (property)
                    {
                        case 1:
                            {
                                SetProperty((o, f) => o.OrderDate = f(), order, () => { Console.WriteLine("Set Date in format yyyy-mm-dd"); return DateTime.Parse(Console.ReadLine()); }, "OrderDate");
                                break;
                            }
                        case 2:
                            {
                                SetProperty((o, f) => o.OrderNumber = f(), order, () => Console.ReadLine(), "OrderNumber");
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

        private void UpdateOrderDetailProperties(OrderDetails orderDetail)
        {
            int property = 0;
            while (true)
            {
                ShowCommands(1);

                property = int.Parse(Console.ReadLine());
                try
                {
                    switch (property)
                    {
                        case 3:
                            {
                                SetProperty((od, f) => od.ProductName = f(), orderDetail, () => Console.ReadLine(), "ProductName");
                                break;
                            }
                        case 4:
                            {
                                SetProperty((od, f) => od.Price = f(), orderDetail, () => decimal.Parse(Console.ReadLine()), "Price");
                                break;
                            }
                        case 5:
                            {
                                SetProperty((od, f) => od.Quantity = f(), orderDetail, () => decimal.Parse(Console.ReadLine()), "Quantity");
                                break;
                            }

                        case 6:
                            {
                                SetProperty((od, f) => od.PositionId = f(), orderDetail, () => int.Parse(Console.ReadLine()), "PositionId");
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

        private void ShowCommands(int state)
        {
            if (state == 0)
            {
                Console.WriteLine("1.OrderDate");
                Console.WriteLine("2.OrderNumber");
            }
            else
            {
                Console.WriteLine("3.ProductName");
                Console.WriteLine("4.Price");
                Console.WriteLine("5.Quantity");
                Console.WriteLine("6.PositionId");
                Console.WriteLine("7.Exit");
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
                if (order.OrderDetails.Any())
                {
                    var maxPositionId = order.OrderDetails.Max(od => od.PositionId);
                    orderDetail.PositionId = maxPositionId + 1;
                }
                else
                {
                    orderDetail.PositionId++;
                }

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
