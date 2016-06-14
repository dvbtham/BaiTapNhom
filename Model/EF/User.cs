namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            Posts = new HashSet<Post>();
            SourceCodes = new HashSet<SourceCode>();
        }

        [Display(Name = "ID")]
        public long UserID { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Vui lòng nhập Email")]
        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50, ErrorMessage = "Mật khẩu ít nhất phải  6 ký tự", MinimumLength = 6)]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }


        [StringLength(50)]
        [Required(ErrorMessage = "Vui lòng nhập họ tên")]
        [Display(Name = "Họ tên")]
        public string Name { get; set; }

        [Display(Name = "Giới thiệu bản thân")]
        public string AboutMe { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [Display(Name = "Ngày đăng ký")]
        public DateTime? CreatedDate { get; set; }

        [Display(Name = "Quyền")]
        public int? RoleID { get; set; }

        [Display(Name = "Xác nhận")]
        public bool? ConfirmedByEmail { get; set; }

        [Display(Name = "Trạng thái")]
        public bool Status { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn ảnh đại diện")]
        [Display(Name = "Ảnh đại diện")]
        public string Avatar { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Post> Posts { get; set; }

        public virtual Role Role { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SocialNetwork> SocialNetworks { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SourceCode> SourceCodes { get; set; }
    }
}
