namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Video")]
    public partial class Video
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long VideoID { get; set; }

        [StringLength(500)]
        public string Link { get; set; }
        [StringLength(550)]
        public string Iframe { get; set; }

        [StringLength(250)]
        public string VideoName { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        public long? PostID { get; set; }

        public virtual Post Post { get; set; }
    }
}
