using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using resturant.Data;
using resturant.Pages.Models;

namespace resturant.Pages.AdminMenu
{
    public class DetailsModel : PageModel
    {
        private readonly resturant.Data.resturantContext _context;

        public DetailsModel(resturant.Data.resturantContext context)
        {
            _context = context;
        }

      public FoodItem FoodItem { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.FoodItems == null)
            {
                return NotFound();
            }

            var fooditem = await _context.FoodItems.FirstOrDefaultAsync(m => m.Id == id);
            if (fooditem == null)
            {
                return NotFound();
            }
            else 
            {
                FoodItem = fooditem;
            }
            return Page();
        }
    }
}
