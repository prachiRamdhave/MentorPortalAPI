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
    public class DdlAbilityInterestController : ApiController
    {
        CareerToolsRepository ct = new CareerToolsRepository();
        [Route("api/CareerTools/DDLAbility")]
        public List<DDLAbility> GetDDLAbility()
        {
            return ct.GetAbility_DDL();
        }

        [Route("api/CareerTools/DDLAbility2")]
        public List<DDLAbility> GetDDLAbility2(string Ability1)
        {
            return ct.GetAbility_DDL2(Ability1);
        }

        [Route("api/CareerTools/DDLAbility3")]
        public List<DDLAbility> GetDDLAbility3(string Ability1, string Ability2)
        {
            return ct.GetAbility_DDL3(Ability1, Ability2);
        }

        [Route("api/CareerTools/DDLInterest")]
        public List<DDLInterest> GetDDLInterest()
        {
            return ct.GetInterest_DDL();
        }

        [Route("api/CareerTools/DDLInterest2")]
        public List<DDLInterest> GetDDLInterest2(int factorno )
        {
            return ct.GetInterest_DDL2(factorno);
        }

    }
}
