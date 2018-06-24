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
using MimeKit;
using MailKit.Net.Smtp;
using System.Collections;

namespace EmailWeb.Controllers
{
    [Authorize]
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Route("api/Emails")]
    public class EmailsController : BaseController
    {
        public EmailsController(webMailContext context,
            IConfiguration configuration) :
            base(context, configuration)
        { }
        
        [HttpGet]
        public IEnumerable<Emails> Get()
        {

            return  DbContext.Email.ToList();
        }

        [HttpPost("/api/savemail")]
        public IActionResult savemail([FromBody]Emails model)
        {
            var messages = new MimeMessage();
            messages.From.Add(new MailboxAddress("vochanhdai2k@gmail.com"));
            messages.To.Add(new MailboxAddress(model.MailTo));
            messages.Subject = model.Subject;
            messages.Body = new TextPart("plain")
            {
                Text = model.Messages
            };
            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate("vochanhdai2k@gmail.com", "vochanhdai@2K");
                client.Send(messages);
                client.Disconnect(true);
            }
            DbContext.Email.Add(new Emails
            {
                MailTo = model.MailTo,
                Subject = model.Subject,
                Messages = model.Messages,
                UserId = model.UserId
            });
            DbContext.SaveChanges();
            return Ok("done");

        }
        // GET: api/Emails/username
        [HttpGet("/api/getmail/{username}")]
        public IEnumerable GetMailByUser(string username)
        {
            //var user=  DbContext.User.Where(x => x.Username == username).SingleOrDefault();
            //if (user.Role != 1)
            //{
            //    return DbContext.Email.ToList();
            //}
             return DbContext.Email.Where(x => x.User.Username == username).ToList();

        }
        //// GET: api/Emails/5
        //[HttpGet("{id}", Name = "GetEmail")]
        //public Emails GetID(int id)
        //{
        //    var emails = DbContext.Email.FirstOrDefault(e => e.UserId == id);
        //    return emails;

        //}


        //// PUT: api/Emails/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

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
