﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using EmailWeb.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace EmailWeb.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    public class AuthController : BaseController
    {
        public AuthController(webMailContext context, IConfiguration configuration) : base(context, configuration)
        {
        }


     
        [HttpPost("token")]
        public IActionResult Token()
        {
            //string tokenString = "test";
            var header = Request.Headers["Authorization"];
            if (header.ToString().StartsWith("Basic"))
            {
                var credValue = header.ToString().Substring("Basic ".Length).Trim();
                var usernameAndPassenc = Encoding.UTF8.GetString(Convert.FromBase64String(credValue)); //admin:pass
                //var usernameAndPassenc = credValue;
                var usernameAndPass = usernameAndPassenc.Split(":");
                //check in DB username and pass exist                
                if (DbContext.User.Any(x => x.Username == usernameAndPass[0].ToString() &&  BCrypt.Net.BCrypt.Verify(usernameAndPass[1].ToString(), x.Password)))
                {
                    var usr = DbContext.User.Where(x => x.Username == usernameAndPass[0]).SingleOrDefault();
                    var claimsdata = new[] { //claimns là nội dung ở phần payload, info user
                        new Claim("username", usernameAndPass[0]),
                        new Claim("email", usr.Email.ToString()),
                        new Claim("id", usr.Id.ToString()),
                        new Claim("Fullname", usr.Fullname.ToString()),
                        new Claim("role", usr.Role.ToString())
                    };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("thisisverykhokey"));
                    var signInCred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
                    var token = new JwtSecurityToken(
                         issuer: "localhost:4200",
                         audience: "localhost:4200",
                         expires: DateTime.Now.AddMinutes(15), // 15 phut
                         claims: claimsdata,
                         signingCredentials: signInCred
                        );
                    var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                    return Ok(tokenString);
                }
            }
            return BadRequest("wrong request");

            //// return View();
        }
    }
}
