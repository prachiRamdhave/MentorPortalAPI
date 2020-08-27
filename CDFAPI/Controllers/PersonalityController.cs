using CDFAPI.Models;
using CDFAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Cors;
using System.Web.Http;

namespace CDFAPI.Controllers
{
    [Authorize]
    [EnableCorsAttribute("*", "*", "*")]
    public class PersonalityController : ApiController
    {
       
        PersonalityRepository PR = new PersonalityRepository();
        [Route("api/Personality/PersonalityQuestions")]
        public List<PDQuestions> GetPersonalityQuestions()
        {
            return PR.GetPDQuestion();
        }

        // GET: api/Personality/langid
        [Route("api/Personality/PersonalityQuestionsBylang")]
        public List<PDQuestions> GetPersonalityQuestionsBylang(int id)
        {
            return PR.GetPDQuestionByLang(id);
        }
        [Route("api/Personality/UserCount")]
        public string GetUserCount(int Uid)
        {
            return PR.GetUserCount(Uid);
        }

        [Route("api/Personality/ProductMasterCount")]
        public bool GetProductMasterCount(int Uid)   //Using ProductID=7
        {
            return PR.GetProductMasterCount(Uid);
        }
        [Route("api/Personality/ProductMasterCompleteCount")]
        public string GetProductMasterCompleteCount(int Uid)   //Using ProductID=7
        {
            return PR.GetProductMasterCompleteCount(Uid);
        }
        // POST: api/Personality/uId
        [Route("api/Personality/PersonalityAnswers")]
        public Boolean Post(int id, [FromBody] PDAnswers[] pdAnswers)
        {
            return PR.SubmitPDTest(id, pdAnswers);
        }
        [Route("api/Personality/PersonalityQuestionsByUserID")]
        public List<PDQuestionsByUser> GetPersonalityQuestionsByUserID(int Uid, int langid)
        {
            return PR.GetPDQuestionByUserID(Uid, langid);
        }
        [Route("api/Personality/UserTestStatus")]
        public List<TestStatus> GetTestStatus(int uid, int langid)
        {
            return PR.GetTestStatus(uid, langid);
        }
    }
}
