using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Aastha_WEB.Models;

namespace Aastha_WEB.ViewModel
{
    public class SalesCodeViewModel
    {
        public List<ProductModel> productModels { get; set; }
        public SalesCodeModel salesCodeModel { get; set; }
    }
}