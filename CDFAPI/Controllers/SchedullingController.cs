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
    public class SchedullingController : ApiController
    {
        SchedulingRepository SR = new SchedulingRepository();
        [Route("api/Scheduling/SessionSchedule")]
        public IEnumerable<Scheduling> GetSessionSchedule(int CDF_ID)//,DateTime FromDate,DateTime ToDate
        {
            return SR.GetSessionSchedule(CDF_ID);//FromDate,  ToDate
        }
        [Route("api/Scheduling/SessionAcceptReject")]
        [HttpPut]
        public bool UpdateSession([FromBody]AcceptRej AR)
        {
            return SR.UpdateSessionAcceptReject(AR);
        }

        [Route("api/Scheduling/ShadowSessionAcceptReject")]
        [HttpPut]
        public bool UpdateShadowSession([FromBody]AcceptRejShadow SAR)
        {
            return SR.UpdateShadowSessionAcceptReject(SAR);
        }

        [Route("api/Scheduling/RequestOTPForSessionCompletion")]
        public bool RequestOTP([FromBody]RequestOTP ROTP)
        {
            return SR.RequestOTPForSessionComp(ROTP);
        }

        [Route("api/Scheduling/SubmitOTPForSessionCompletion")]
        public bool UpdateSessionStatus(SessionStatus SS)
        {
            return SR.UpdateSessionStatus(SS);
        }
    }
}
