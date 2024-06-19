using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShayanShop.Data;
using ShayanShop.Models;
using System.IO;
using System.Linq;

namespace ShayanShop.Pages.Admin
{
    public class DeleteModel : PageModel
    {
        private ShayanShopContext _context;

        public DeleteModel(ShayanShopContext context)
        {
            _context = context;
        }


        [BindProperty]
        public Product Product { get; set; }
        public void OnGet(int id)
        {
            Product = _context.Products.FirstOrDefault(p => p.Id == id);

        }

        public IActionResult OnPost()
        {
            var product = _context.Products.Find(Product.Id);
            var item = _context.Items.First(p => p.Id == product.ItemId);
            _context.Items.Remove(item);
            _context.Products.Remove(product);

            _context.SaveChanges();

            string filePath = Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot",
                "image",
                product.Id + ".jpg");
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            return RedirectToPage("Index");
        }
    }
}
