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

        public IActionResult Index(string sortOrder, string searchStr)
        {
            if (Request.Cookies["SessionId"] == null || Request.Cookies["Username"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            User user = dbContext.Users.FirstOrDefault(x =>
            x.Username == Request.Cookies["Username"]);

            List<MyPurchase> myPurchases = dbContext.MyPurchases.OrderByDescending(x => x.PurchaseDate).Where(x => x.UserId == user.Id).ToList();
            ViewData["myPurchases"] = myPurchases;

            ViewBag.sortbyPurchaseDate = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
            ViewBag.sortbyId = sortOrder == "Id" ? "id_desc" : "Id";

            //if (!string.IsNullOrEmpty(searchStr))
            //{
            //    myPurchases = dbContext.MyPurchases.OrderByDescending(x => x.PurchaseDate).Where(x => x.UserId == user.Id && x.Qty == 1).ToList();
            //}
            switch (sortOrder)
            {
                case "date_desc":
                    myPurchases = dbContext.MyPurchases.OrderByDescending(x => x.PurchaseDate).Where(x => x.UserId == user.Id).ToList();
                    ViewData["myPurchases"] = myPurchases;
                    break;
                case "Date":
                    myPurchases = dbContext.MyPurchases.OrderBy(x => x.PurchaseDate).Where(x => x.UserId == user.Id).ToList();
                    ViewData["myPurchases"] = myPurchases;
                    break;
                case "Id":
                    myPurchases = dbContext.MyPurchases.OrderBy(x => x.Id).Where(x => x.UserId == user.Id).ToList();
                    ViewData["myPurchases"] = myPurchases;
                    break;
                case "id_desc":
                    myPurchases = dbContext.MyPurchases.OrderByDescending(x => x.Id).Where(x => x.UserId == user.Id).ToList();
                    ViewData["myPurchases"] = myPurchases;
                    break;
                default:
                    myPurchases = dbContext.MyPurchases.OrderByDescending(x => x.PurchaseDate).Where(x => x.UserId == user.Id).ToList();

                    break;
            }
            return View();
        }
    }
}
