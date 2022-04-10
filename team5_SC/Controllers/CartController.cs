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
            Session session = GetSession();

            if (session == null)
            {
                return RedirectToAction("Index", "Logout");
            }

            User user = dbContext.Users.FirstOrDefault(x =>
            x.Username == Request.Cookies["Username"]);

            List<Cart> cartDetails = dbContext.Cart.Where(x =>
             x.UserId == user.UserId).ToList();

            List<Product> products = dbContext.Product.ToList();

            ViewBag["cartDetails"] = cartDetails;
            ViewBag["products"] = products;

            return View();
        }

        public IActionResult AddToCart([FromBody] string req)
        {
            Console.WriteLine("uuid:" + req);                                                  //uuid?

            Guid productId = Guid.Parse(req);
            if (Request.Cookies["SessionId"] == null)
            {
                return null;
            }

            string username = Request.Cookies["Username"];
            User user = dbContext.User.FirstOrDefault(x =>
            x.Username == username);

            Product product = dbContext.Product.FirstOrDefault(x =>
           x.ProductId.Equals(req));

            Cart cartDetails = dbContext.Cart.FirstOrDefault(x =>
            x.ProductId == productId && x.UserId == user.UserId);

            if (cartDetails == null)
            {
                dbContext.Add(new Cart
                {
                    UserId = user.UserId,
                    ProductId = productId,
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

        public IActionResult UpdateCart()
        {
            int CartNum = 0;
            if (Request.Cookies["SessionId"] == null)
            {
                return null;
            }
            Console.WriteLine("Username:" + Request.Cookies["Username"]);



            User user = dbContext.User.FirstOrDefault(x => x.Username == Request.Cookies["Username"]);
            List<Cart> cartDetails = dbContext.Cart.Where(x =>
                x.UserId == user.UserId).ToList();
            foreach (Cart cartDetail in cartDetails)
            {

                CartNum += cartDetail.Quantity;

            }
            Console.WriteLine("cartDetail.Quantity : " + CartNum);
            return Json(new { quantity = CartNum });
        }

        public IActionResult ContinueShopping()
        {

            if (Request.Cookies["SessionId"] != null)
            {
                Guid sessionId = Guid.Parse(Request.Cookies["SessionId"]);
                Session session = dbContext.Session.FirstOrDefault(x =>
                    x.Id == sessionId
                );

                if (session == null)
                {
                    return RedirectToAction("Index", "Logout");
                }

                
                return RedirectToAction("AllProducts", "Product");                                                        // we need AllProducts Action for this line of code...
            }

            
            return View();
        }

        public IActionResult CartCounter([FromBody] ProductCounter productCounter)           //We need a productcounter model for this
        {
            if (Request.Cookies["SessionId"] == null)
            {
                return null;
            }

            User user = dbContext.User.FirstOrDefault(x => x.Username == Request.Cookies["Username"]);

           
            Cart cartDetail = dbContext.Cart.FirstOrDefault(x =>
                  x.UserId == user.UserId && Guid.Parse(productCounter.ProductId) == x.ProductId);

            if (productCounter.ClassName == "num_count")
            {
                Console.WriteLine(" num_count + productid:" + productCounter.ProductId);
                Console.WriteLine(" num_count + productid:" + productCounter.ItemQuantity);
                if (productCounter.ItemQuantity == null)
                {
                    productCounter.ItemQuantity = "0";
                }
                int qty = Convert.ToInt32(productCounter.ItemQuantity);
                cartDetail.Quantity = qty;
                dbContext.SaveChanges();
            }

            if (productCounter.ClassName == "count_i")
            {
                Console.WriteLine(" count_i + productid:" + productCounter.ProductId);

                cartDetail.Quantity += 1;
                dbContext.SaveChanges();
            }
            else if (productCounter.ClassName == "count_d")
            {
                Console.WriteLine(" count_d + productid:" + productCounter.ProductId);

                if (Request.Cookies["SessionId"] == null)
                {
                    return null;
                }

            
                if (cartDetail.Quantity <= 0)
                {
                    dbContext.Remove(cartDetail);
                }
                else
                {
                    cartDetail.Quantity -= 1;
                }


                dbContext.SaveChanges();

            }
            else if (productCounter.ClassName == "delete_i")
            {
                Console.WriteLine(" delete_i + productid:" + productCounter.ProductId);

                dbContext.Remove(cartDetail);

                dbContext.SaveChanges();
            }

            List<Cart> cartDetails = dbContext.Cart.Where(x =>
                      x.UserId == user.UserId).ToList();


            return Json(new { status = "success" });

        }

        public IActionResult UpdateCartTotalAmount()
        {

            if (Request.Cookies["SessionId"] == null)
            {
                return null;
            }
            Console.WriteLine("Username:" + Request.Cookies["Username"]);

            // retrieve current user instance

            User user = dbContext.User.FirstOrDefault(x => x.Username == Request.Cookies["Username"]);

            // retrieve current user's cartdetail  
            List<Cart> cartDetails = dbContext.Cart.Where(x =>
                x.UserId == user.UserId).ToList();

            double sum = 0;
            foreach (Cart cartDetail in cartDetails)

            {
                Product product = dbContext.Product.FirstOrDefault(x => x.ProductId == cartDetail.ProductId);
                sum += cartDetail.Quantity * product.ProductPrice;
            }

            Console.WriteLine("cartDetail sum : " + sum);
            return Json(new { carttotal = sum });
        }

        public IActionResult AddCheckoutItemstoPurchaseHistory()
        {

            Session session = GetSession();
            User user = dbContext.User.FirstOrDefault(x =>
            x.Username == Request.Cookies["Username"]);

            List<Cart> cartDetails = dbContext.Cart.Where(x =>
                  x.UserId == user.UserId).ToList();

            Console.WriteLine("Add Checkout Items to Purchase History : " + Request.Cookies["LoginStatus"]);
            Console.WriteLine("Add Checkout Items to Purchase History : " + session.Id);
            if (session != null && Request.Cookies["LoginStatus"].Equals("0"))
            {
                return RedirectToAction("Index", "Login");
            }

            if (session != null || Request.Cookies["LoginStatus"].Equals("1"))
            {
                foreach (Cart cartDetail in cartDetails)
                {
                    UserPurchaseHistory userPurchaseHistory = new UserPurchaseHistory();                                //We need userpurchase history model for this part.

                    dbContext.Add(new UserPurchaseHistory
                    {
                        PurchaseId = cartDetail.CartDetailId,
                        ProductId = cartDetail.ProductId,
                        UserId = user.UserId,
                        Purchase_on = DateTime.Now,
                        ActivationCodes = GenerateActCodes(cartDetail.Quantity)


                    });
                    Console.WriteLine("purchaseid :" + cartDetail.CartDetailId);
                    Console.WriteLine("product id :" + cartDetail.ProductId);
                };

                ClearCart();

                return RedirectToAction("Index", "Checkout");
            }
            return RedirectToAction("Index", "Login");
        }

        public IActionResult ClearCart()
        {

            if (Request.Cookies["SessionId"] == null)
            {
                return null;
            }


            List<Cart> cartDetails = dbContext.Cart.Where(x =>
               x.User.Username == Request.Cookies["Username"]).ToList();

            foreach (Cart cartDetail in cartDetails)
            {
                Console.WriteLine("clear cartDetail id" + cartDetail.CartDetailId);
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

        private static List<string> GenerateActCodes(int quantity)                                            //No idea what this is...
        {


            string prefix = "ActCode";

            List<string> actcodelist = new List<string>();
            for (int i = 0; i < quantity; i++)
            {

                actcodelist.Add(prefix + Guid.NewGuid().ToString());
            }

            foreach (string str in actcodelist)
            {
                Console.WriteLine("actcodelist str " + str);
            }
            return actcodelist;
        }

        private Session GetSession()
        {
            if (Request.Cookies["SessionId"] == null)
            {
                return null;
            }

            Guid sessionId = Guid.Parse(Request.Cookies["SessionId"]);
            Session session = dbContext.Session.FirstOrDefault(x =>
                x.Id == sessionId
            );

            return session;
        }



    }
}
