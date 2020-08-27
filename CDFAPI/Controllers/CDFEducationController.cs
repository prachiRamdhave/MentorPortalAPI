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
    public class CDFEducationController : ApiController
    {
        CDFRepository cr = new CDFRepository();
        public IEnumerable<CDFEducation> GetEducation(int id)
        {
            return cr.GetCDFEducation(id);
        }

        [Route("api/CDFEducation/MoreDetails")]
        public CDFEducation GetEducationMoreDetails(int id, int eduId)
        {
            return cr.GetCDFEducationMoreDetails(id, eduId);
        }

        [Route("api/CDFEducation/Add")]
        public string Post([FromBody] CDFEducation CDFEducation)
        {
            return cr.PostCDFEducationAdd(CDFEducation);
        }

        [Route("api/CDFEducation/Update")]
        public string PutEduUpdate(int id, int eduId, [FromBody]CDFEducation cdfeducation)
        {
            return cr.PutCDFEducationUpdate(id, eduId, cdfeducation);
        }

        [Route("api/CDFEducation/Delete")]
        public string DeleteCDFEdu(int id, int eduId)  //, CDFEducation CDFEducationDel
        {
            return cr.DeleteCDFEducation(id, eduId);  //, CDFEducationDel
        }
    }
}
