﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EmailWeb.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Cors;
using System.Security.Claims;

namespace EmailWeb.Controllers
{
    [EnableCors("CorsPolicy")]
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
            //var user = DbContext.User.ToList();
            //return user
            string duong="duong";
            var user =  DbContext.User.Where(x => x.Username ==duong).ToList();
            return user;
        }
       
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]User model)
        {
          
                DbContext.User.Add(new User
                {
                    Username = model.Username,
                    Password = model.Password,
                    Email = model.Email,
                    Fullname = model.Fullname,
                });

                return Ok(await DbContext.SaveChangesAsync());
            
        }
       




    }
}
