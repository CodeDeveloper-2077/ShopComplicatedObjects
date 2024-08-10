using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Shop.Models
{
    public class OrderDetails
    {
        [Key]
        public int OrderDetailsId { get; set; }

        public int OrderNumber { get; set; }

        [ForeignKey("OrderNumber")]
        public Order? Order { get; set; }

        public List<Product>? Products { get; set; }

        public OrderDetails()
        {
            Products = new List<Product>();
        }
    }
}
