using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
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



                //if (usernameAndPass[0] == "admin" && usernameAndPass[1] == "admin")
                if (DbContext.User.Any(x => x.Username == usernameAndPass[0] && x.Password == usernameAndPass[1]))
                {
                    var usr = DbContext.User.Where(x => x.Username == usernameAndPass[0]).SingleOrDefault();
                    var claimsdata = new[] {
                        new Claim("username", usernameAndPass[0]),
                        new Claim("email", usr.Email.ToString()),
                        new Claim("Fullname", usr.Fullname.ToString()),
                        new Claim("role", usr.Role.ToString())
                    };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ahbasshfbsahjfbshajbfhjasbfashjbfsajhfvashjfashfbsahfbsahfksdjf"));
                    var signInCred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
                    var token = new JwtSecurityToken(
                         issuer: "mysite.com",
                         audience: "mysite.com",
                         expires: DateTime.Now.AddMinutes(1),
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