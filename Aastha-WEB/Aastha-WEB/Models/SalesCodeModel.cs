using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Aastha_WEB.Models
{
    public class SalesCodeModel
    {
        public int SalesCodeId { get; set; }

        [Required(ErrorMessage = "Required")]
        public string CustomerName { get; set; }
        public Nullable<System.DateTime> SalesDate { get; set; }

        [Required(ErrorMessage = "Required")]
        public string CustomerEmail { get; set; }

        [Required(ErrorMessage = "Required")]
        public string ContactNumber { get; set; }

        [Required(ErrorMessage = "Required")]
        public string Address { get; set; }
    }
}