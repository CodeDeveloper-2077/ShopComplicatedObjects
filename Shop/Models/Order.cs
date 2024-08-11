using System.ComponentModel.DataAnnotations;

namespace Shop.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        public int OrderNumber { get; set; }
    }
}
