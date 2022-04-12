//using System.Runtime.InteropServices;

//// In SDK-style projects such as this one, several assembly attributes that were historically
//// defined in this file are now automatically added during build and populated with
//// values defined in project properties. For details of which attributes are included
//// and how to customise this process see: https://aka.ms/assembly-info-properties


//// Setting ComVisible to false makes the types in this assembly not visible to COM
//// components.  If you need to access a type in this assembly from COM, set the ComVisible
//// attribute to true on that type.

//[assembly: ComVisible(false)]

//// The following GUID is for the ID of the typelib if this project is exposed to COM.

//[assembly: Guid("cb15b9c1-31f5-4989-9045-beeaa16409ba")]
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using team5_SC.Models;
using System.Security.Cryptography;
using System.Text;

namespace team5_SC.Controllers
{
    public class LoginController : Controller
    {
        private readonly DBContext dbContext;

        public LoginController(DBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpPost]
        public IActionResult Login(IFormCollection form)
        {
            string username = form["username"];
            string password = form["password"];

            HashAlgorithm sha = SHA256.Create();
            byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(username + password));

            // authenticate  user and password
            User user = dbContext.Users.FirstOrDefault(x =>
                x.Username == username &&
                x.Password == hash
            );

            if (user == null)
            {
                ViewBag.Message = "Login failed. Invalid credentials entered.";

                return View("Index");

                //return RedirectToAction("Index", "Login");
            }

            if (Request.Cookies["SessionId"] != null)
            {
                Guid sessionid = Guid.Parse(Request.Cookies["SessionId"]);
                Session session = dbContext.Sessions.FirstOrDefault(x =>
                                    x.Id == sessionid);
                session.User = user;
                dbContext.SaveChanges();

                List<Cart> products = dbContext.Carts.Where(x =>
                    x.User.Id == user.Id
                ).ToList();

                List<Cart> carts = dbContext.Carts.Where(x =>
                    x.SessionId == sessionid
                ).ToList();

                foreach(Cart cart in carts)
                {
                    //cart.User = user;
                    foreach(Cart product in products)
                    {
                        if (cart.Product.Id == product.Product.Id)
                        {
                            product.Quantity += cart.Quantity;
                            dbContext.SaveChanges();
                            dbContext.Remove(cart);
                            dbContext.SaveChanges();
                        }                        
                        else
                        {
                            cart.User = user;
                            dbContext.SaveChanges();
                        }
                    }
                }
                

                Response.Cookies.Append("Username", user.Username);
            }
            else
            {
                // create a new session and tag to user
                Session newsession = new Session()
                {
                    User = user
                };
                dbContext.Sessions.Add(newsession);
                dbContext.SaveChanges();

                // ask browser to save and send back these cookies next time
                Response.Cookies.Append("SessionId", newsession.Id.ToString());
                Response.Cookies.Append("Username", user.Username);
            }            

            return RedirectToAction("Index","Home");
        }

        public IActionResult Index()
        {
            if (Request.Cookies["SessionId"] != null)
            {
                Guid sessionId = Guid.Parse(Request.Cookies["sessionId"]);
                Session session = dbContext.Sessions.FirstOrDefault(x =>
                    x.Id == sessionId
                );

                //User user = dbContext.Users.FirstOrDefault(x =>
                //    x.Id == session.User.Id
                //);

                if (session.User == null)
                {
                    // someone has used an invalid Session ID (to fool us?); 
                    // route to Logout controller
                    return View();
                }

                // valid Session ID; route to Home page
                return RedirectToAction("Index", "Home");
            }

            // no Session ID; show Login page
            return View();
        }


        public void bindData()
        {

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}