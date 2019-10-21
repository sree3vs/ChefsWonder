using ChefsWonders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChefsWonders.ViewModels
{
    public class OrdersViewModel
    {
        public Order Order { get; set; }
        public List<OrderDetails> OrderDetails { get; set; }
    }
}