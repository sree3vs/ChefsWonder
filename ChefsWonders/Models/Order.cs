using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChefsWonders.Models
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        [Display(Name = "Order Number")]
        public int OrderID { get; set; }

        [Display(Name = "Ordered Time")]
        public DateTime OrderDate { get; set; }

        [Display(Name = "Order Status")]
        public int statuscode { get; set; }
        public virtual StatusMaster statusMaster { get; set; }

        public List<OrderDetails> OrderDetails { get; set; }

    }

    public class OrderDetails
    {
        [Display(Name = "Order Detail Number")]
        public int OrderDetailsID { get; set; }

        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [Display(Name = "Order Number")]
        public int OrderID { get; set; }
        public virtual Order Order { get; set; }

        [Display(Name = "Food Item")]
        public int FoodItemID { get; set; }
        public virtual FoodItem FoodItems { get; set; }

        [Display(Name = "Chef")]
        public int ChefID { get; set; }

        [Display(Name = "Status")]
        public int statuscode { get; set; }
        
    }
}