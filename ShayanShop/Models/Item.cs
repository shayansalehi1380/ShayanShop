using System.ComponentModel.DataAnnotations;

namespace ShayanShop.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int QuantityInStock { get; set; }

        public Product Product { get; set; }
    }
}
