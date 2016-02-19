using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Manuals.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string Email { get; set; }

        public int Theme { get; set; }

        public string AvatarSrc { get; set; }

        public int Language { get; set; }

        public DateTime? BirthDate { get; set; }

        public string Description { get; set; }
    }
}