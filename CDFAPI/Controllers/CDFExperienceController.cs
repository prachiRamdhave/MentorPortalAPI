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
    public class CDFExperienceController : ApiController
    {
        CDFRepository cr = new CDFRepository();
        public IEnumerable<CDFExperience> GetExperience(int id)
        {
            return cr.GetCDFExperience(id);
        }

        [Route("api/CDFExperience/MoreDetails")]
        public CDFExperience GetExperienceMoreDetails(int id, int expId)
        {
            return cr.GetCDFExperienceMoreDetails(id, expId);
        }

        [Route("api/CDFExperience/Add")]
        public string Posthello([FromBody] CDFExperience CDFExperience)
        {
            return cr.PostCDFExperienceAdd(CDFExperience);
        }

        [Route("api/CDFExperience/Update")]
        public string PutEduUpdate(int id,int expId, [FromBody]CDFExperience CDFExperience)
        {
            return cr.PutCDFExperienceUpdate(id,expId, CDFExperience);
        }

        [Route("api/CDFExperience/Delete")]
        public string DeleteCDFEdu(int expId)
        {
            //int id, 
            return cr.DeleteCDFExperience(expId);
        }
    }
}
