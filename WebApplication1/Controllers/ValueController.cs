using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
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
            // demo
            return DbContext.User.Where(x => x.Username == username).ToList();
            // khong tra ve loi
            //return DbContext.User.Where(x => x.Username == username || true).ToList();
        }
        [HttpGet("/api/del/{id}")]
        public IActionResult UserDel(int id)
        {   //csrf
            var itemremove = DbContext.User.SingleOrDefault(x => x.Id == id);
            if (itemremove != null)
            {
                DbContext.User.Remove(itemremove);
                DbContext.SaveChanges();
                return Ok("Done");

            }
            return StatusCode((int)HttpStatusCode.NotAcceptable, "Khong xoa duoc");
        }
        //[HttpGet("/api/get2/{username}")]
        //public IEnumerable GetMailByUser2(string username)
        //{
        //    var connectionString = "Data Source=DESKTOP-3OLT4EN;Initial Catalog=myEmail";
        //    SqlConnection connection = new SqlConnection(connectionString);

        //    var cmd = new SqlCommand("SELECT * FROM Email ",connection);
        //   // cmd.Parameters.AddWithValue("@id", id.Text);
        //    connection.Open();
        //    SqlDataReader n = cmd.ExecuteReader();
        //    int contact = (int)n["ID"];

        //    yield return contact;
        //}
        [Authorize]
        [HttpGet("/api/test")]
        public IActionResult Test()
        {

            var claim = HttpContext.User.Claims.First(c => c.Type == "username");
            var userName = claim.Value;
            return Ok(userName);
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
        // debug => window=> autos
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
