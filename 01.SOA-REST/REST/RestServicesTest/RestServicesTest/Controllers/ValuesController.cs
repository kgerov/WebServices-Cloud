using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RestServicesTest.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public int Get()
        {
            return 5;
        }

        [Route("sum")]
        public IHttpActionResult Sum(int a, int b)
        {
            if (a == 0 || b == 0)
            {
                this.BadRequest("a and b can't be 0");
            }

            return this.Ok(a + b);
        }

        [Route("distance")]
        public IHttpActionResult Distance(int x1, int y1, int x2, int y2)
        {
            double powDeltaX = Math.Pow((x1 - x2), 2);
            double powDeltaY = Math.Pow((y1 - y2), 2);

            double dist = Math.Sqrt(powDeltaX + powDeltaY);

            return this.Ok(dist);
        }
    }
}
