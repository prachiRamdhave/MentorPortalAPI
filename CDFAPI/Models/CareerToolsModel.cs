using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDFAPI.Models
{
    public class CareerToolsModel
    {
        public List<CareerToolsAbility> lcta = new List<CareerToolsAbility>();
        public List<CareerToolsInterest> lcti = new List<CareerToolsInterest>();
        public List<CareerToolsPersonality> lctp = new List<CareerToolsPersonality>();
        public List<CareerToolsCombined> lctc = new List<CareerToolsCombined>();
        public List<CareerToolsAbility> lctap = new List<CareerToolsAbility>();
        public List<CareerToolsInterest> lctip = new List<CareerToolsInterest>();
        public List<CareerToolsPersonality> lctpp = new List<CareerToolsPersonality>();
        public List<CareerToolsCombined> lctcp = new List<CareerToolsCombined>();

        public List<CareerToolsAbility> AbilityTable = new List<CareerToolsAbility>();
        public List<AbilityChart> AbilityChart = new List<AbilityChart>();
        public List<CareerToolsInterest> InterestTable = new List<CareerToolsInterest>();
        public List<InterestChart> InterestChart = new List<InterestChart>();
        public List<CareerToolsRAPD> RAPDTable = new List<CareerToolsRAPD>();
        public List<RAPDChart> RAPDChart = new List<RAPDChart>();
        public List<CareerToolsCombined> CombinedTable = new List<CareerToolsCombined>();
        public List<CombinedChart> CombinedChart = new List<CombinedChart>();
        public List<CareerToolsAbility_RAPD> Ability_RAPDTable = new List<CareerToolsAbility_RAPD>();
        public List<CareerToolsAbility_Interest> Ability_InterestTable = new List<CareerToolsAbility_Interest>();
        public List<CareerToolsInterest_RAPD> Interest_RAPDTable = new List<CareerToolsInterest_RAPD>();
        public List<Ability_RAPDChart> Ability_RAPDChart = new List<Ability_RAPDChart>();
        public List<Ability_InterestChart> Ability_InterestChart = new List<Ability_InterestChart>();
        public List<Interest_RAPDChart> Interest_RAPDChart = new List<Interest_RAPDChart>();


    }
}