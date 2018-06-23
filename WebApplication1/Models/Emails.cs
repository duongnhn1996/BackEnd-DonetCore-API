using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmailWeb.Models
{
    public partial class Emails
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Subject { get; set; }

        [Required(AllowEmptyStrings = false)]
        [EmailAddress]
        public string MailTo { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Messages { get; set; }
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
