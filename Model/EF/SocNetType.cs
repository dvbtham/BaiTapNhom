namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SocNetType")]
    public partial class SocNetType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SocNetType()
        {
            SocialNetworks = new HashSet<SocialNetwork>();
        }

        [Key]
        public int SNTypeID { get; set; }

        [StringLength(50)]
        public string SNTypeName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SocialNetwork> SocialNetworks { get; set; }
    }
}
