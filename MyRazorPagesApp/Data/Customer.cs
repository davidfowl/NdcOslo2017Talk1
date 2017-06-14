using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyRazorPagesApp.Data
{
    public class Customer
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("First name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Last name")]
        public string LastName { get; set; }
    }
}
