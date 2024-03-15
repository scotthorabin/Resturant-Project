using System.ComponentModel.DataAnnotations;

namespace resturant.Data
{
    public class CheckoutCustomer
    {
        [Key]
        [StringLength(50)]
        public string Email { get; set; }
        [StringLength(50)]
        public int BasketID { get; set; }
    }

    public class Basket
    {
        [Key]
        public int BasketID { get; set; }
    }

    public class BasketItem
    {
        [Required]
        public int StockID { get; set; }
        [Required]
        public int BasketID { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
