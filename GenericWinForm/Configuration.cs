namespace App
{
    using App.WinForm.Entities.Security;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<TestModelContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "GenericWinAppTests";
        }

        protected override void Seed(TestModelContext context)
        {
           
            context.Roles.AddOrUpdate(
                 r => r.Id
              ,
              new Role { Id = 1, Name = "Root",Hidden= true },
              new Role { Id = 2, Name = "Admin" },
              new Role { Id = 3, Name = "User" },
              new Role { Id = 4, Name = "Project Management system" }
            );

        }
    }
}
