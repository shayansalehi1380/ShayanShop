using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShayanShop.Data;
using ShayanShop.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ShayanShop.Pages.Admin
{
    public class AddModel : PageModel
    {
        private ShayanShopContext _context;

        public AddModel(ShayanShopContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AddEditProductViewModel product { get; set; }

        [BindProperty]
        public List<int> selectedGroups { get; set; }
        public void OnGet()
        {
            //product = new AddEditProductViewModel()
            //{
            //    Categories = _context.Categories.ToList()
            //};
        }


        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();


            var item = new Item()
            {
                Price = product.Price,
                QuantityInStock = product.QuantityInStock
            };
            _context.Add(item);
            _context.SaveChanges();

            var pro = new Product()
            {
                Name = product.Name,
                Item = item,
                Description = product.Description,

            };
            _context.Add(pro);
            _context.SaveChanges();
            pro.ItemId = pro.Id;
            _context.SaveChanges();

            if (product.Picture?.Length > 0)
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "image",
                    pro.Id + Path.GetExtension(product.Picture.FileName));
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    product.Picture.CopyTo(stream);
                }
            }

            if (selectedGroups.Any() && selectedGroups.Count > 0)
            {
                foreach (int gr in selectedGroups)
                {
                    _context.CategoryToProducts.Add(new CategoryToProduct()
                    {
                        CategoryId = gr,
                        ProductId = pro.Id
                    });
                }

                _context.SaveChanges();
            }

            return RedirectToPage("Index");
        }
    }
}
