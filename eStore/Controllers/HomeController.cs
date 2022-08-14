using BusinessObject;
using DataAccess.Repository;
using eStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace eStore.Controllers
{
    public class HomeController : Controller
    {
        IMemberRepository memberRepository = new MemberRepository();
        Member currUser = null;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            int? MemberID = HttpContext.Session.GetInt32("MemberID");
            if (MemberID == null)
            {
                return View();
            }
            else if (MemberID == 0)
            {
                return RedirectToAction(nameof(Index), "Admin");
            }
            else
            {
                return RedirectToAction(nameof(Index), "Member");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CheckLogin(string email, string password)
        {
            try
            {
                bool checkAdminResult = checkAdmin(email, password);
                if (checkAdminResult)
                {
                    HttpContext.Session.SetInt32("MemberID", 0);
                    return RedirectToAction(nameof(Index), "Admin");
                }
                else
                {
                    currUser = memberRepository.Login(email, password);
                    if (currUser != null)
                    {
                        HttpContext.Session.SetInt32("MemberID", currUser.MemberId);
                        return RedirectToAction(nameof(Index), "Member");
                    }
                    else
                    {
                        ViewBag.Message = "Wrong email or password!";
                        ViewBag.Email = email;
                        ViewBag.Password = password;
                        return View(nameof(Index));
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(nameof(Error));
            }
        }

        private bool checkAdmin(string email, string password)
        {
            IConfiguration config = new ConfigurationBuilder()
                                        .SetBasePath(Directory.GetCurrentDirectory())
                                        .AddJsonFile("appsettings.json", true, true)
                                        .Build();
            if (email.Equals(config["DefaultEmail:Email"]) &&
                password.Equals(config["DefaultEmail:Password"]))
            {
                return true;
            }
            return false;
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
