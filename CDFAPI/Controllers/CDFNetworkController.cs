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
    public class CDFNetworkController : ApiController
    {
        CDFRepository CR = new CDFRepository();
        public IEnumerable<CDFNetwork> GetLeads()
        {
            return CR.GetCDFNetwork();
        }
    }
}
