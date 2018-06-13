using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EmailWeb.Models;
using EmailWeb.Controllers;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading;
using System.Security.Claims;

namespace EmailWeb.Controllers
{
    [Produces("application/json")]
    [Route("api/User")]
    public class UserController : BaseController
    {
        public UserController(webMailContext context,
             IConfiguration configuration) :
             base(context, configuration)
        { }

        [HttpGet]
        public IEnumerable<User> Get()
        {
            var user = DbContext.User.ToList();
            return user;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]User model)
        {
            using (var context = new webMailContext())
            {
                context.User.Add(new User
                {
                    Username = model.Username,
                    Password = model.Password,
                    Email = model.Email,
                    Fullname = model.Fullname,
                    Role = model.Role
                });

                return Ok(await context.SaveChangesAsync());
            }
        }



    }
}
