using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using team5_SC.Models;

namespace team5_SC.Controllers
{
    public class HomeController : Controller
    {
        private readonly DBContext dbContext;

        public HomeController(DBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index(string? searchStr)
        {
            Session session = ValidateSession();

            List<Product> products;

            if (searchStr == null)
            {
                searchStr = "";

                products = dbContext.Products.Where(x =>
                    x.Id != null
                ).ToList();
            }
            else
            {
                products = dbContext.Products.Where(x =>
                    x.Name.Contains(searchStr)
                ).ToList();
            }

            ViewData["searchStr"] = searchStr;
            ViewData["products"] = products;
            return View();
        }

        private Session ValidateSession()
        {
            // check if there is a SessionId cookie
            if (Request.Cookies["SessionId"] == null)
            {
                return null;
            }

            // convert into a Guid type (from a string type)
            Guid sessionId = Guid.Parse(Request.Cookies["SessionId"]);
            Session session = dbContext.Sessions.FirstOrDefault(x =>
                x.Id == sessionId
            );

            return session;
        }

    }
}
