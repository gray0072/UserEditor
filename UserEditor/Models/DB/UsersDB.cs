using System.Data.Entity;
using UserEditor.Models.DB;

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
