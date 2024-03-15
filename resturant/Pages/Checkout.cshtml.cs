using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using resturant.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using resturant.Pages.Models;
namespace resturant.Pages
{
    public class CheckoutModel : PageModel
    {

        private readonly resturantContext _db;
        private readonly UserManager<IdentityUser> _UserManager;
        public IList<CheckoutItem> Items { get; private set; }

        public decimal Total = 0;
        public long AmountPayable = 0;

        public OrderHistory Order = new OrderHistory();

        public CheckoutModel(resturantContext db, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _UserManager = userManager;
        }
        public async Task onGetAsync()
        {
            var user = await _UserManager.GetUserAsync(User);
            CheckoutCustomer customer = await _db.CheckoutCustomers.FindAsync(user.Email);

            Items = _db.CheckoutItems.FromSqlRaw(
                "SELECT FoodItem.ID, FoodItem.Price, FoodItem.Item_Name,  BasketItems.BasketID, BasketItems.Quantity   FROM FoodItem INNER JOIN BasketItems ON FoodItem.ID = BasketItems.StockID WHERE BasketID = {0}",
                customer.BasketID).ToList();

            Total = 0;

            foreach (var item in Items)
            {
                Total += (item.Quantity * item.Price);
            }
            AmountPayable = (long)Total;

        }
        // Process the buy click
        public async Task<IActionResult> OnPostBuyAsync()
        {
            var currentOrder = _db.OrderHistories.FromSqlRaw("SELECT * From OrderHistories")
                .OrderByDescending(b => b.OrderNo)
                .FirstOrDefault();

            if (currentOrder == null)
            {
                Order.OrderNo = 1;
            }
            else
            {
                Order.OrderNo = currentOrder.OrderNo + 1;
            }

            var user = await _UserManager.GetUserAsync(User);
            Order.Email = user.Email;
            _db.OrderHistories.Add(Order);

            CheckoutCustomer customer = await _db
                .CheckoutCustomers
                .FindAsync(user.Email);

            var basketItems =
                _db.BasketItems
                .FromSqlRaw("SELECT * From BasketItems WHERE BasketID = {0}", customer.BasketID)
                .ToList();

            foreach (var item in basketItems)
            {
                OrderItems oi = new OrderItems
                {
                    OrderNo = Order.OrderNo,
                    StockID = item.StockID,
                    Quantity = item.Quantity
                };
                _db.OrderItems.Add(oi);
                _db.BasketItems.Remove(item);
            }
            await _db.SaveChangesAsync();
            return RedirectToPage("/Index");
        }

    }


}
