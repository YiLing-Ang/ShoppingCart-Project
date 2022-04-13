using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using team5_SC.Models;

namespace team5_SC.Middleware
{
    public class LoginChecker
    {
        private readonly RequestDelegate next;
        private readonly DBContext dbContext;

        public LoginChecker(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string sessionId = context.Request.Cookies["sessionId"];

            User user = dbContext.Users.FirstOrDefault(x =>
                x.Id == Guid.Parse(sessionId)
            );

            var userId = user.Id;

            if((sessionId == null) && (userId.ToString() == null))
            {
                context.Response.Redirect("/Login");
                return;
            }

            await next(context);
        }
    }
}
