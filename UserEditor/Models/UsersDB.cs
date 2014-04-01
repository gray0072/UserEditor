using System.Data.Entity;

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
