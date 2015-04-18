using System;
using System.Collections.Generic;
using System.Data.Entity;
using MusicSystem.Data.Repositories;
using MusicSystem.Models;

namespace MusicSystem.Data
{
    public class MusicSystemData : IMusicSystemData
    {
        private DbContext context;
        private IDictionary<Type, object> repositories;

        public MusicSystemData(DbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IRepository<ApplicationUser> Users
        {
            get { return this.GetRepository<ApplicationUser>(); }
        }

        public IRepository<Artist> Artists
        {
            get { return this.GetRepository<Artist>(); }
        }

        public IRepository<Album> Albums
        {
            get { return this.GetRepository<Album>(); }
        }

        public IRepository<Song> Songs
        {
            get { return this.GetRepository<Song>(); }
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            var type = typeof(T);
            if (!this.repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepository<T>);

                this.repositories.Add(typeof(T), Activator.CreateInstance(repositoryType, this.context));
            }

            return (IRepository<T>)this.repositories[type];
        }
    }
}
