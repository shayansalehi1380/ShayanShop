using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShayanShop.Models
{
    public class CategoryToProduct
    {
        [Key]
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int ProductId { get; set; }

        // Nav Prop
        public Category Category { get; set; }

        public Product Product { get; set; }
    }
}
