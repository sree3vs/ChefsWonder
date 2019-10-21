using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ChefsWonders.Models;

namespace ChefsWonders.ViewModels
{
    public class OrderCommonViewModelClass
    {        
        public int Quantity { get; set; }
        public bool FoodSelect { get; set; }
        public FoodItem FoodItem { get; set; }
    }
}