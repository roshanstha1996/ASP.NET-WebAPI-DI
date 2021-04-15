using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aastha_WEB.Models
{
    public class GetSalesReportModel
    {
        public string CustomerName { get; set; }
        public Nullable<System.DateTime> SalesDate { get; set; }
        public string ProductName { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<double> Quantity { get; set; }
    }
}