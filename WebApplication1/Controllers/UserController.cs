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
        ////// GET: api/Users
        //public HttpResponseMessage Get(string gender="all")
        //{
        //    string username = Thread.CurrentPrincipal.Identity.Name;
        //    return "0k";
        //}
        [HttpGet]
        //public IEnumerable<User> Get()
        //{
        //    var user = DbContext.User.ToList();
        //    return user;
        //}
        // GET: api/Users/?username
        //public IEnumerable<User> Get(string username)
        //{

        //    return DbContext.User.Where(x => x.Username == username).ToList();

        //}
        //public static bool  Login<(string username,string password)
        //{

        //    DbContext.User
        //}

        // GET: api/User/5
        [HttpGet("{username}", Name = "GetUser")]
        public IEnumerable<User> Get(string username)
        {
            return DbContext.User.Where(x => x.Username == username).ToList();
        }
        
        // POST: api/User
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/User/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
