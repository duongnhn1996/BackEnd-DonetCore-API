using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Security.Application;

namespace EmailWeb.Controllers
{
    [Produces("application/json")]
    [Route("api/Values")]
    public class ValueController : BaseController
    {
        public ValueController(webMailContext context,
            IConfiguration configuration) :
            base(context, configuration)
        { }
        // GET: api/Value
        [HttpGet("/api/get/{username}")]
        public IEnumerable GetMailByUser(string username)
        {
            
              return DbContext.Email.Where(x => x.User.Username == username).ToList();
            

        }


        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        //// GET: api/Value/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}
        
        // POST: api/Value
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Value/5
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
