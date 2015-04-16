using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogSystem.Data.Repositories;

namespace BlogSystem.Data
{
    public interface IBlogSystemData
    {
        //IRepository<User> Users { get; set; }

        int SaveChanges();
    }
}
