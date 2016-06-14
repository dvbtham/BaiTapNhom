using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace itforum_teamwork.Models
{
    public class MailModel
    {
        [Required(ErrorMessage="Vui lòng nhập họ tên"), Display(Name = "Họ tên")]
        public string FromName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Email"), Display(Name = "Email bạn"), EmailAddress(ErrorMessage="Định dạng Email không chính xác")]
        public string FromEmail { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tiêu đề"), Display(Name = "Tiêu đề")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập nội dung"), Display(Name = "Nội dung")]
        [AllowHtml]
        public string Message { get; set; }
    }
}