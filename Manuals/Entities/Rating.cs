namespace Manuals.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Rating
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ManualId { get; set; }

        [Key]
        [Column(Order = 1)]
        public string UserId { get; set; }

        public bool? Liked { get; set; }

        public virtual Manual Manual { get; set; }
    }
}
