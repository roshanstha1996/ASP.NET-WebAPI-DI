//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccessLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class BillTable
    {
        public int BillId { get; set; }
        public Nullable<int> SalesCodeId { get; set; }
        public Nullable<decimal> Vat { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public Nullable<decimal> NetTotal { get; set; }
    
        public virtual SalesCodeTable SalesCodeTable { get; set; }
    }
}