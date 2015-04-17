using System.Data.Entity;
using System.Web.Http;
using BlogSystem.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BlogSystem.Services.Controllers
{
    using System.Web.Mvc;
    using BlogSystem.Data;

    public abstract class BaseApiController : ApiController
    {
        private IBlogSystemData data;

        public BaseApiController()
        {
            var context = new ApplicationDbContext();
            this.data = new BlogSystemData(context);
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
        }

        protected IBlogSystemData Data
        {
            get { return this.data; }
        }

        protected UserManager<ApplicationUser> UserManager { get; set; }
    }
}