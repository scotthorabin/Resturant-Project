using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using resturant.Data;
using resturant.Pages.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
namespace resturant.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        [BindProperty]
        public string Search {  get; set; }

        private readonly resturantContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly resturantContext _context;

        public IndexModel(resturantContext  context, ILogger<IndexModel> logger, resturantContext db, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _logger = logger;
            _db = db;
            _userManager = userManager;
        }

        public IList<FoodItem> FoodItem { get; set; } 

    
        public void OnGet()
        {
            FoodItem = _context.FoodItems.FromSqlRaw("Select * FROM FoodItem").ToList();
        }

        public IActionResult OnPostSearch()
        {
            FoodItem = _context.FoodItems
                .FromSqlRaw("SELECT * FROM FoodItem WHERE Item_name LIKE '" + Search + "%'").ToList();
            return Page();
        }

        public async Task<IActionResult> OnPostBuyAsync(int itemID)
        {
            var user = await _userManager.GetUserAsync(User);
            CheckoutCustomer customer = await _db
                .CheckoutCustomers
                .FindAsync(user.Email);

            var item = _db.BasketItems
                .FromSqlRaw("SELECT * FROM BasketItems WHERE StockID = {0}" +
                " AND BasketID = {1}", itemID, customer.BasketID)
                .ToList()
                .FirstOrDefault();

            if (item == null)
            {
                BasketItem newItem = new BasketItem
                {
                    BasketID = customer.BasketID,
                    StockID = itemID,
                    Quantity = 1
                };
                _db.BasketItems.Add(newItem);
                await _db.SaveChangesAsync();
            } else
            {
                item.Quantity = item.Quantity + 1;
                _db.Attach(item).State = EntityState.Modified;
                try
                {
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException e)
                {
                    throw new Exception($"Basket not found!", e);
                }
            }
            return RedirectToPage();
            
        }
    }
}