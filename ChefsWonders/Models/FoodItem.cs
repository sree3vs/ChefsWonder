using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChefsWonders.Models
{
    [Table("FoodItems")]
    public class FoodItem
    {
        [Key]
        public int FoodItemID { get; set; }

        [Required]
        [Display(Name = "Food Name")]
        public string FoodItemName { get; set; }

        [Required]
        [Display(Name = "Price")]
        public double Price { get; set; }

        public int FoodTypeID { get; set; }
        public virtual FoodType foodType { get; set; }

        public int FoodCategoryID { get; set; }
        public virtual FoodCategory foodCategory { get; set; }
    }
}