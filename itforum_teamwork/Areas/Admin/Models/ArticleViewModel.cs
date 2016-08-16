using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace itforum_teamwork.Areas.Admin.Models
{
    public class ArticleViewModel
    {
        [Key]
        [Display(Name="ID")]
        public long PostID { get; set; }

        [StringLength(250)]
        [Display(Name = "Tiêu đề")]
        public string Title { get; set; }

        [Display(Name = "Người đăng")]
        public long? UserID { get; set; }

        [StringLength(500)]
        [Display(Name = "Tóm tắt")]
        public string ShortContent { get; set; }

        [Display(Name = "Nội dung chi tiết")]
        public string Content { get; set; }

        [Display(Name = "Danh mục")]
        public int? CategoryID { get; set; }

        [Display(Name = "Trạng thái")]
        public bool? Status { get; set; }

        [Display(Name = "Ảnh đại diện")]
        public string Avatar { get; set; }

        [Display(Name = "Ngày đăng")]
        public DateTime? PostedDate { get; set; }

        [Display(Name = "Lượt xem")]
        public int? Views { get; set; }
    }
}