using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using resturant.Data;
using resturant.Pages.Models;

namespace resturant.Pages.AdminMenu
{
    public class CreateModel : PageModel
    {
        private readonly resturant.Data.resturantContext _context;

        public CreateModel(resturant.Data.resturantContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public FoodItem FoodItem { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

          // Created to catch the image from the admin and save it to the database
          // for loop
          foreach (var file in Request.Form.Files)
            {
                //convert file into a binary stream
                MemoryStream ms = new MemoryStream();
                file.CopyTo(ms);
                // save it into a byte array
                FoodItem.ImageData = ms.ToArray();
                // closes and disposes memory stream
                ms.Close();
                ms.Dispose();
            }

            _context.FoodItems.Add(FoodItem);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
