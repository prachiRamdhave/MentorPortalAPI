using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDFAPI.Models
{
    public class Scheduling
    {
        public int StudId { get; set; }
        public int productId { get; set; }
        public string CDF_id { get; set; }
        public int Shadow_id { get; set; }
        public int SesId { get; set; }
        public string pro_name { get; set; }
        public string shadowacceptance { get; set; }
        public string ConductingCDF { get; set; }
        public string ShadowCDF { get; set; }
        public string SesDate { get; set; }
        public DateTime SessionDate { get; set; }
        public string SesTime { get; set; }
        public DateTime SesDateTime { get; set; }
        public string SesStatus { get; set; }
        public string ShadowMob { get; set; }
        public string CDFMOb { get; set; }
        public string CdfAcceptance { get; set; }
        public string SesAddress { get; set; }
        public int SesCityId { get; set; }
        public string SesCity { get; set; }
        public string CandName { get; set; }
        public string CandGender { get; set; }
        public string CandEmail { get; set; }
        public string CandContact { get; set; }
        public string LeadType { get; set; }


    }
    public class RequestOTP
    {
        public int StudentID { get; set; }
        public string CDFName { get; set; }

    }
    public class AcceptRej
    {
        public string CDFResponse { get; set; }
        public int CDFId { get; set; }
        public int StudentId { get; set; }
    }

    public class AcceptRejShadow
    {
        public string ShadowCDFResponse { get; set; }
        public int ShadowCDFId { get; set; }
        public int StudentId { get; set; }
    }
    public class SessionStatus
    {
        public int OTP { get; set; }
        public int CDFId { get; set; }
        public int StudentId { get; set; }
    }
}