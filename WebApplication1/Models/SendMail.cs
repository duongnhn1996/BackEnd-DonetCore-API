using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmailWeb.Models
{
    public class SendMail
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
        public string ReCaptcha { get; set; }
    }
}
