using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDFAPI.Models
{
    public class CommonDetails
    {
        public class FieldOfWork
        {
           // public int value { get; set; }
            public string fieldOfWorkList { get; set; }
        }
        public class yearOfExp
        {
            public int value { get; set; }
            public int YearOfExperience { get; set; }
        }
        public class IndustrySect
        {
          //  public int IndSecValue { get; set; }
            public string IndustrySector { get; set; }
        }
    }
}