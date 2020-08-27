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
    public class StatesController : ApiController
    {
        CountryStateCityRepository cs = new CountryStateCityRepository();
        // GET: api/States
        public IEnumerable<States> GetStates()
        {
            return cs.GetAllStates();
        }
        // GET: api/States/countryid
        public IEnumerable<States> GetStates(int id)
        {
            return cs.GetAllStatesByCountryid(id);
        }
        [Route("api/States/GetAllCountry")]
        public IEnumerable<Country> GetCountry()
        {
            return cs.GetAllCountry();
        }
        [Route("api/States/CountryCode")]
        public CountryCode GetCountryCode(int countryId)
        {
            return cs.GetCntryCode(countryId);
        }

    }
}
