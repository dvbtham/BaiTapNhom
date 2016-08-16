namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SocialNetwork")]
    public partial class SocialNetwork
    {
        [Key]
        public int SocNetID { get; set; }

        [StringLength(250)]
        public string Link { get; set; }

        public bool? Status { get; set; }

        public long? UserID { get; set; }

        public int SNTypeID { get; set; }

        public virtual SocNetType SocNetType { get; set; }

        public virtual User User { get; set; }
    }
}
