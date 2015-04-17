namespace BlogSystem.Data
{
    using BlogSystem.Data.Migrations;
    using System.Data.Entity;
    using BlogSystem.Models;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("BlogSystemConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer<ApplicationDbContext>(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        //public IDbSet<ApplicationUser> Users { get; set; }
        public IDbSet<Post> Posts { get; set; }
        public IDbSet<Comment> Comments { get; set; }
        public IDbSet<Tag> Tags { get; set; }
    }
}
