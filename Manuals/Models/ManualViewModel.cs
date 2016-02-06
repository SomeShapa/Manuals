using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Manuals.Models
{
    public class ManualViewModel
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public DateTime DateAdded { get; set; }

        public int CategoryId { get; set; }

        public string ImageLink { get; set; }

        public string VideoLink { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }

        public int Rating { get; set; }

        public virtual CategoryViewModel Category { get; set; }

        public virtual UserProfileViewModel UserProfile { get; set; }

        public virtual ICollection<TagViewModel> Tags { get; set; }

        public virtual ICollection<CommentViewModel> Comments { get; set; }


    }
}