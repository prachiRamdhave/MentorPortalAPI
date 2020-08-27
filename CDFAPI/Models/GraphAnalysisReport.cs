using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CDFAPI.Models
{
    public class GraphAnalysisReport
    {
        [Display(Name = "StudentId")]
        public int StudId { get; set; }
        [Display(Name = "StudentName")]
        public string StudName { get; set; }
        [Display(Name = "SessionId")]
        public int sesId { get; set; }
        [Display(Name = "SessionDate")]
        public DateTime SesDate { get; set; }
        [Display(Name = "SessionTime")]
        public string SesTime { get; set; }
        [Display(Name = "SessionAddress")]
        public string SesAdd { get; set; }
        [Display(Name = "CDFId")]
        public int CDFId { get; set; }
        [Display(Name = "CDFName")]
        public string CDFName { get; set; }
        [Display(Name = "ShadowCDFId")]
        public int ShadowCDFId { get; set; }
        [Display(Name = "ShadowCDFName")]
        public string ShadowCDFName { get; set; }
    }
}