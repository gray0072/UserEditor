using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using UserEditor.Models;

namespace UserEditor
{
    public class UsersDB : DbContext
    {
        public UsersDB()
            : base("DefaultConnection")
        {
        }

        public DbSet<User> Users { get; set; }
    }
    
}
