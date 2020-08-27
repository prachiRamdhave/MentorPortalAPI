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
    public class CareerToolsController : ApiController
    {
        CareerToolsRepository ct = new CareerToolsRepository();
        [Route("api/CareerTools/Combined")]
        public CareerToolsModel GetCombined(string ability1, string ability2, string ability3, string interest1, string interest2, string Personality1, string Personality2, string Personality3)
        {
            return ct.GetAllFilters(ability1, ability2, ability3, interest1, interest2, Personality1, Personality2, Personality3);
        }

        [Route("api/CareerTools/CareerByRAPD")]
        public IEnumerable<CareerToolsRAPD> GetCareerRAPD(string Rscore, string Ascore, string Pscore, string Dscore)
        {  //, string OccupationalCategory, string careerCategory
            return ct.GetCareerByRAPD(Rscore, Ascore, Pscore, Dscore);
        }

        [Route("api/CareerTools/Occupationalcategory")]
        public IEnumerable<CareerTools> GetOccupationalcategory()
        {
            return ct.GetOccupationalCategory();
        }

        [Route("api/CareerTools/Careercategory")]
        public IEnumerable<CareerTools> GetCareercategory(string OccupationalCategory)
        {
            return ct.GetCareerCategory(OccupationalCategory);
        }

        [Route("api/CareerTools/CareerDetails")]
        public CareerDetails GetAllCareerDetails(int id)
        {
            return ct.GetCareerDetails(id);
        }

        [Route("api/CareerTools/LineOfEducationToCareer")]
        public IEnumerable<LineOfEductToCareer> GetLineOfEduct(int id)
        {
            return ct.GetlineOfEductCareer(id);
        }


    }
}
