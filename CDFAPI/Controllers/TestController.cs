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
    public class TestController : ApiController
    {
        TestRepository TR = new TestRepository();
        [Route("api/Test/Assessment")]
        public List<Assesment> GetAssessment(int langID)
        {
            return TR.GetAssessment(langID);
        }

        [Route("api/Test/Separate_Pesonality_TestStatus")]
        public List<SeparateTestStatus> GetSeparate_Pesonality_TestStatus(int langID)
        {
            return TR.GetSeparate_Pesonality_TestStatus(langID);
        }
    }
}
