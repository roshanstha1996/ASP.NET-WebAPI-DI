using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Aastha_WEB.Models;
using Newtonsoft.Json;

namespace Aastha_WEB.Controllers
{
    public class ProductController : Controller
    {

        string apiURL = System.Configuration.ConfigurationManager.AppSettings["apiUrl"];
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> GetAllProduct()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Clear();

                List<ProductModel> product = null;
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource  using HttpClient  
                var responseTask = await client.GetAsync("LoadAllProduct");
                //responseTask.Wait();

                //var result = responseTask.Result;
                if (responseTask.IsSuccessStatusCode)
                {
                    var resString = await responseTask.Content.ReadAsStringAsync();
                    product = JsonConvert.DeserializeObject<List<ProductModel>>(resString);

                    if (product.Count > 0 && product != null)
                    {
                        return View(product);
                    }
                    else
                    {
                        return Json(new { status = "error", message = "error in load product. Please Try again!!!" });
                    }
                }
                else //web api sent error response 
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }

        public ActionResult CreateNewProduct()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateNewProduct(ProductModel product)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                product.CategoryId = 1;
                //HTTP put
                var postTask = await client.PostAsJsonAsync(client.BaseAddress + "product/post", product);

                //dynamic result = postTask.Result;
                if (postTask.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetAllProduct");
                }
            }
            return View(product);
        }

        public ActionResult Edit(int id)
        {
            ProductModel product = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Clear();

                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource  using HttpClient  
                var responseTask = client.GetAsync("getproductbyid?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var resString = result.Content.ReadAsStringAsync().Result;
                    product = JsonConvert.DeserializeObject<ProductModel>(resString);

                    if (product != null)
                    {
                        return View(product);
                    }
                    else
                    {
                        return Json(new { status = "error", message = "unable to edit product details with id=" + id.ToString() +". Please Try again!!!" });
                    }
                }
                else //web api sent error response 
                {
                    return Json(new { status = "error", message = "unable to edit product details with id=" + id.ToString() + ". Please Try again!!!" });
                }
            }
        }

        [HttpPost]
        public async Task<ActionResult> Edit(ProductModel product)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Clear();

                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource  using HttpClient  
                var responseTask = await client.PutAsJsonAsync<ProductModel>(client.BaseAddress + "product/put", product);

                if (responseTask.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetAllProduct");
                }
                else
                    return View(product);
            }
        }

        public async Task<ActionResult> Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Clear();

                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource  using HttpClient  
                var responseTask = await client.DeleteAsync("deleteproduct?id=" + id.ToString());
                //responseTask.Wait();

                if (responseTask.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetAllProduct");
                }
                else //web api sent error response 
                {
                    return Json(new { status = "error", message = "unable to delete product details. Please Try again!!!" });
                }
            }
        }
    }
}