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
    public class KYTestController : ApiController
    {
        KYTestRepository KY = new KYTestRepository();

        [Route("api/KYTest/KYQuestions")]
        public List<KYQuestions> GetKYQuestions()
        {
            return KY.GetKYQuestion();
        }
        // GET: api/KYTest/langid
        [Route("api/KYTest/KYQuestionsBylang")]
        public List<KYQuestions> GetKYQuestionsBylang(int id)
        {
            return KY.GetKYQuestionByLang(id);
        }
        [Route("api/KYTest/KYQuestionsByUserID")]
        public List<KYQuestionsByUser> GetKYQuestionsByUserID(int Uid, int langid)
        {
            return KY.GetKYQuestionsByUserID(Uid, langid);
        }
        // POST: api/KYTest/uId
        [Route("api/KYTest/SubmitKYTest")]
        public Boolean Post(int uid, [FromBody] KYAnswers[] kyAnswers)
        {
            return KY.SubmitKYTest(uid, kyAnswers);
        }
        [Route("api/KYTest/GenerateKYTestFactor")]
        public Boolean generateKYTestFactor(int c_id, int batid, int testid)
        {
            return KY.generateKYTestFactor(c_id, batid, testid);
        }
        [Route("api/KYTest/AllTestCompleteKYAndPD")]
        public Boolean AllTestCompleteKYAndPD([FromBody] KyAndPD KP)
        {
            return KY.AllTestCompleteKYAndPD(KP);
        }

    }
}
