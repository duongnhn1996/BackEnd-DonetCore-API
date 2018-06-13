using System;
using System.Collections.Generic;

namespace EmailWeb.Models
{
    public partial class User
    {
        public User()
        {
            EmailNavigation = new HashSet<Emails>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Fullname { get; set; }
        public int? Role { get; set; }

        public ICollection<Emails> EmailNavigation { get; set; }

        
    }
}
