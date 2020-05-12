using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SellerUI.Models;

namespace SellerUI.Controllers
{
    public class AccountController : Controller
    {
        public static string url = "https://localhost:62518/api/v1/";
        public IActionResult Index()
        {
            return View();
        }
        // GET: User
        // GET: User/Create
        public async Task<IActionResult> RegisterSeller()
        {
            return View();
        }
        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterSeller(SellerRegister sellerRegister)
        {
            SellerRegister seller = new SellerRegister();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(sellerRegister), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("http://localhost:62518/api/v1/RegisterUser/", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    //userDetails1 = JsonConvert.DeserializeObject<UserDetails>(apiResponse);
                }
            }
            return RedirectToAction("SellerLogin");
        }
        public async Task<IActionResult> SellerLogin()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SellerLogin(SellerLogin sellerLogin)
        {
            SellerLogin sellerLogin1= new SellerLogin();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(sellerLogin), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("http://localhost:62518/api/v1/UserLogin/", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (apiResponse != null)
                    {
                        return RedirectToAction("SellerIndex");
                    }
                    else
                    {
                        Console.WriteLine("Invalid Credentials");
                        return RedirectToAction("SellerLogin");
                    }
                    //sellerLogin1 = JsonConvert.DeserializeObject<SellerLogin>(apiResponse);
                }

            }
        }
        
    }
}