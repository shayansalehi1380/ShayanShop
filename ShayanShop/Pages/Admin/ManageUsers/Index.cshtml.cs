using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ShayanShop.Data;
using ShayanShop.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShayanShop.Pages.Admin.ManageUsers
{
    public class IndexModel : PageModel
    {
        private readonly ShayanShopContext _context;

        public IndexModel(ShayanShopContext context)
        {
            _context = context;
        }

        public IList<Users> Users { get; set; }

        public async Task OnGetAsync()
        {
            Users = await _context.Users.ToListAsync();
        }
    }
}
