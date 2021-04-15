using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aastha_WebAPI_DI.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public string ProductName { get; set; }
        public Nullable<decimal> Rate { get; set; }
        public Nullable<int> QuantityInStock { get; set; }
        public Nullable<int> ThresholdValue { get; set; }
        public Nullable<System.DateTime> MfgDate { get; set; }
        public Nullable<System.DateTime> ExpDate { get; set; }
        public string Remarks { get; set; }
    }
}