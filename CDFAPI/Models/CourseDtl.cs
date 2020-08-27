using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDFAPI.Models
{
    public class CourseDtl
    {
        //public int[]  co_id { get; set; }
        //public string[] co_text { get; set; }
        public string CourseName { get; set; }
        public string WhatThisCourseAllAbout { get; set; }
        public string JobMarket { get; set; }
        public string IndiaDemand { get; set; }
        public string OverseasDemand { get; set; }
        public string CourseCategory { get; set; }
        public string StreamIn { get; set; }
        public string Physics { get; set; }
        public string Chemistry { get; set; }
        public string Mathematics { get; set; }
        public string Biology { get; set; }
        public string OtherSubjects { get; set; }
     //   public string course13 { get; set; }
      //  public string listOfCareer { get; set; }
    }
    public class careerList
    {
        public int co_id { get; set; }
        public string co_text { get; set; }
    }
    public class InstZone
    {
        public string zone { get; set; }

    }
}