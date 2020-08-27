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
    public class AbilityInterestRAPDFilterController : ApiController
    {
        CareerToolsRepository ct = new CareerToolsRepository();

        [Route("api/CareerTools/TotalCompa_Ability")]
        public IEnumerable<CareerToolsAbility> GetTotalCompa_Ability(string ability1, string ability2, string ability3)
        {
            return ct.GetTotalCompa_Ability(ability1, ability2, ability3);
        }

        [Route("api/CareerTools/TotalCompa_Interest")]
        public IEnumerable<CareerToolsInterest> GetTotalCompa_Interest(string Interest1, string Interest2)
        {
            return ct.GetTotalCompa_Interest(Interest1, Interest2);
        }

        [Route("api/CareerTools/TotalCompa_RAPD")]
        public IEnumerable<CareerToolsPersonality> GetTotalCompa_RAPD(string R, string A, string P, string D)
        {
            return ct.GetTotalCompa_RAPD(R, A, P, D);
        }

        [Route("api/CareerTools/TotalCompa_Combined")]  //Query For Combined OF ABILITY Interest And Personality
        public IEnumerable<CareerToolsCombined> GetTotalCompa_Combined(string ability1, string ability2, string ability3, string Interest1, string Interest2, string R, string A, string P, string D)
        {
            return ct.GetTotalCompa_Combined(ability1, ability2, ability3, Interest1, Interest2, R, A, P, D);
        }

        [Route("api/CareerTools/PartiallyAbility")]
        public IEnumerable<CareerToolsAbility> GetPartiallyAbility(string ability1, string ability2, string ability3)
        {
            return ct.GetPartiallyAbility(ability1, ability2, ability3);
        }

        [Route("api/CareerTools/Partially_Interest")]
        public IEnumerable<CareerToolsInterest> GetPartiallyInterest(string Interest1, string Interest2)
        {
            return ct.GetPartiallyInterest(Interest1, Interest2);
        }

        [Route("api/CareerTools/PartiallyRAPD")]
        public IEnumerable<CareerToolsPersonality> GetPartiallyRAPD(string R, string A, string P, string D)
        {
            return ct.GetPartiallyRAPD(R, A, P, D);
        }

        [Route("api/CareerTools/PartiallyCombo_Abi_Int_RAPD")]  //Query For Combined OF ABILITY Interest And Personality
        public IEnumerable<CareerToolsCombined> GetPartiallyCombo_Abi_Int_RAPD(string ability1, string ability2, string ability3, string Interest1, string Interest2, string R, string A, string P, string D)
        {
            return ct.GetPartiallyCombo_Abi_Int_RAPD(ability1, ability2, ability3, Interest1, Interest2, R, A, P, D);
        }


        [Route("api/CareerTools/TotalCompa_AbilityAndChart")]
        public CareerToolsModel GetTotalCompa_AbilityAndChart(string ability1, string ability2, string ability3)
        {
            return ct.GetTotalCompa_AbilityAndChart(ability1, ability2, ability3);
        }

        [Route("api/CareerTools/TotalCompa_InterestAndChart")]
        public CareerToolsModel GetTotalCompa_InterestAndChart(string Interest1, string Interest2)
        {
            return ct.GetTotalCompa_InterestAndChart(Interest1, Interest2);
        }

        [Route("api/CareerTools/TotalCompa_RAPDAndChart")]
        public CareerToolsModel GetTotalCompa_RAPDAndChart(string R, string A, string P, string D)
        {
            return ct.GetTotalCompa_RAPDAndChart(R, A, P, D);
        }

        [Route("api/CareerTools/TotalCompa_CombinedAndChart")]  //Query For Combined OF ABILITY Interest And Personality
        public CareerToolsModel GetTotalCompa_CombinedAndChart(string ability1, string ability2, string ability3, string Interest1, string Interest2, string R, string A, string P, string D)
        {
            return ct.GetTotalCompa_CombinedAndChart(ability1, ability2, ability3, Interest1, Interest2, R, A, P, D);
        }

        [Route("api/CareerTools/TotalCompa_AbilityInterestAndChart")]
        public CareerToolsModel GetTotalCompa_AbilityInterestAndChart(string ability1, string ability2, string ability3, string Interest1, string Interest2)
        {
            return ct.GetTotalCompa_AbilityInterestAndChart(ability1, ability2, ability3, Interest1, Interest2);
        }

        [Route("api/CareerTools/TotalCompa_AbilityRAPDAndChart")]
        public CareerToolsModel GetTotalCompa_AbilityRAPDAndChart(string ability1, string ability2, string ability3, string R, string A, string P, string D)
        {
            return ct.GetTotalCompa_AbilityRAPDAndChart(ability1, ability2, ability3, R, A, P, D);
        }

        [Route("api/CareerTools/TotalCompa_InterestRAPDAndChart")]
        public CareerToolsModel GetTotalCompa_InterestRAPDAndChart(string Interest1, string Interest2, string R, string A, string P, string D)
        {
            return ct.GetTotalCompa_InterestRAPDAndChart(Interest1, Interest2, R, A, P, D);
        }

    }
}
