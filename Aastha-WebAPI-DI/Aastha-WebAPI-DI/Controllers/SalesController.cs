using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Repository;
using Aastha_WebAPI_DI.Models;
using Aastha_WebAPI_DI.ViewModel;

namespace Aastha_WebAPI_DI.Controllers
{
    public class SalesController : ApiController
    {
        private ISalesRepo _iSalesRepo;
        private IProductRepo _iProductRepo;

        public SalesController(ISalesRepo sales, IProductRepo product)
        {
            //object parameters will be injected into this controller and we can use below object for calling the method
            //_iproductRepo = productRepo;
            _iSalesRepo = sales;
            _iProductRepo = product;
        }

        public SalesCodeViewModel Post([FromBody] SalesCodeTable salesCode)
        {
            try
            {
                SalesCodeViewModel salesCodeVM = new SalesCodeViewModel();
                var saveSalesCode = _iSalesRepo.SaveSalesCodeDetail(salesCode);
                if (saveSalesCode != null)
                {
                    var salesModel = saveSalesCode;
                    List<ProductTable> productList = _iProductRepo.GetAllProduct().ToList();
                    salesCodeVM.salesCodeModel = salesModel;
                    salesCodeVM.productModels = productList;

                    var message = Request.CreateResponse(HttpStatusCode.Created, salesCode);
                    message.Headers.Location = new Uri(Request.RequestUri + salesCode.SalesCodeId.ToString());

                    return salesCodeVM;
                }
                else
                {
                    return new SalesCodeViewModel();
                }
            }
            catch (Exception)
            {
                return new SalesCodeViewModel();
            }
        }

    }
}
