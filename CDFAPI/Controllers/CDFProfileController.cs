using CDFAPI.Models;
using CDFAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using static CDFAPI.Models.CDFProfile;
using static CDFAPI.Models.CommonDetails;

namespace CDFAPI.Controllers
{
    [Authorize]
    [EnableCorsAttribute("*", "*", "*")]
    public class CDFProfileController : ApiController
    {
        CDFRepository cr = new CDFRepository();
        public CDFProfile GetDetails(int id)
        {
            return cr.GetAllCDFDetails(id);
        }

        public bool Put(int id, [FromBody]CDFProfile cdfProfile)
        {
            return cr.PutCDFDetailsUpdate(id, cdfProfile);
        }


        [HttpGet]
        [Route("api/CDFProfile/GetCDFLevelDetails")]
        public CDFLevelDetails GetList(int id)
        {
            return cr.GetCDFLevelDetails(id);

        }
        [HttpGet]
        [Route("api/CDFProfile/GetFieldOfWork")]
        public IEnumerable<FieldOfWork> GetFieldOfWork()
        {
            return cr.GetFieldOfWorkData();
        }
        [HttpGet]
        [Route("api/CDFProfile/GetYearOfExperience")]
        public IEnumerable<yearOfExp> GetYrOfExp()
        {
            return cr.GetYerOfExp();
        }
        [HttpGet]
        [Route("api/CDFProfile/IndustrySector")]
        public IEnumerable<IndustrySect> IndustrySec()
        {
            return cr.IndSector();
        }


        [Route("api/CDFProfile/SessionAcceptReject")]
        public bool UpdateSession(bool CDFResponse, int CDFId, int StudentId)
        {
            return cr.UpdateSessionAcceptReject(CDFResponse, CDFId, StudentId);
        }
    }
}
