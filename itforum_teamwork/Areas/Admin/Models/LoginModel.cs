using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace itforum_teamwork.Areas.Admin.Models
{
    public class LoginModel
    {
        public Account _Account { get; set; }

        [Display(Name="Ghi nhớ?")]
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }
    }
}