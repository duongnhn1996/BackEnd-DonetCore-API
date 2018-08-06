using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using EmailWeb.Models;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmailWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.
                   //AllowAnyOrigin()
                    WithOrigins("http://localhost:4200")
                    .WithMethods("GET", "PUT", "POST", "DELETE")
                    .AllowAnyHeader()
                    .AllowCredentials());
               
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer=true,
                    ValidateAudience=true,
                    ValidateIssuerSigningKey=true,
                    ValidIssuer= "localhost:4200",
                    ValidAudience = "localhost:4200",
                    IssuerSigningKey=  new SymmetricSecurityKey(Encoding.UTF8.GetBytes("thisisverykhokey"))
                   
                };
            });

            services.AddMvcCore(
                options =>
            {
                
                //options.RequireHttpsPermanent = true;
                options.RespectBrowserAcceptHeader = true; // false by default
                                                           //options.OutputFormatters.RemoveType<HttpNoContentOutputFormatter>();
            })

        .AddJsonFormatters();
            var connection = @"Server=localhost;Database=myEmail;Trusted_Connection=True;ConnectRetryCount=0";
            //var connection = @"Data Source=tcp:demoemailwebdbserver.database.windows.net,1433;Initial Catalog=DemoEmailWeb_db;User Id=pinonguyen@demoemailwebdbserver;Password=NAMduong1234";
            services.AddDbContext<webMailContext>(options => options.UseSqlServer(connection));
            services.AddMvc(
                options =>
                {
                   // options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                });
        }
            

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();


            }


            app.UseCors("CorsPolicy");

            app.UseAuthentication();

            app.UseMvc();
        }
      
    }
}
