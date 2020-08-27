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

namespace CDFAPI.Controllers
{
    [Authorize]
    [EnableCorsAttribute("*", "*", "*")]
    public class BankDetailsUpdateController : ApiController
    {
        CDFRepository cr = new CDFRepository();
        public bool put(int id, [FromBody]BankDtl cdfBank)
        {
            return cr.PutCDFBankDetailUpdate(id, cdfBank);
        }

        [HttpGet]
        [Route("api/BankDetailsUpdate/GetBankDetails")]

        public BankDtl GetBankDtl(int UserID)
        {
            return cr.GetBankDetails(UserID);
        }
    }
}
