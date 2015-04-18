using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicSystem.Data.Repositories;
using MusicSystem.Models;

namespace MusicSystem.Data
{
    public interface IMusicSystemData
    {
        IRepository<ApplicationUser> Users { get; }
        IRepository<Artist> Artists { get; }
        IRepository<Album> Albums { get; }
        IRepository<Song> Songs { get; }
        int SaveChanges();
    }
}
