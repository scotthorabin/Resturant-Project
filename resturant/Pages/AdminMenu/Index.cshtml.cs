using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using resturant.Data;
using resturant.Pages.Models;
using Microsoft.AspNetCore.Authorization;

namespace resturant.Pages.AdminMenu
{
    // user has to be in the admin role to access page
    [Authorize (Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly resturant.Data.resturantContext _context;

        public IndexModel(resturant.Data.resturantContext context)
        {
            _context = context;
        }

        public IList<FoodItem> FoodItem { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.FoodItems != null)
            {
                FoodItem = await _context.FoodItems.ToListAsync();
            }
        }
    }
}
