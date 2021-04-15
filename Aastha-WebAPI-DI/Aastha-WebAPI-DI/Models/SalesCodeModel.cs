using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aastha_WebAPI_DI.Models
{
    public class SalesCodeModel
    {
        public int SalesCodeId { get; set; }
        public string CustomerName { get; set; }
        public Nullable<System.DateTime> SalesDate { get; set; }
        public string CustomerEmail { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
    }
}