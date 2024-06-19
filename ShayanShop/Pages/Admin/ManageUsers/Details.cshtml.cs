using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ShayanShop.Data;
using ShayanShop.Models;
using System.Threading.Tasks;

namespace ShayanShop.Pages.Admin.ManageUsers
{
    public class DetailsModel : PageModel
    {
        private readonly ShayanShopContext _context;

        public DetailsModel(ShayanShopContext context)
        {
            _context = context;
        }

        public Users Users { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Users = await _context.Users.FirstOrDefaultAsync(m => m.UserId == id);

            if (Users == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
