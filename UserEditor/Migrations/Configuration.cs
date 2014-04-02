namespace UserEditor.Migrations
{
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;

    using UserEditor.Models.DB;

    internal sealed class Configuration : DbMigrationsConfiguration<UsersDB>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
            this.ContextKey = "UserEditor.UsersDB";
        }

        protected override void Seed(UsersDB context)
        {
            var users = new List<User>
                            {
                                new User
                                    {
                                        Id = 1,
                                        Firstname = "John",
                                        Lastname = "Smith",
                                        Status = "Married",
                                        PagesString = "Page1;Page2",
                                        IsAdmin = false
                                    },
                                new User
                                    {
                                        Id = 2,
                                        Firstname = "John",
                                        Lastname = "Doe",
                                        Status = "Married",
                                        PagesString = "Page1;Page3",
                                        IsAdmin = true
                                    },
                                new User
                                    {
                                        Id = 3,
                                        Firstname = "Mary",
                                        Lastname = "Foo",
                                        Status = "Single",
                                        PagesString = "Page2;Page3",
                                        IsAdmin = true
                                    },
                                new User
                                    {
                                        Id = 4,
                                        Firstname = "Janny",
                                        Lastname = "Smith",
                                        Status = "Divorced",
                                        PagesString = "Page3",
                                        IsAdmin = false
                                    }
                            };

            users.ForEach(user => context.Users.AddOrUpdate(user));
            context.SaveChanges();

        }
    }
}
