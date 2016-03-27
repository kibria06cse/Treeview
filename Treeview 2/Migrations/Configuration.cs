namespace Treeview_2.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Treeview_2.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Treeview_2.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Treeview_2.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            foreach (Enums item in Enum.GetValues(typeof(Enums)))
            {
                context.TreeHierarchys.AddOrUpdate(new TreeHierarchy()
                {
                    Id= (int)item,
                    Title = item.ToString()
                }
                );
            }
            context.SaveChanges();

        }
    }
}
