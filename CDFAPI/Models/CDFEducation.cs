using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDFAPI.Models
{
    public class CDFEducation
    {
        //Education
        public string college { get; set; }
        public string degree { get; set; }
        public string description { get; set; }
        public string grade { get; set; }
        public int uId { get; set; }
        public int eduId { get; set; }
    }
}