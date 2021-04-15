using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Repository;

namespace Aastha_WebAPI_DI.Controllers
{

    //[BasicAuthentication]
    public class ProductController : ApiController
    {
        private IProductRepo _iProductRepo;

        public ProductController(IProductRepo product)
        {
            _iProductRepo = product;
        }


        [Route("api/loadallproduct", Name = "LoadAllProduct")]
        [HttpGet]
        public IEnumerable<ProductTable> LoadAllProduct()
        {
            return _iProductRepo.GetAllProduct();
        }

        public HttpResponseMessage Post([FromBody] ProductTable product)
        {
            try
            {
                if(_iProductRepo.SaveProductDetail(product) != null)
                {
                    var message = Request.CreateResponse(HttpStatusCode.Created, product);
                    message.Headers.Location = new Uri(Request.RequestUri + product.ProductId.ToString());
                    return message;
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Error in saving the product!!");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Route("api/deleteproduct", Name = "deleteproduct")]
        public HttpResponseMessage Delete(int id)
        {
            using (AasthaDBEntities entities = new AasthaDBEntities())
            {
                try
                {
                    var entity = entities.ProductTables.FirstOrDefault(e => e.ProductId == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Product with Id" + id.ToString() + "was not found to delete");
                    }
                    else
                    {
                        entities.ProductTables.Remove(entity);
                        entities.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                }
            }
        }

        public HttpResponseMessage Put([FromBody] ProductTable product)
        {
            using (AasthaDBEntities entities = new AasthaDBEntities())
            {
                try
                {
                    var entity = entities.ProductTables.FirstOrDefault(e => e.ProductId == product.ProductId);

                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Product with Id" + product.ProductId.ToString() + " was not found to update");
                    }
                    else
                    {
                        entity.ProductName = product.ProductName;
                        entity.CategoryId = product.CategoryId;
                        entity.Rate = product.Rate;
                        entity.QuantityInStock = product.QuantityInStock;
                        entity.ThresholdValue = product.ThresholdValue;
                        entity.MfgDate = product.MfgDate;
                        entity.ExpDate = product.ExpDate;
                        entity.Remarks = product.Remarks;

                        entities.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                }
            }
        }


        [Route("api/getproductbyid", Name = "GetProductById")]
        [HttpGet]
        public ProductTable GetProductById(int id)
        {
            using (AasthaDBEntities entities = new AasthaDBEntities())
            {
                try
                {
                    var entity = entities.ProductTables.FirstOrDefault(e => e.ProductId == id);
                    if (entity == null)
                    {
                        return new ProductTable();
                    }
                    else
                    {
                        return entity;
                    }
                }
                catch (Exception )
                {
                    return new ProductTable();
                }
            }
        }
    }
}
