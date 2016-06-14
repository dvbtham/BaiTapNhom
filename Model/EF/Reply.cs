namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Reply")]
    public partial class Reply
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ReplyID { get; set; }

        public string Content { get; set; }

        public long? CommentID { get; set; }

        public DateTime? RepliedDate { get; set; }

        public virtual Comment Comment { get; set; }
    }
}
