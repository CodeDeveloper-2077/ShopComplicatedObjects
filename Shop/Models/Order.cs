using System.ComponentModel.DataAnnotations;

namespace Shop.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        public string OrderNumber { get; set; }

        public virtual IList<OrderDetails> OrderDetails { get; set; }

        public Order()
        {
            OrderDetails = new List<OrderDetails>();
        }
    }
}
