using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace itforum_teamwork.Areas.Admin.Models
{
    public class ContactModel
    {
        [Key]
        [Display(Name="ID")]
        public int ContactID { get; set; }

        [StringLength(250, ErrorMessage = "Chỉ nhập tối đa 250 ký tự")]
        public string Slogan { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Điện thoại")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        public string Fax { get; set; }

        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }

        [Display(Name = "Trạng thái")]
        public bool? Status { get; set; }
    }
}