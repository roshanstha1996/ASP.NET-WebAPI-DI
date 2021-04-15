using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using Aastha_WEB.Models;

namespace Aastha_WEB.Controllers
{
    public class ReportSearchController : Controller
    {
        string apiURL = System.Configuration.ConfigurationManager.AppSettings["apiUrl"];

        // GET: ReportSearch
        public ActionResult Index(DateTime? fromDate, DateTime? toDate)
        {
            IEnumerable<GetSalesReportModel> sales = null;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);

                //sales. = 1;
                //HTTP put
                var postTask = client.GetAsync(client.BaseAddress + "GetSalesReportByDate?fromDate="+ fromDate.ToString() + "&toDate="+ toDate.ToString());
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var resString = result.Content.ReadAsAsync<IList<GetSalesReportModel>>();
                    resString.Wait();

                    sales = resString.Result;
                }
            }
            return View(sales);
        }
    }
}