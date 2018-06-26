using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EmailWeb.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Cors;
using System.Security.Claims;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

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
        public async Task<IActionResult> Post([FromBody]Register model)
        {

            //if (ModelState.IsValid)
            { // mang giá trị false khi 1 thuộc tính nào đó mang giá trị không hợp lệ.
                var result = await VerifyCaptcha(model.ReCaptcha);
                if (!result.Success)
                {
                    return StatusCode((int)HttpStatusCode.NotAcceptable, "Captcha is not valid");
                }

                if (DbContext.User.Any(x => x.Username == model.Username))
                    return StatusCode((int)HttpStatusCode.NotAcceptable, "USERNAME EXITS");
                if (DbContext.User.Any(x => x.Email == model.Email))
                    return StatusCode((int)HttpStatusCode.NotAcceptable, "EMAIL EXITS");
                var hashPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);
                DbContext.User.Add(new User
                {
                    Username = model.Username,
                    Password = hashPassword,
                    Email = model.Email,
                    Fullname = model.Fullname
                });

                return Ok(await DbContext.SaveChangesAsync());
            }
            //return StatusCode((int)HttpStatusCode.NotAcceptable, "Register failed");

        }

            private async Task<CaptchaVerification> VerifyCaptcha(string captchaResponse)
        {
            string userIP = string.Empty;
            var ipAddress = Request.HttpContext.Connection.RemoteIpAddress;
            if (ipAddress != null) userIP = ipAddress.MapToIPv4().ToString();
            var payload = string.Format("&secret={0}&remoteip={1}&response={2}","6LedxGAUAAAAAPiUiloBA7bx1_YrVxlAus3jLkX4",userIP,captchaResponse
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
