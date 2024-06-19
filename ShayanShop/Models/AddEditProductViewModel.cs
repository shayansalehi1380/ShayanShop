using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ShayanShop.Models
{
    public class AddEditProductViewModel
    {
        public int Id { get; set; }
        [Display(Name = "نام محصول")]
        public string Name { get; set; }
        [Display(Name = "توضیحات محصول")]
        public string Description { get; set; }
        [Display(Name = "قیمت محصول")]
        public decimal Price { get; set; }
        [Display(Name = "تعداد محصول")]
        public int QuantityInStock { get; set; }
        [Display(Name = "بارگزاری تصویر محصول")]
        public IFormFile Picture { get; set; }
    }
}
