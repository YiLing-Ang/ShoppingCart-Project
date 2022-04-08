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

        public IActionResult Login(string username, string password)
        {
            string sessionId = Request.Cookies["SessionId"];
            if (sessionId != null)
            {              
                Session session = SessionData.GetSessionById(sessionId);
                if (session != null)
                {                   
                    return RedirectToAction("Track");
                }
            }

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {               
                return View();
            }

            User user = UserData.GetUserByUsername(username);
            if (user == null)
            {               
                return View();
            }

            if (user.Password != password)
            {
                return View();
            }

            sessionId = Guid.NewGuid().ToString();
            int res = SessionData.CreateSession(new Session
            {
                Id = sessionId,
                UserId = user.Id
            });

            if (res != 1)
            {
                return StatusCode(500);
            }

            Response.Cookies.Append("SessionId", sessionId);

            return RedirectToAction("Track");
        }

        public IActionResult Track(string clickedBtn)
        {
            Click click;

            string sessionId = Request.Cookies["SessionId"];
            if (sessionId == null)
            {
                return RedirectToAction("Login");
            }

            Session session = SessionData.GetSessionById(sessionId);
            if (session == null)
            {
                return RedirectToAction("Login");
            }
        
            click = ClickData.GetClickByUserId(session.UserId);

            if (click == null)
            {
                ClickData.CreateClick(new Click
                {
                    UserId = session.UserId,
                    ClickButtons = clickedBtn
                });
            }
            else
            {
                ClickData.UpdateClick(new Click
                {
                    UserId = session.UserId,
                    ClickButtons = click.ClickButtons + " " + clickedBtn
                });
            }
            User user = UserData.GetUserById(session.UserId);
            if (user != null)
                ViewData["username"] = user.Username;

            click = ClickData.GetClickByUserId(session.UserId);
            if (click != null)
                ViewData["clickedButtons"] = click.ClickButtons;

            return View();
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
