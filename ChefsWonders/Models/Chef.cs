using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChefsWonders.Models
{
    [Table("Chef")]
    public class Chef
    {
        [Key]
        public int ChefID { get; set; }

        [Required]
        [Display(Name = "Chef Name")]
        public string ChefName { get; set; }

        public int statuscode { get; set; }
        public virtual StatusMaster statusMaster { get; set; }

    }
}