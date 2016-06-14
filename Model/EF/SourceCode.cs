namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SourceCode")]
    public partial class SourceCode
    {
        public long SourceCodeID { get; set; }

        [StringLength(250)]
        public string Title { get; set; }

        [StringLength(550)]
        public string Details { get; set; }

        [StringLength(550)]
        public string Link { get; set; }

        public long? UserID { get; set; }

        public virtual User User { get; set; }
    }
}
