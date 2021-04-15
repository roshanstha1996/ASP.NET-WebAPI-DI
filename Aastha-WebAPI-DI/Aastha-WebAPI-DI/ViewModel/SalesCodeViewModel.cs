using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using Aastha_WebAPI_DI.Models;
using Repository;

namespace Aastha_WebAPI_DI.ViewModel
{
    public class SalesCodeViewModel
    {
        public List<ProductTable> productModels { get; set; }
        public SalesCodeTable salesCodeModel { get; set; }
    }
}