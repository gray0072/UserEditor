using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserEditor.Models.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class UserDto
    {
        public int Id { get; set; }

        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }

        public string Status { get; set; }

        public string[] Pages { get; set; }

        public bool IsAdmin { get; set; }


    }
}