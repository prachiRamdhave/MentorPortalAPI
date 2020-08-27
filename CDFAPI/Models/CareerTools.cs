using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDFAPI.Models
{
    public class CareerTools
    {
        public int caId { get; set; }
        public string CareerCategory { get; set; }
        public string OccupationalCategory { get; set; }
        public string Career { get; set; }

    }
    public class CareerToolsAbility
    {
        public string FutureRelavance { get; set; }
        public int caId { get; set; }
        public string CareerCategory { get; set; }
        public string OccupationalCategory { get; set; }
        public string Career { get; set; }
    }

    public class CareerToolsInterest
    {
        public string FutureRelavance { get; set; }
        public int caId { get; set; }
        public string CareerCategory { get; set; }
        public string OccupationalCategory { get; set; }
        public string Career { get; set; }
    }

    public class CareerToolsPersonality
    {
        public int caId { get; set; }
        public string CareerCategory { get; set; }
        public string OccupationalCategory { get; set; }
        public string Career { get; set; }
    }

    public class CareerToolsCombined
    {
        public string FutureRelavance { get; set; }
        public int caId { get; set; }
        public string CareerCategory { get; set; }
        public string OccupationalCategory { get; set; }
        public string Career { get; set; }
    }

    public class CareerToolsRAPD
    {
        public string FutureRelavance { get; set; }
        public string CareerCategory { get; set; }
        public string OccupationalCategory { get; set; }
        public string Career { get; set; }
        public int caId { get; set; }
    }

    public class CareerDetails
    {
        public int caId { get; set; }
        public string SectorYouWork { get; set; }
        public string IndustriesYouWork { get; set; }
        public string ProfessionalInterests { get; set; }
        public string CareerCategory { get; set; }
        public string CareerAllAbout { get; set; }
        public string JobYouDo { get; set; }
        public string CareerAdvancementOpportunities { get; set; }
     //   public string LineOfEducationToCareer { get; set; }
        //public string JobFocus1 { get; set; }
        //public string JobFocus2 { get; set; }
        //public string JobFocus3 { get; set; }
        //public string JobFocus4 { get; set; }
        //public string JobFocus5 { get; set; }
        //public string JobFocus6 { get; set; }
        //public string JobFocus7 { get; set; }
        //public string JobFocus8 { get; set; }
      //  public string ListOfCourses { get; set; }
        public string WorkingOfficeEnvironment { get; set; }
        public string MobilityTravel { get; set; }
        public string WorkingWithEquipment { get; set; }
        public string WorkingWithComputers { get; set; }
        public string HighRiskEnvironment { get; set; }
        public string LongHours { get; set; }
        public string WorkingAtNight { get; set; }
        public string WorkingWithHightech { get; set; }
        public string WorkingCloselyWithPeople { get; set; }
        public string SittingAtOnePlace { get; set; }
        public string ResultOriented { get; set; }
        public string ShortTermTimelineProjects { get; set; }
        public string SettingGoalsForYourselfAndTeam { get; set; }
        public string HighDecisionMakingNeeds { get; set; }
        public string HighProblemSolvingNeeds { get; set; }
        public string PeopleToPeopleInteraction1 { get; set; }
        public string WorkingInTeams { get; set; }
        public string LeadingTeam { get; set; }
        public string IndividualWorkingEnvironment { get; set; }
        public string RepetitiveJobsAndRoutine { get; set; }
        public string WorkingHigherQualityAccuracyRequirements { get; set; }
        public string DesigningProcesses { get; set; }
        public string WorkingWithProcesses { get; set; }
        public string WorkingWithEmails { get; set; }
        public string PeopleToPeopleInteraction2 { get; set; }
        public string VerbalCommunicationWithPeopleMasses { get; set; }
        public string WrittenCommunicationExpression { get; set; }
        public string NonVerbalCommunication { get; set; }
        public string NumericalAbility { get; set; }
        public string VerbalAbility { get; set; }
        public string WrittenComprehension { get; set; }
        public string WrittenExpression { get; set; }
        public string VerbalComprehension { get; set; }
        public string VerbalExpression { get; set; }
        public string LogicalThinking { get; set; }
        public string AnalyticalThinking { get; set; }
        public string ProblemSensitivityAndSolving { get; set; }
        public string SpaceRelations { get; set; }
        public string AbstractReasoning { get; set; }
        public string Visualization { get; set; }
        public string CreativeImaginationOriginality  { get; set; }
        public string MemoryRecall { get; set; }
        public string FingerHandSwiftness { get; set; }
        public string Hearing { get; set; }
        public string Vision { get; set; }
        public string NightVision { get; set; }
        public string StaminaFitness { get; set; }
        public string HandSteadiness { get; set; }
        public string GrossBodyCoordination { get; set; }
        public string FutureRelevance { get; set; }
        public string WorkOfficeEnvtIndoors { get; set; }
    }

    public class LineOfEductToCareer
    {
        public int co_id { get; set; }
        public string course1 { get; set; }
    }
    public class DDLAbility
    {
        public string ability { get; set; }
    }

    public class DDLInterest
    {
        public int factorNo { get; set; }
        public string factor { get; set; }
    }
    public class AbilityChart
    {
        public string CareerCategory { get; set; }
        public int CategoryCount { get; set; }
        public double Compatibility { get; set; }
    }
    public class InterestChart
    {
        public string CareerCategory { get; set; }
        public int CategoryCount { get; set; }
        public double Compatibility { get; set; }
    }
    public class RAPDChart
    {
        public string CareerCategory { get; set; }
        public int CategoryCount { get; set; }
        public double Compatibility { get; set; }
    }
    public class CombinedChart
    {
        public string CareerCategory { get; set; }
        public int CategoryCount { get; set; }
        public double Compatibility { get; set; }
    }
    public class CareerToolsAbility_RAPD
    {
        public string CareerCategory { get; set; }
        public string OccupationalCategory { get; set; }
        public string Career { get; set; }
        public int caId { get; set; }
        public string FutureRelavance { get; set; }
    }
    public class CareerToolsAbility_Interest
    {
        public string CareerCategory { get; set; }
        public string OccupationalCategory { get; set; }
        public string Career { get; set; }
        public int caId { get; set; }
        public string FutureRelavance { get; set; }
    }
    public class CareerToolsInterest_RAPD
    {
        public string CareerCategory { get; set; }
        public string OccupationalCategory { get; set; }
        public string Career { get; set; }
        public int caId { get; set; }
        public string FutureRelavance { get; set; }
    }
    public class Ability_RAPDChart
    {
        public string CareerCategory { get; set; }
        public int CategoryCount { get; set; }
        public double Compatibility { get; set; }
    }
    public class Ability_InterestChart
    {
        public string CareerCategory { get; set; }
        public int CategoryCount { get; set; }
        public double Compatibility { get; set; }
    }
    public class Interest_RAPDChart
    {
        public string CareerCategory { get; set; }
        public int CategoryCount { get; set; }
        public double Compatibility { get; set; }
    }

}