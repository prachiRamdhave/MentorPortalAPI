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
    public class CDFLeadsController : ApiController
    {
        CDFLeadsRepository CR = new CDFLeadsRepository();
        [Route("api/CDFLeads/LeadsByCDF")]
        public IEnumerable<Leads> GetLeadsByCDF(int CDF_ID)
        {
            return CR.GetLeadsByCDF(CDF_ID);
        }
    }
}
