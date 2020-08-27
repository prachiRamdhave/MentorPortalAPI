using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDFAPI.Models
{
    public class CDFExperience
    {

        //Experience
        public int expId { get; set; }
        public string company { get; set; }
        public string position { get; set; }
        public DateTime? job_start_date { get; set; }
        public DateTime? job_end_date { get; set; }
        public string location { get; set; }
        public string position_discription { get; set; }
        public int uId { get; set; }
    }
}