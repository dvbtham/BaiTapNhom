namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Asset
    {
        public long AssetID { get; set; }

        [StringLength(150)]
        public string Title { get; set; }

        public int AssetTypeID { get; set; }

        public string Content { get; set; }
        public string Image { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EditedDate { get; set; }

        [StringLength(500, ErrorMessage = "Nhập không quá 500 ký tự")]
        public string ShortContent { get; set; }

        public DateTime? PostedDate { get; set; }
        public long? Views { get; set; }

        public long? UserID { get; set; }

        [StringLength(150)]
        public string Link { get; set; }

        [StringLength(500, ErrorMessage = "Nhập không quá 500 ký tự")]
        public string LinkDuPhong { get; set; }

        public virtual AssetType AssetType { get; set; }

        public virtual User User { get; set; }
    }
}
