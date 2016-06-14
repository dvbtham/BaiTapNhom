using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace itforum_teamwork.Areas.Admin.Models
{
    public class UserViewModel
    {
        [Key]
        [Display(Name = "ID")]
        public long UserID { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Vui lòng nhập Email")]
        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập lại mật khẩu")]
        [StringLength(50, ErrorMessage = "Mật khẩu ít nhất phải  6 ký tự", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("Password", ErrorMessage = "Mật khẩu không trùng khớp")]
        public string ConfirmPassword { get; set; }

        [StringLength(150)]
        public string Avatar { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Vui lòng nhập họ tên")]
        [Display(Name = "Họ tên")]
        public string Name { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [Display(Name = "Ngày đăng ký")]
        public DateTime? CreatedDate { get; set; }

        [Display(Name = "Quyền")]
        [Required(ErrorMessage="Chưa đặt quyền người dùng")]
        public int? RoleID { get; set; }

        [Display(Name = "Kích hoạt")]
        public bool? ConfirmedByEmail { get; set; }

        [Display(Name = "Trạng thái")]
        public bool? Status { get; set; }
    }
}