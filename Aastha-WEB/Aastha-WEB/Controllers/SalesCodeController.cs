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
    public class SalesCodeController : Controller
    {
        string apiURL = System.Configuration.ConfigurationManager.AppSettings["apiUrl"];

        // GET: SalesCode
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(SalesCodeModel salesCode)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                salesCode.SalesDate = DateTime.Now;
                //HTTP put
                var postTask = await client.PostAsJsonAsync(client.BaseAddress + "sales/post", salesCode);

                //dynamic result = postTask.Result;
                if (postTask.IsSuccessStatusCode)
                {
                    var resString = await postTask.Content.ReadAsStringAsync();
                    SalesCodeViewModel salesCodeVM = JsonConvert.DeserializeObject<SalesCodeViewModel>(resString);

                    TempData["item"] = salesCodeVM;

                    return RedirectToAction("ProductAdd");
                    
                }
            }
            return View(salesCode);
        }

        public ActionResult ProductAdd()
        {
            SalesCodeViewModel salesCodeVM = (SalesCodeViewModel)TempData["item"];
            ViewData["salesCode"] = salesCodeVM;
            return View(salesCodeVM);
        }

    }
}