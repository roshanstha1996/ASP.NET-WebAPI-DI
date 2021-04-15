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
    public class SalesDetailController : ApiController
    {
        private ISalesRepo _iSalesRepo;
        private IProductRepo _iProductRepo;

        public SalesDetailController(ISalesRepo sales, IProductRepo product)
        {
            _iSalesRepo = sales;
            _iProductRepo = product;
        }

        [HttpPost]
        public HttpResponseMessage SaveSalesDetail([FromBody] SalesTable sales)
        {
            try
            {
                if (_iSalesRepo.SaveSalesDetail(sales) != null)
                {
                    var message = Request.CreateResponse(HttpStatusCode.Created, sales);
                    message.Headers.Location = new Uri(Request.RequestUri + sales.SalesId.ToString());
                    return message;
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Error in saving the sales detail!!");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        

        [Route("api/GetSalesReportByDate", Name = "GetSalesReportByDate")]
        [HttpGet]
        public IHttpActionResult GetSalesReportByDate(DateTime? fromdate, DateTime? todate)
        {
            var result = _iSalesRepo.GetSalesReportByDate(fromdate, todate).ToList();
            //if (result.Count > 0)
            //{
            //    return Ok(result);
            //}
            //else
            //{
            //    return Content(HttpStatusCode.BadRequest, "No Sales Report Data found");
            //    //return NotFound();
            //}
            return Ok(result);
        }
    }
}
