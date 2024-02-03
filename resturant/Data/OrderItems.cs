using System.ComponentModel.DataAnnotations;

namespace resturant.Data
{
    public class OrderItems
    {
        [Key, Required] 
        public int OrderNo { get; set; }
        [Required]
        public int StockID { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
