using System.Web.Http;
using BugTracker.Data;
using BugTracker.Data.UnitOfWork;

namespace BugTracker.RestServices.Controllers
{
    public abstract class BaseApiController : ApiController
    {
        private readonly IBugTrackerData data;

        public BaseApiController() 
            : this(new BugTrackerData())
        {
            
        }
        public BaseApiController(IBugTrackerData data)
        {
            this.data = data;
        }

        public IBugTrackerData Data 
        {
            get { return this.data; }
        }
    }
}
