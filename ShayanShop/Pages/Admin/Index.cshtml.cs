using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ShayanShop.Data;
using ShayanShop.Models;
using System.Collections.Generic;

namespace ShayanShop.Pages.Admin
{
    public class IndexModel : PageModel
    {
        private ShayanShopContext _context;

        public IndexModel(ShayanShopContext context)
        {
            _context = context;
        }
        public IEnumerable<Product> Products { get; set; }
        public void OnGet()
        {
            Products = _context.Products.Include(p => p.Item);
        }

        public void OnPost()
        {

        }
    }
}
