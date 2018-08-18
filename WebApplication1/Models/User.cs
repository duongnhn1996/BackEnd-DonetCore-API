using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmailWeb.Models
{
    public partial class User
    {
        public User()
        {
            EmailNavigation = new HashSet<Emails>();
        }

        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [RegularExpression(@"^[a-zA-Z0-9._-]+$")]
        public string Username { get; set; }

        [Required(AllowEmptyStrings = false)]
        
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false)]
        [EmailAddress]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Fullname { get; set; }
        public int? Role { get; set; }

        public ICollection<Emails> EmailNavigation { get; set; }
        
    }
  

}
