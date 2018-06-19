using System;
using System.Collections.Generic;

namespace EmailWeb.Models
{
    public partial class Emails
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Email { get; set; }
        public string Messages { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
