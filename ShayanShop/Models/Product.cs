using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShayanShop.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "نام محصول")]
        public string Name { get; set; }
        [Display(Name = "توضیحات محصول")]
        public string Description { get; set; }
        public int ItemId { get; set; }

        public List<CategoryToProduct> CategoryToProducts { get; set; }
        public Item Item { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}
