using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailWeb.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmailWeb.Controllers
{
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Route("api/Mail")]
    public class MailController : Controller
    {
        // GET: api/Mail
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]User model)
        {

            //DbContext.User.Add(new User
            //{
            //    Username = model.Username,
            //    Password = model.Password,
            //    Email = model.Email,
            //    Fullname = model.Fullname,
            //});

            return Ok(model.Username);

        }
        // GET: api/Mail/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        //// POST: api/Mail
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}
        
        // PUT: api/Mail/5
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
