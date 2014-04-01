using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserEditor.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Status { get; set; }

        public string Pages { get; set; }

        public bool IsAdmin { get; set; }

        public string[] PagesArray 
        {
            get
            {
                return string.IsNullOrEmpty(this.Pages) ? null : this.Pages.Split(';');
            }
            set
            {
                this.Pages = value == null ? null : string.Join(";", value);
            }
        }
    }
}
