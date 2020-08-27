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
    public class ForgotPasswordController : ApiController
    {
        ForgotPasswordRepository fp = new ForgotPasswordRepository();
        //Post api/ForgotPassword sent email and sms ..
        public int Post([FromBody]ForgotPassword userEmail)
        {
            return fp.ForgotPassword(userEmail);
        }
    }
}
