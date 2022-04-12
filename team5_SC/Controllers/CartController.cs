using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using team5_SC.Models;

namespace team5_SC.Controllers
{
    public class CartController : Controller
    {
        private DBContext dbContext;

        public CartController(DBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IActionResult Index()
        {
            Cart carts = dbContext.Carts.FirstOrDefault(x =>
            x.SessionId.ToString() == GetSessionId());

            if (carts == null)
            {
                dbContext.Add(new Cart
                {
                    SessionId = Guid.Parse(GetSessionId())
                });
                dbContext.SaveChanges();
            }

            List<Cart> cartDetails = dbContext.Carts.Where(x =>
            x.SessionId == Guid.Parse(GetSessionId())).ToList();
            List<Product> products = dbContext.Products.ToList();
            ViewBag.cartDetails = cartDetails;
            ViewBag.products = products;
            return View();
        }


        public IActionResult AddToCart([FromBody] Guid req)
        {
            Cart carts = dbContext.Carts.FirstOrDefault(x =>
            x.SessionId.ToString() == GetSessionId());

            if (carts == null)
                {
                dbContext.Add(new Cart
                {
                    SessionId = Guid.Parse(GetSessionId()),
                });
                dbContext.SaveChanges();
            }

            //Check if product in existing session and cart
            Session sessions = dbContext.Sessions.FirstOrDefault(x =>
            x.Id == Guid.Parse(GetSessionId()));
            Product product = dbContext.Products.FirstOrDefault(x =>
            x.Id.Equals(req));
            Cart cartDetails = dbContext.Carts.FirstOrDefault(x =>
            x.Product.Id == req && x.SessionId == Guid.Parse(GetSessionId()));



            if (carts == null)
            {
                dbContext.Add(new Cart
                {
                    SessionId = Guid.Parse(GetSessionId()),
                    Product = product,
                    Quantity = 1
                });
                dbContext.SaveChanges();
            }
            else
            {
                cartDetails.Quantity += 1;
                dbContext.SaveChanges();
            }
            return Json(new { status = "success" });
        }

        public string GetSessionId()
        {
            string SessionId = Request.Cookies["SessionId"];
            if (Request.Cookies["SessionId"] == null)
            {
                Guid SessionGuidId = Guid.NewGuid();
                //if (!string.IsNullOrWhiteSpace(Request.Cookies["Username"]))
                //{
                // string username = Request.Cookies["Username"];
                // Session sessionId = dbContext.Sessions.FirstOrDefault(x => x.Id.ToString() == username);

                //}
                //else

                // Generate a new random GUID using System.Guid class.

                dbContext.Add(new Session
                {
                    Id = SessionGuidId
                });
                //Response.Cookies.Append("SessionId", tempCartId);
                dbContext.SaveChanges();
                return SessionGuidId.ToString();
            }
            return SessionId;
        }
    }
}
