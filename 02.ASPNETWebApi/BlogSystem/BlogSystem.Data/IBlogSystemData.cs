using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogSystem.Data.Repositories;
using BlogSystem.Models;

namespace BlogSystem.Data
{
    public interface IBlogSystemData
    {
        IRepository<Post> Posts { get; }
        IRepository<Comment> Comments { get; }
        IRepository<Tag> Tags { get; }

        int SaveChanges();
    }
}
