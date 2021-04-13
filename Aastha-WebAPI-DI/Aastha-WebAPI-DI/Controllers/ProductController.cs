using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Repository;

namespace Aastha_WebAPI_DI.Controllers
{
    public class ProductController : ApiController
    {
        private IProductRepo _iProductRepo;

        public ProductController(IProductRepo product)
        {
            _iProductRepo = product;
        }

        [HttpGet]
        public IEnumerable<ProductTable> LoadAllProduct()
        {
            using (AasthaDBEntities entities = new AasthaDBEntities())
            {
                return entities.ProductTables.ToList();
            }
        }

        public HttpResponseMessage Post([FromBody] ProductTable product)
        {
            try
            {
                using (AasthaDBEntities entities = new AasthaDBEntities())
                {
                    entities.ProductTables.Add(product);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, product);
                    message.Headers.Location = new Uri(Request.RequestUri + product.ProductId.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public IHttpActionResult GetSalesReportByDate(DateTime fromdate, DateTime todate)
        {
            using(AasthaDBEntities entities = new AasthaDBEntities())
            {
                var result = entities.GetSalesReportByDate(fromdate, todate).ToList();

                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }
            }
        }
    }
}
