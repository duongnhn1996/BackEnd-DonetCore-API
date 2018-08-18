using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmailWeb.Models
{
    public partial class Register
    {
        public Register()
        {
            EmailNavigation = new HashSet<Emails>();
        }

        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [RegularExpression(@"^[a-zA-Z0-9._-]+$", ErrorMessage = "Username is not a valid! You only use 'a - z','A - Z','0 - 9','.','_' ")]
        public string Username { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false)]
        [EmailAddress]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false)]
        [RegularExpression(@"^[A-Za-zÀ-ú]+ [A-Za-zÀ-ú]+$", ErrorMessage="Full Name not valid")]
        public string Fullname { get; set; }
        public int? Role { get; set; }

        public ICollection<Emails> EmailNavigation { get; set; }

        public string ReCaptcha { get; set; }
    }
    public class CaptchaVerification
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("error-codes")]
        public IList Errors { get; set; }
    }

}
