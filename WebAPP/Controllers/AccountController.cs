using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebAPP.Helper;
using WebAPP.Models;

namespace WebAPP.Controllers
{
    public class AccountController : Controller
    {
        const string SessionUserName = "UserName";
        const string SessionToken = "Token";
        public IActionResult Login()
        {
           // HttpContext.Session.SetString(SessionUserName, "Jarvik");
            return View();
        }

        [HttpPost]       
        public IActionResult Login(Login login)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(login);
                }
                HttpClient httpClient = UserAPI.Initail();
                var reg = JsonConvert.SerializeObject(login);
                var content = new StringContent(reg.ToString(), Encoding.UTF8, "application/json");
                HttpResponseMessage res = httpClient.PostAsync("api/Account/Login", content).Result;
                if (res.IsSuccessStatusCode)
                {
                    var result = res.Content.ReadAsStringAsync().Result;
                    var JsonResult = JsonConvert.DeserializeObject<LoginCustomjsonResult>(result);
                    if (JsonResult.Result == "Invalid login attempt")
                    {
                        ModelState.AddModelError("", "Invalid login attempt");
                        return View(login);
                    }
                    else
                    {
                        HttpContext.Session.SetString(SessionUserName, JsonResult.Email);
                        HttpContext.Session.SetString(SessionToken, JsonResult.Token);

                        return RedirectToAction("Index", "Home");
                    }

                }
                else
                {
                    ModelState.AddModelError("", "Unable to authorize to access the api");
                    return View(login);
                }
            }
            catch
            {
                ModelState.AddModelError("", "Unable to authorize to access the api");
                return View(login);
            }

         
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Registration registration)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(registration);
                }
                HttpClient httpClient = UserAPI.Initail();
                var reg = JsonConvert.SerializeObject(registration);
                var content = new StringContent(reg.ToString(), Encoding.UTF8, "application/json");
                HttpResponseMessage res = httpClient.PostAsync("api/Account/Register", content).Result;
                if (res.IsSuccessStatusCode)
                {
                    var result = res.Content.ReadAsStringAsync().Result;
                    var JsonResult = JsonConvert.DeserializeObject<JsonResultViewModel>(result);
                    if (JsonResult.result == "Error")
                    {
                        ModelState.AddModelError("", "Unable to Create User");
                        return View(registration);
                    }
                    else
                    {
                        return RedirectToAction("Login");
                    }
                    
                }
                else
                {
                    ModelState.AddModelError("", "Unable to authorize to access the api");
                    return View(registration);
                }
            }
            catch
            {
                ModelState.AddModelError("", "Unable to authorize to access the api");
                return View(registration);
            }

           
           
        }
    }
}