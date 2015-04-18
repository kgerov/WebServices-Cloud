namespace MusicSystem.Services.Controllers
{
    using System.Web.Http;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using MusicSystem.Data;
    using MusicSystem.Models;

    public class BaseController : ApiController
    {
        private IMusicSystemData data;

        public BaseController()
        {
            var context = new ApplicationDbContext();
            this.data = new MusicSystemData(context);
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
        }

        public IMusicSystemData Data
        {
            get { return this.data; }
        }

        protected UserManager<ApplicationUser> UserManager { get; set; }
    }
}