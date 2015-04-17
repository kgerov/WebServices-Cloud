using BlogSystem.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using BlogSystem.Models;

namespace BlogSystem.Data
{
    public class BlogSystemData : IBlogSystemData
    {
        private DbContext context;
        private IDictionary<Type, object> repositories;

        public BlogSystemData(DbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IRepository<ApplicationUser> Users
        {
            get { return this.GetRepository<ApplicationUser>(); }
        }

        public IRepository<Post> Posts
        {
            get { return this.GetRepository<Post>(); }
        }

        public IRepository<Comment> Comments
        {
            get { return this.GetRepository<Comment>(); }
        }

        public IRepository<Tag> Tags
        {
            get { return this.GetRepository<Tag>(); }
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
