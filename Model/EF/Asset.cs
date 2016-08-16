namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Asset
    {
        [Display(Name = "ID")]
        public int AssetID { get; set; }

        [StringLength(150)]
        [Display(Name = "Tiêu đề")]
        public string Title { get; set; }

        [Display(Name = "Danh mục")]
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
        public DateTime? CreatedDate { get; set; }

        [Display(Name = "Lượt xem")]
        public long? Views { get; set; }

        [Display(Name = "Người đăng")]
        public long? UserID { get; set; }
        [Display(Name = "Trạng thái")]
        public bool Status { get; set; }

        [Display(Name = "Link")]
        [StringLength(150)]
        public string Link { get; set; }

        [Display(Name = "Link dự phòng")]
        [StringLength(500, ErrorMessage = "Nhập không quá 500 ký tự")]
        public string LinkDuPhong { get; set; }

        public virtual AssetType AssetType { get; set; }

        public virtual User User { get; set; }
    }
}
