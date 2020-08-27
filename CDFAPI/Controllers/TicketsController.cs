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
    public class TicketsController : ApiController
    {
        LeadsTicketsRepository lt = new LeadsTicketsRepository();

        public IEnumerable<Tickets> GetTickets(string fname,string lname,string dheyaEmail)
        {
           return lt.GetCDFTickets(fname, lname, dheyaEmail);
        }

        public Tickets GetMoreTickets(string fname, string lname, string dheyaEmail, string id)
        {
            return lt.GetCDFMoreTickets(fname, lname, dheyaEmail, id);
        }

        [Route("api/Tickets/CreateTicket")]
        public string PostCreateTicket([FromBody] Tickets Tickets)
        {
            return lt.CreateTicket(Tickets);
        }
    }
}
