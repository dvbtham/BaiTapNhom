namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Picture")]
    public partial class Picture
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name="ID")]
        public long PictureID { get; set; }

        [StringLength(500)]
        [Display(Name = "Đường dẫn")]
        public string Path { get; set; }

        [StringLength(500)]
        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        [StringLength(250)]
        [Display(Name = "Tiêu đề")]
        public string PictureName { get; set; }

        [Display(Name = "Hình ảnh cho bài viết")]
        public long? PostID { get; set; }

        [Display(Name = "Hiển thị cho tài khoản")]
        public long? UserID { get; set; }

        public virtual Post Post { get; set; }
    }
}
