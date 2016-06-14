namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Post")]
    public partial class Post
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Post()
        {
            Comments = new HashSet<Comment>();
            Pictures = new HashSet<Picture>();
            Videos = new HashSet<Video>();
            PostTags = new HashSet<PostTag>();
        }
        [Display(Name="ID")]
        [Key]
        public long PostID { get; set; }

        [StringLength(250)]
        [Required(ErrorMessage="Bạn chưa nhập tiêu đề")]
        [Display(Name = "Tiêu đề")]
        public string Title { get; set; }

        [Display(Name = "Người đăng")]
        public long? UserID { get; set; }

        [StringLength(500)]
        [Display(Name = "Tóm tắt")]
        [Required(ErrorMessage = "Bạn chưa nhập tóm tắt nội dung")]
        public string Content { get; set; }

        [Display(Name = "Nội dung")]
        [Required(ErrorMessage = "Bạn chưa nhập nội dung bài viết")]
        public string Detail { get; set; }

        [Display(Name = "Danh mục")]
        public int? CategoryID { get; set; }

        [Display(Name = "Trạng thái")]
        public bool? Status { get; set; }

        [Display(Name = "Ảnh đại diện")]
        public string ImageShowOnHome { get; set; }

        [Display(Name = "Ngày đăng")]
        public DateTime? PostedDate { get; set; }

        [Display(Name = "Lượt xem")]
        public int? Views { get; set; }

        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Picture> Pictures { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PostTag> PostTags { get; set; }

        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Video> Videos { get; set; }
    }
}
