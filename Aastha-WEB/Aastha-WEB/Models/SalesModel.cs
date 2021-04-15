using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aastha_WEB.Models
{
    public class SalesModel
    {
        public int SalesId { get; set; }
        public Nullable<int> SalesCodeId { get; set; }
        public Nullable<int> ProductId { get; set; }
        public Nullable<double> SalesQuantity { get; set; }
    }
}