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
using Microsoft.Security.Application;
using System.Security.Claims;
using System.Text;
using System;
using Newtonsoft.Json;

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
        public async Task<IActionResult> savemailAsync([FromBody]SendMail model)
        {
           
            if (ModelState.IsValid)
            {
                var result = await VerifyCaptcha(model.ReCaptcha);
                if (!result.Success)
                {
                    return StatusCode((int)HttpStatusCode.NotAcceptable, "Captcha is not valid");
                }
                var messages = new MimeMessage();
                messages.From.Add(new MailboxAddress("smarteraemail@gmail.com"));
                messages.To.Add(new MailboxAddress(model.MailTo));
                messages.Subject = model.Subject;
                var bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = string.Format(model.Messages);
                messages.Body = bodyBuilder.ToMessageBody();
                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate("testmailduongnguyen@gmail.com", "daylaMATKHAUr4tkh0#");
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
                return Ok("Done");
            }
            return StatusCode((int)HttpStatusCode.NotAcceptable, "Du lieu nhap khong hop le!");

        }
        // GET: api/Emails/username
        [HttpGet("/api/getmail/{username}")]
        public IEnumerable GetMailByUser(string username)
        {
  
            return DbContext.Email.Where(x => x.User.Username == username).ToList(); 
        }


        // [HttpDelete("{id}")]
  
        [HttpPost("/api/del/{id}")]
        public IActionResult RemoveEmail(int id)
        {
            var itemremove = DbContext.Email.SingleOrDefault(x => x.Id==id);
            if (itemremove != null)
            {
                DbContext.Email.Remove(itemremove);
                DbContext.SaveChanges();
                return Ok("Done");

            }
            return StatusCode((int)HttpStatusCode.NotAcceptable, "Khong xoa duoc");

        }
        private async Task<CaptchaVerification> VerifyCaptcha(string captchaResponse)
        {
            string userIP = string.Empty;
            var ipAddress = Request.HttpContext.Connection.RemoteIpAddress;
            if (ipAddress != null) userIP = ipAddress.MapToIPv4().ToString();
            var payload = string.Format("&secret={0}&remoteip={1}&response={2}", "6LeNGmIUAAAAAGYrgNz0EADO6LIZL89OV0B3nV9Y",
                userIP, captchaResponse
            );

            var client = new HttpClient();
            client.BaseAddress = new Uri("https://www.google.com");
            var request = new HttpRequestMessage(HttpMethod.Post, "/recaptcha/api/siteverify");
            request.Content = new StringContent(payload, Encoding.UTF8, "application/x-www-form-urlencoded");
            var response = await client.SendAsync(request);
            return JsonConvert.DeserializeObject<CaptchaVerification>(response.Content.ReadAsStringAsync().Result);
        }

    }
}
