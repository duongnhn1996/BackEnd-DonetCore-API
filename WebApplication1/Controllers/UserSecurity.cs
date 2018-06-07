using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailWeb.Models;
namespace EmailWeb.Controllers
{
    public class UserSecurity 
    {

        public static bool Login(string username, string password)
        {

            using(webMailContext context = new webMailContext())
            {
                return context.User.Any(user => user.Username.ToLower().Equals(username.ToLower()) && user.Password == password);
            }
        }
    }
}
