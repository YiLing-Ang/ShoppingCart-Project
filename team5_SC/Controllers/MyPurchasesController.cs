using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using team5_SC.Models;

namespace team5_SC.Controllers
{
    public class MyPurchasesController : Controller
    {
        private DBContext dbContext;
        public MyPurchasesController(DBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            if (Request.Cookies["SessionId"] == null || Request.Cookies["UserId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            User user = dbContext.Users.FirstOrDefault(x =>
                x.Id == Guid.Parse(Request.Cookies["UserId"]));

            List<MyPurchase> purchases = dbContext.MyPurchases.Where(x =>
            x.UserId == user.Id).ToList();

            ViewData["purchases"] = purchases;
            return View();
        }
    }
}
