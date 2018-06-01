using System.Linq;
using System.Net.Http;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using EmailWeb.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Web.Http.Results;
using Microsoft.EntityFrameworkCore;

namespace EmailWeb.Controllers
{

    [Produces("application/json")]
    [Route("api/Emails")]
    public class EmailsController : BaseController
    {

        public EmailsController(webMailContext context,
           IConfiguration configuration) :
           base(context, configuration)
        { }
        //// GET: api/Emails
        [HttpGet]
        public JsonResult Get()
        {
            var emails = DbContext.Email.ToArray();
            return Json(data: emails);
        }
        //GET: api/Emails/?userid
        public IEnumerable<Emails> GetMailByUser(int userID = 0)
        {
  
                return DbContext.Email.Where(x => x.UserId == userID).ToList();
            
            
        }
        // GET: api/Emails/5
        [HttpGet("{id}", Name = "GetEmail")]
        public Emails GetID(int id)
        {
            var emails = DbContext.Email.FirstOrDefault(e => e.UserId == id);
            return emails;
          
        }
        
        // POST: api/Emails
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Emails/5
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
