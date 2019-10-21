using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChefsWonders.Models
{
    [Table("FoodCategory")]
    public class FoodCategory
    {
        [Key]
        public int FoodCategoryID { get; set; }

        [Required]
        [Display(Name = "Food Category")]
        public string FoodCategoryName { get; set; }

        public IEnumerable<FoodItem> FoodItems { get; set; }
    }
}