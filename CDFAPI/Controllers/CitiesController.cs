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
    public class CitiesController : ApiController
    {
        CountryStateCityRepository cs = new CountryStateCityRepository();

        // GET: api/Cities
        public IEnumerable<Cities> GetCities()
        {
            return cs.GetAllCities();
        }
        // GET: api/Cities/stateid
        public IEnumerable<Cities> GetCities(int id)
        {
            return cs.GetAllCitiesByStateId(id);
        }
        // GET: api/Cities/cityName=Pune
        public IEnumerable<Cities> GetCitiesByName(string cityname)
        {
            return cs.GetAllCitiesByCityName(cityname);
        }
    }
}
