using System;
using System.Data;
using System.Linq;

namespace CDFAPI.Models
{

    public class SessionReportModel
    {
        // public int Report_ID { get; set; }
        public int CDF_ID { get; set; }
        public int Session_ID { get; set; }
        //    public int ReportVersion { get; set; }
        public string ReportStatus { get; set; }
        //   public int ReportPreparedBy { get; set; }
        public string[] Stud_Strengths { get; set; }
        public string[] Stud_Personality_Traits { get; set; }
        public string AreaAdvantage { get; set; }
        public string Motivators { get; set; }
        public string GeneralAbilities { get; set; }
        public string LeadershipType { get; set; }
        public string WaySetGoals { get; set; }
        public string WayStudy { get; set; }
        public string WayCommunicate { get; set; }
        public string MentalAbilities { get; set; }
        public string Creativities { get; set; }
        public string PhysicalAbilities { get; set; }
        public string Skills { get; set; }
        public string CareerCategory { get; set; }
        public string OccupationalCategory { get; set; }
        public string Career { get; set; }
        //    public string Recommendations { get; set; }
        public string ReviewComment { get; set; }
    }


    public class CareerCategory
    {
        public string Category { get; set; }
    }
    public class OccupationalCategory
    {
        public string Occupation { get; set; }
    }

    public class Career
    {
        public int ca_Id { get; set; }
        public string Mode_Work { get; set; }
    }
    public class SessionReport
    {
        public int Report_ID { get; set; }
        public int CDF_ID { get; set; }
        public int Session_ID { get; set; }
        public int ReportVersion { get; set; }
        public string ReportStatus { get; set; }
        public int ReportPreparedBy { get; set; }
        public string Stud_Strengths { get; set; }
        public string Stud_Personality_Traits { get; set; }
        public string AreaAdvantage { get; set; }
        public string Motivators { get; set; }
        public string GeneralAbilities { get; set; }
        public string LeadershipType { get; set; }
        public string WaySetGoals { get; set; }
        public string WayStudy { get; set; }
        public string WayCommunicate { get; set; }
        public string MentalAbilities { get; set; }
        public string Creativities { get; set; }
        public string PhysicalAbilities { get; set; }
        public string Skills { get; set; }
        public string CareerCategory { get; set; }
        public string OccupationalCategory { get; set; }
        public string Career { get; set; }
        public string Recommendations { get; set; }
        public string ReviewedAndApprovedBy { get; set; }
        public string ReviewComment { get; set; }
    }
}