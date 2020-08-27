using CDFAPI.Models;
using CDFAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CDFAPI.Controllers
{
    [Authorize]
    [EnableCorsAttribute("*", "*", "*")]
    public class StatsController : ApiController
    {
        StatsRepository sr = new StatsRepository();

        public Stats Get(string dheyaEmail, int uId)
        {
            if (ModelState.IsValid)
            {
                return sr.GetStats(dheyaEmail,uId);
            }
            else
            {
                return null;
            }
        }
    }
}
