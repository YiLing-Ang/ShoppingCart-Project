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

//[assembly: Guid("dc21b74d-a2e1-471f-9d27-8342a6441457")]
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using team5_SC.Models;


namespace team5_SC.Controllers
{
    public class LogoutController : Controller
    {
        private readonly DBContext dbContext;

        public LogoutController(DBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            // remove session from our database
            if (Request.Cookies["SessionId"] != null)
            {
                Guid sessionId = Guid.Parse(Request.Cookies["sessionId"]);

                Session session = dbContext.Sessions.FirstOrDefault(x => x.Id == sessionId);
                if (session != null)
                {
                    // delete session record from our database;
                    dbContext.Remove(session);

                    // commit to save changes
                    dbContext.SaveChanges();
                }
            }

            // ask client to remove these cookies so that
            // they won't be sent over next time
            Response.Cookies.Delete("SessionId");
            Response.Cookies.Delete("Username");

            return RedirectToAction("Index", "Login");
        }
    }
}