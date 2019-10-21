using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChefsWonders.Models
{
    [Table("FoodType")]
    public class FoodType
    {
        [Key]
        public int FoodTypeID { get; set; }

        [Required]
        [Display(Name = "Food Type")]
        public string FoodTypeName { get; set; }

        public IEnumerable<FoodItem> FoodItems { get; set; }
    }
}