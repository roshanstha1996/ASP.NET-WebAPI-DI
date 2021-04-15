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
    public class UserController : Controller
    {
        string apiURL = System.Configuration.ConfigurationManager.AppSettings["apiUrl"];


        // GET: User
        public ActionResult Login()
        {
            UserModel user = new UserModel
            {
                UserName = "",
                Password = "",
                IsActive = false,
                Role = "",
                CreatedDate = DateTime.Today,
                CreatedBy = "",
                LoginDate = DateTime.Now
            };
            return View(user);
        }

        [HttpPost]
        public ActionResult Login(UserModel user)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //client.

                //model.SecondPartiesIdentitiesList = teststring;
                ////Serialize and transfer the model
                //string modelSerialize = JsonConvert.SerializeObject(model);

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                var responseTask = client.GetAsync("Login" + "?username=" + user.UserName + "&password=" + user.Password);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync().Result;

                    if (readTask == "true")
                    {
                        return RedirectToAction("Index", "Home", new { area = "" });
                    }
                    else
                    {
                        return RedirectToAction("Login");
                    }
                }
                else //web api sent error response 
                {
                    return RedirectToAction("Login");
                }
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetAllUsers()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Clear();

                List<UserModel> user = null;
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //model.SecondPartiesIdentitiesList = teststring;
                ////Serialize and transfer the model
                //string modelSerialize = JsonConvert.SerializeObject(model);

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                var responseTask = await client.GetAsync("LoadAllUsers");
                //responseTask.Wait();
                //var result = responseTask.Result;
                if (responseTask.IsSuccessStatusCode)
                {
                    var resString = await responseTask.Content.ReadAsStringAsync();
                    user = JsonConvert.DeserializeObject<List<UserModel>>(resString);
                    //dynamic list= JsonConvert.DeserializeObject<IEnumerable<UserModel>();

                    if (user.Count > 0 && user != null)
                    {
                        return View(user);
                    }
                    else
                    {
                        return RedirectToAction("Login");
                    }
                }

                else //web api sent error response 
                {
                    return RedirectToAction("Login");
                }
            }
        }
        public ActionResult CreateNewUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateNewUser(UserModel user)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //HTTP POST
                var postTask = await client.PostAsJsonAsync(client.BaseAddress + "user/post", user);

                //dynamic result = postTask.Result;
                if (postTask.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetAllUsers");
                }
            }
            return View(user);
        }

        
    }
}