using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WebAPP.Helper;
using WebAPP.Models;

namespace WebAPP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var token = HttpContext.Session.GetString("Token");
            if (token == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else { return View(); }

            //  return View();

        }

        [HttpPost]
        public async Task<IActionResult> Reservation(Reservation reservation)
        {
            var manu = Request.Form["manu"];
            reservation.ManuType = manu;
            try
            {
                if (!ModelState.IsValid)
                {
                    return RedirectToAction("Index", "Home");
                }
                HttpClient httpClient = UserAPI.Initail();
                var token = HttpContext.Session.GetString("Token");
                if (token == null)
                {
                    return RedirectToAction("Login", "Account");
                }
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var reg = JsonConvert.SerializeObject(reservation);
                var content = new StringContent(reg.ToString(), Encoding.UTF8, "application/json");
                HttpResponseMessage res = httpClient.PostAsync("api/Reservation", content).Result;
                if (res.IsSuccessStatusCode)
                {
                    var result = res.Content.ReadAsStringAsync().Result;
                    var JsonResult = JsonConvert.DeserializeObject<JsonResultViewModel>(result);
                    if (JsonResult.result == "Sucessfully Register")
                    {
                        ViewBag.Reservation = "Sucessfully Reservation";
                        return RedirectToAction("Index", "Home");
                       
                    }
                    else
                    {


                        ModelState.AddModelError("", "Unable to authorize to access the api");
                        return RedirectToAction("Index", "Home");
                    }

                }
                else
                {
                    ModelState.AddModelError("", "Unable to authorize to access the api");
                    return RedirectToAction("Index", "Home");
                }
            }
            catch
            {
                ModelState.AddModelError("", "Unable to authorize to access the api");
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
