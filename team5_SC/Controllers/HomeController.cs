using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using team5_SC.Models;

namespace team5_SC.Controllers
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
            return View();
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

        

        public IActionResult Logout()
        {
            string sessionId = Request.Cookies["SessionId"];
            if (sessionId == null)
            {
                return RedirectToAction("Login");
            }

            int res = SessionData.DeleteSesion(sessionId);
            if (res != 1)
            {
                return StatusCode(500);
            }

            Response.Cookies.Delete("SessionId");

            return RedirectToAction("Login", "Home");
        }
    }
}
}
