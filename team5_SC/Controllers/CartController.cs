using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using team5_SC.Models;
using team5_SC.DataHelper;

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
            Session session = GetSession();

            User user = null;
            List<Cart> carts = null;
            int userCartQty = 0;


            if (session == null)
            {
                return RedirectToAction("Index", "Logout");
            }

            if (session != null && Request.Cookies["Username"] == null)
            {
                carts = dbContext.Carts.Where(x =>
                    x.SessionId == session.Id
                ).ToList();

                userCartQty = CartQty.get(session, null, dbContext);
            }

            else
            {
                user = dbContext.Users.FirstOrDefault(x => x.Id == session.User.Id);

                carts = dbContext.Carts.Where(x =>
                    x.User.Id == user.Id
                ).ToList();

                userCartQty = CartQty.get(session, user, dbContext);
            }

            ViewData["userCartQty"] = userCartQty;
            ViewData["carts"] = carts;
            return View();
        }

        //Add to cart

        public IActionResult AddToCart([FromBody] ReqToAddProducts req)
        {
            Guid reqProductId = Guid.Parse(req.ProductId);

            Session session = GetSession();

            int cartqty = 0;

            if (session == null && Request.Cookies["Username"] == null)
            {
                Guid SessionId = Guid.NewGuid();
                dbContext.Add(new Session
                {
                    Id = SessionId
                });
                dbContext.SaveChanges();

                Response.Cookies.Append("SessionId", SessionId.ToString());

                Product product = dbContext.Products.FirstOrDefault(x =>
                x.Id == reqProductId);

                dbContext.Add(new Cart
                    {
                        Product = product,
                        Quantity = 1,
                        SessionId = SessionId
                    });
                dbContext.SaveChanges();

                cartqty = 1;

                //return Json(new { addstatus = "success", cartqty = 1 });
            }

            else if(session != null && Request.Cookies["Username"] == null)
            {
                Cart cartDetails = dbContext.Carts.FirstOrDefault(x =>
                x.Product.Id == reqProductId && x.SessionId == session.Id);

                if(cartDetails == null)
                {
                    Product product = dbContext.Products.FirstOrDefault(x =>
                        x.Id == reqProductId
                    );
                    dbContext.Add(new Cart
                    {
                        Product = product,
                        Quantity = 1,
                        SessionId = session.Id
                    });
                    dbContext.SaveChanges();

                }
                else
                {
                    cartDetails.Quantity += 1;
                    dbContext.SaveChanges();
                }

                cartqty = CartQty.get(session, null, dbContext);

                //return Json(new { addstatus = "success" , cartqty = CartQty.get(session, null, dbContext) });
            }

            else
            {
                User user = dbContext.Users.FirstOrDefault(x => x.Id == session.User.Id);

                Product product = dbContext.Products.FirstOrDefault(x =>
                x.Id == reqProductId);

                Cart cartDetails = dbContext.Carts.FirstOrDefault(x =>
                x.Product.Id == reqProductId && x.User.Id == user.Id);

                if (cartDetails == null)
                {
                    dbContext.Add(new Cart
                    {
                        User = user,
                        Product = product,
                        Quantity = 1,
                        SessionId = session.Id
                    });
                    dbContext.SaveChanges();
                }
                else
                {
                    cartDetails.Quantity += 1;
                    dbContext.SaveChanges();
                }

                cartqty = CartQty.get(session, user, dbContext);

                //return Json(new { addstatus = "success", cartqty = CartQty.get(session, user, dbContext) });
            }

            return Json(new { addstatus = "success", cartqty = cartqty });
        }

        public IActionResult CountIcreOrDcre([FromBody] CartUpdate req)
        {
            if (Request.Cookies["SessionId"] == null)
            {
                return null;
            }
            
            Session session = GetSession();

            // filter selected item by product id and current user id / session id
            Cart cartDetail;
            if (session.User != null)
            {
                cartDetail = dbContext.Carts.FirstOrDefault(x =>
                  x.User.Id == session.User.Id && x.Product.Id == Guid.Parse(req.ProductId)
                );
            }
            else
            {
                cartDetail = dbContext.Carts.FirstOrDefault(x =>
                  x.SessionId == session.Id && x.Product.Id == Guid.Parse(req.ProductId)
                );
            }

            if (req.ClassName == "num_count")
            {
                Console.WriteLine(" num_count + productid:" + req.ProductId);
                Console.WriteLine(" num_count + productid:" + req.ItemQuantity);
                if (req.ItemQuantity == null)
                {
                    req.ItemQuantity = "0";
                }
                int qty = Convert.ToInt32(req.ItemQuantity);
                cartDetail.Quantity = qty;
                dbContext.SaveChanges();
            }

            if (req.ClassName == "count_i")
            {
                Console.WriteLine(" count_i + productid:" + req.ProductId);

                cartDetail.Quantity += 1;
                dbContext.SaveChanges();
            }
            else if (req.ClassName == "count_d")
            {
                Console.WriteLine(" count_d + productid:" + req.ProductId);

                if (Request.Cookies["SessionId"] == null)
                {
                    return null;
                }

                // if quantity is 0, delete the record 
                if (cartDetail.Quantity == 1)
                {
                    dbContext.Remove(cartDetail);
                }
                else
                {
                    cartDetail.Quantity -= 1;
                }
                dbContext.SaveChanges();

            }
            else if (req.ClassName == "delete_btn")
            {
                Console.WriteLine(" delete_btn + productid:" + req.ProductId);

                dbContext.Remove(cartDetail);
                dbContext.SaveChanges();
            }
            // all products in view cart for specific user 
            List<Cart> cartDetails = dbContext.Carts.Where(x =>
                      x.SessionId == session.Id).ToList();
            return Json(new { status = "success" });
        }

        public IActionResult UpdateCartTotalAmount()
        {
            if (Request.Cookies["SessionId"] == null)
            {
                return null;
            }

            // retrieve current user instance
            User user = dbContext.Users.FirstOrDefault(x => x.Username == Request.Cookies["Username"]);

            // retrieve current user's cartdetail
            List<Cart> cartDetails = dbContext.Carts.Where(x =>
                x.User.Id == user.Id
            ).ToList();

            double sum = 0;

            foreach (Cart cartDetail in cartDetails)
            {
                Product product = dbContext.Products.FirstOrDefault(x => x.Id == cartDetail.Product.Id);
                sum += cartDetail.Quantity * product.Price;
            }

            return Json(new { carttotal = sum });
        }
        public IActionResult Checkout()
        {
            Session session = GetSession();

            if(session != null && Request.Cookies["Username"]==null)
            {
                return RedirectToAction("Index", "Login");
            }
                
            User user = dbContext.Users.FirstOrDefault(x =>
            x.Username == Request.Cookies["Username"]);

            List<Cart> Carts = dbContext.Carts.Where(x =>
            x.User.Id == user.Id).ToList();

            if (session != null)
            {
                
                foreach (Cart cartDetail in Carts)
                {
                    dbContext.Add(new MyPurchase
                    {
                        Id = cartDetail.Id,
                        Qty = cartDetail.Quantity,
                        UserId = user.Id,
                        PurchaseDate = DateTime.Now,
                        Product = cartDetail.Product,
                        ActivationCodes = GenerateActivationCode(cartDetail.Quantity, cartDetail.Product.Id)
                    });

                    dbContext.RemoveRange(cartDetail);
                }

                dbContext.SaveChanges();
                RedirectToAction("ClearCart");
                return RedirectToAction("Index", "MyPurchases");
            }
            return RedirectToAction("Index", "Login");
        }

        public IActionResult ClearCart()
        {
            if (Request.Cookies["SessionId"] == null)
            {
                return null;
            }

            List<Cart> cartDetails = dbContext.Carts.Where(x =>
            x.User.Username == Request.Cookies["Username"]).ToList();

            foreach (Cart cartDetail in cartDetails)
            {
                Console.WriteLine("clear cartDetail id" + cartDetail.Id);
            }

            try
            {
                dbContext.RemoveRange(cartDetails);
                dbContext.SaveChanges();
            }
            catch
            {
                Console.WriteLine("error remove item from cart");
                return Json(new { status = "fail" });
            }
            return RedirectToAction("Index", "Cart");
        }

        private Session GetSession()
        {
            if (Request.Cookies["SessionId"] == null)
            {
                return null;
            }

            Guid sessionId = Guid.Parse(Request.Cookies["SessionId"]);
            Session session = dbContext.Sessions.FirstOrDefault(x =>
                x.Id == sessionId
            );

            return session;
        }

        private List<ActivationCode> GenerateActivationCode(int quantity,Guid pid)
        {
            List<ActivationCode> codes = new List<ActivationCode>();

            for(int i = 1; i <= quantity; i++)
            {
                ActivationCode code = new ActivationCode
                {
                    ProductId = pid
                };
                codes.Add(code);
            }
            return codes;
        }

    }
}
