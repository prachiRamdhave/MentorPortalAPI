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
    public class CheckOTPController : ApiController
    {
        ForgotPasswordRepository fp = new ForgotPasswordRepository();

        //post api //Check OTP is correct
        public Boolean Post(int id, [FromBody] CheckOTP checkOTP)
        {
            return fp.CheckOTP(id, checkOTP);
        }
    }
}
