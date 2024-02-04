using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace resturant.Pages.Models
{
    public class FoodItem
    {
        // ID will be Primary Key
        [Key]
        public int Id { get; set; }

        // Name of food item
        [StringLength(30)]
        public string Item_name { get; set; }

        // Description of item
        [StringLength(255)]
        public string Item_desc { get; set;}

        // If item is available using a bool (True or False)
        public Nullable<bool> Available { get; set; }

        // If item is vegetarian using a nullable bool (True or False)
        public bool? Vegetarian { get; set; }
        [DataType(DataType.Currency)]
        [Column(TypeName = "Money")]
        public Nullable<decimal> Price { get; set;}

        public string ImageDescription { get; set; }
        public byte[] ImageData { get; set; }  
    }
}
