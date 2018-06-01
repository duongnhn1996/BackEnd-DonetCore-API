using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using EmailWeb.Models;

namespace EmailWeb.Controllers
{
    public class BaseController : Controller
    {
        protected webMailContext DbContext;

        public BaseController(webMailContext context,
            IConfiguration configuration)
        {
            DbContext = context;
        }
    }
}