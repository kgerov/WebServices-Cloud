using Microsoft.AspNet.Identity.EntityFramework;
using MusicSystem.Models;
using System.Data.Entity;
using MusicSystem.Data.Migrations;

namespace MusicSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("MusicSystemConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public IDbSet<Artist> Artists { get; set; }

        public IDbSet<Song> Songs { get; set; }

        public IDbSet<Album> Albums { get; set; }
    }
}
