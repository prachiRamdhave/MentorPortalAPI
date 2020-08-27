
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
    public class UserLoginController : ApiController
    {
        LoginRepository lr = new LoginRepository();

       
        public Output Post([FromBody]UserLogin ul)
        {
            if (ModelState.IsValid)
            {
                return lr.PostUserLogin(ul);
            }
            else
            {

                Output op = new Output();
                op.flag = false;
                op.uid = 0;
                op.email = null;
                op.msg = "Invalid Input"; // "Invalid Input";  BadRequest(ModelState);
                return op;
            }
        }

        [HttpGet]
        [Route("api/UserLogin/GetUserLoginNew")]
        
        public UserLoginNew GetUserLogin(string userName, string password)
        {
            
           return lr.GetUserLoginNew(userName, password);
        }
    }
}
