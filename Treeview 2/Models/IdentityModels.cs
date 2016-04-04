using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Treeview_2.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        public DbSet<Treeview> Treeviews { get; set; }
        public DbSet<TreeHierarchy> TreeHierarchys { get; set; }
        public DbSet<Division> Divisions { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Base> Bases { get; set; }

        public DbSet<Thana> Thanas { get; set; }

        public DbSet<Union> Unions { get; set; }

        public DbSet<Village> Villages { get; set; }

        public DbSet<School> Schools { get; set; }

        public DbSet<Student> Students { get; set; }
        public DbSet<SchoolClass> SchoolClasses { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Result> Results { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Result>().HasRequired(c => c.Class).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Result>().HasRequired(c => c.School).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Result>().HasRequired(c => c.Subject).WithMany().WillCascadeOnDelete(false);
            base.OnModelCreating(modelBuilder);
        }

    }
}