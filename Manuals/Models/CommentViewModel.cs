using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Manuals.Models
{
    public class CommentViewModel
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string Description { get; set; }

        public int ManualId { get; set; }

        public int Rating { get; set; }
    }
}