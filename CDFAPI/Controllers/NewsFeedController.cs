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
    public class NewsFeedController : ApiController
    {
        CDFRepository CR = new CDFRepository();
        [HttpGet]
        [Route("api/NewsFeed/GetNewsFeed")]
        public IEnumerable<NewsFeed> GetLeads()
        {
            return CR.GetNewsFeed();
        }

        [HttpGet]
        [Route("api/NewsFeed/GetMentorNewsFeedDetails")]
        public List<NewsFeedNew> GetNewsFeedNew()
        {
            return CR.GetMentorNewsFeedNew();
        }


    }
}
