using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace resturant.Pages.Models
{
    public class FoodItem
    {
        [Key]
        public int Id { get; set; }
        [StringLength(30)]
        public string Item_name { get; set; }
        [StringLength(255)]
        public string Item_desc { get; set;}

        public Nullable<bool> Available { get; set; }
        public bool? Vegetarian { get; set; }
        [DataType(DataType.Currency)]
        [Column(TypeName = "Money")]
        public Nullable<decimal> Price { get; set;}

        public string ImageDescription { get; set; }
        public byte[] ImageData { get; set; }  
    }
}
