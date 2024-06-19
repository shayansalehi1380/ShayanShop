using Microsoft.AspNetCore.Mvc;
using ShayanShop.Data;
using ShayanShop.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ShayanShop.Components
{
    public class ProductGroupsComponent : ViewComponent
    {
        private ShayanShopContext _context;
        public ProductGroupsComponent(ShayanShopContext context)
        {
            _context = context;            
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = _context.Categories
                .Select(c => new ShowGroupViewModel()
                {
                    GroupId = c.Id,
                    Name = c.Name,
                    ProductCount = _context.CategoryToProducts.Count(g => g.CategoryId == c.Id)
                }).ToList();
            return View("/Views/Components/ProductGroupsComponent.cshtml", categories);

        }
    }
}
