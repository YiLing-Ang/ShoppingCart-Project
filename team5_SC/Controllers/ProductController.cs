using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using team5_SC.Models;

namespace team5_SC.Controllers
{
    public class ProductController : Controller
    {
        private DBContext dbContext;

        public ProductController(DBContext dbContext)
        {
        this.dbContext = dbContext;
        }
        public IActionResult Index(string? searchStr)
        {
            if (searchStr == null)
                searchStr = "";

            List<Product> products = dbContext.Products.Where(x =>
                x.Name != null
            ).ToList();

            ViewData["searchStr"] = searchStr;
            ViewData["products"] = products;
            return View();
        }
    }
}
