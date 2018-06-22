using System;
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

        

        //[HttpGet]
        //public IActionResult Get()
        //{
        ////    //var user = DbContext.User.ToList();
        ////    //return user
        ////    string duong="duong";
        ////    //var user = BCrypt.Net.BCrypt.Verify(duong, "$10$jEPbLVL1DTHHf8fhry3mcORc3AmDKHAPHFaOQJJqDbShk9z7Bwtby");
        ////    return Ok(user);
        ////}

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]User model)
        {
            //if (ModelState.IsValid) { // mang giá trị false khi 1 thuộc tính nào đó mang giá trị không hợp lệ.
            if (DbContext.User.Any(x => x.Username == model.Username))
                return StatusCode(400, "User exits please try againt!");
            var hashPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);
            DbContext.User.Add(new User
                {
                    Username = model.Username,
                    Password = hashPassword,
                    Email = model.Email,
                    Fullname = model.Fullname,
                });

            return Ok(await DbContext.SaveChangesAsync());
            //}
            //return BadRequest("Register not success!");
            
        }
       




    }
}
