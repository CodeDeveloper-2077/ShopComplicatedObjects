using System.ComponentModel.DataAnnotations;

namespace Shop.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        public DateTime OrderDate { get; set; }
    }
}
