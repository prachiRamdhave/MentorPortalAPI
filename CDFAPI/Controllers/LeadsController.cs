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
    public class LeadsController : ApiController
    {
        LeadsTicketsRepository lt = new LeadsTicketsRepository();
        public IEnumerable<Leads> GetLeads(string referredByEmail)
        {
            return lt.GetCDFLeads(referredByEmail);
        }

        public Leads GetMoreLeads(string referredByEmail,string id)
        {
            return lt.GetCDFMoreLeads(referredByEmail, id);
        }

        [Route("api/Leads/CreateLead")]
        public string PostCreateLead([FromBody] Leads Leads)
        {
            return lt.CreateLead(Leads);
        }
    }
}
