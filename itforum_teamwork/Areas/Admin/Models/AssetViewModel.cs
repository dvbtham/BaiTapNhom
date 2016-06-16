using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace itforum_teamwork.Areas.Admin.Models
{
    public class AssetViewModel
    {
        [Key]
        [Display(Name = "ID")]
        public long AssetID { get; set; }

        [StringLength(150)]
        [Display(Name = "Tiêu đề")]
        public string Title { get; set; }

        public int AssetTypeID { get; set; }

        [Display(Name = "Nội dung")]
        public string Content { get; set; }

        [Display(Name = "Hình ảnh")]
        public string Image { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Ngày cập nhật")]
        public DateTime? EditedDate { get; set; }

        [Display(Name = "Tóm tắt")]
        [StringLength(500, ErrorMessage = "Nhập không quá 500 ký tự")]
        public string ShortContent { get; set; }

        [Display(Name = "Ngày đăng")]

        [DataType(DataType.Date)]
        public DateTime? PostedDate { get; set; }

        [Display(Name = "Lượt xem")]
        public long? Views { get; set; }

        public long? UserID { get; set; }
        [Display(Name = "Trạng thái")]
        public bool Status { get; set; }

        [Display(Name = "Link")]
        [StringLength(150)]
        public string Link { get; set; }

        [Display(Name="Link dự phòng")]
        [StringLength(500, ErrorMessage = "Nhập không quá 500 ký tự")]
        public string LinkDuPhong { get; set; }
    }
}