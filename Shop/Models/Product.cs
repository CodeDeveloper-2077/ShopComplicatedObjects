using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Shop.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        public string Name { get; set; }

        public decimal PricePerOne { get; set; }

        public int Count { get; set; }

        public int OrderDetailsId { get; set; }

        [ForeignKey("OrderDetailsId")]
        public OrderDetails? OrderDetails { get; set; }
    }
}
