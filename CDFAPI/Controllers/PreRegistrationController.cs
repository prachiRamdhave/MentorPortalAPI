using CDFAPI.Models;
using CDFAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using static CDFAPI.Models.PreRegistration;

namespace CDFAPI.Controllers
{
    [Authorize]
    [EnableCorsAttribute("*", "*", "*")]
    public class PreRegistrationController : ApiController
    {
        PreRegistrationRepository PR = new PreRegistrationRepository();
        [HttpPost]
        [Route("api/PreRegistration/PostFirstRegistration")]
        public int PostReg1([FromBody]PreRegistration1 Pre)
        {
            return PR.PostPreReg1(Pre);
        }
        [HttpGet]
        [Route("api/PreRegistration/VerifyEmailID")]
        public string GetVEmail(string EmailId)
        {
            return PR.GetVerifyEmail(EmailId);
        }
        [HttpPut]
        [Route("api/PreRegistration/PostSecondRegistration")]
        public bool PostReg2([FromBody] PreRegistration2 Pre2)
        {
            return PR.PostPreReg2(Pre2);
        }
        [HttpGet]
        [Route("api/PreRegistration/GetExecutiveDetails")]
        public ExecutiveDtl GetExeDtl(string EmailId)
        {
            return PR.GetExeDetail(EmailId);
        }

        //CDF Test
        [HttpGet]
        [Route("api/PreRegistration/PersonalityQuestions")]
        public List<PDQuestions> GetPersonalityQuestions()
        {
            return PR.GetPDQuestion();
        }

    }
}
