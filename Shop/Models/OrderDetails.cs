using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Shop.Models
{
    public class OrderDetails
    {
        [Key]
        public int Id { get; set; }

        public int OrderId { get; set; }

        [ForeignKey("OrderId")]
        public Order? Order { get; set; }

        public virtual IList<Product> Products { get; set; }

        public decimal Price { get; set; }

        public OrderDetails()
        {
            Products = new List<Product>();
        }
    }
}
