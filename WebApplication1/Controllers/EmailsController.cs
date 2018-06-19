using System.Linq;
using System.Net.Http;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using EmailWeb.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Web.Http.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using System.Threading.Tasks;

namespace EmailWeb.Controllers
{

    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Route("api/Emails")]
    public class EmailsController : BaseController
    {

        public EmailsController(webMailContext context,
           IConfiguration configuration) :
           base(context, configuration)
        { }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Emails model)
        {

            DbContext.Email.Add(new Emails
            {
                Email = model.Email,
                Subject = model.Subject,
                Messages = model.Messages,
                UserId = model.UserId
            });

            return Ok(await DbContext.SaveChangesAsync());

        }
        // GET: api/Emails/username
        [HttpGet("/api/getmail/{username}")]
        public IEnumerable<Emails> GetMailByUser(string username)
        {
            //var user = DbContext.User.Where(x => x.Username == username).SingleOrDefault();
                return DbContext.Email.Where(x => x.User.Username == username).ToList();
            
            
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
            var itemremove = DbContext.Email.SingleOrDefault(x => x.Id==id);
            if (itemremove != null)
            {
                DbContext.Email.Remove(itemremove);
                DbContext.SaveChanges();

            }
            
        }
    }
}
