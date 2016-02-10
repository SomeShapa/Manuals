namespace Manuals.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class RatingComment
    {
        public int CommentId { get; set; }

        [Required]
        [StringLength(128)]
        public string UserId { get; set; }

        public int Id { get; set; }

        public bool? Liked { get; set; }

        public virtual Comment Comment { get; set; }
    }
}
