using ChefsWonders.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChefsWonders.ViewModels
{
    public class OrderCommonViewModel
    {
        [Display(Name = "Food Category")]
        public string FoodCategoryName { get; set; }

        [Display(Name = "Food Type")]
        public string FoodTypeName { get; set; }

        [Display(Name = "Food Item")]
        public string FoodItemName { get; set; }

        public double Price { get; set; }
                
        public int Quantity { get; set; }

        [Required]
        [Display(Name = "Select Food")]
        public bool FoodSelect { get; set; }

        [Required]
        [Display(Name = "Chef")]
        public int ChefID { get; set; }

        public int OrderID { get; set; }

        [Display(Name = "Order Detail ID")]
        public int OrderDetailsID { get; set; }

        [Required]
        [Display(Name = "Status")]
        public int StatusCode { get; set; }
        public Order Order { get; set; }
        public OrderDetails OrderDetails { get; set; }
        public Chef Chef { get; set; }        
    }
}