using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChefsWonders.Models
{
    [Table("StatusMaster")]
    public class StatusMaster
    {
        [Key]
        public int statuscode { get; set; }

        [Required]
        [Display(Name = "Status")]
        public string status { get; set; }

        public IEnumerable<Chef> chefs { get; set; }

        public IEnumerable<Order> orders { get; set; }

        public IEnumerable<OrderDetails> orderDetails { get; set; }
    }
}