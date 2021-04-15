using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Aastha_WEB.Models;
using Aastha_WEB.ViewModel;
using Newtonsoft.Json;

namespace Aastha_WEB.Controllers
{
    public class SalesController : Controller
    {
        string apiURL = System.Configuration.ConfigurationManager.AppSettings["apiUrl"];

        // GET: Sales
        public ActionResult Index(int salescodeid, int productid)
        {
            SalesModel sales = new SalesModel
            {
                ProductId = productid,
                SalesCodeId = salescodeid,
                SalesQuantity = 1
            };
            return View(sales);
        }

        [HttpPost]
        public async Task<ActionResult> Index(SalesModel sales)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //sales. = 1;
                //HTTP put
                var postTask = await client.PostAsJsonAsync(client.BaseAddress + "salesdetail/savesalesdetail", sales);

                //dynamic result = postTask.Result;
                if (postTask.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(sales);
        }
    }
}