using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDFAPI.Models
{
    public class Leads
    {
        public string id { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string contactNo { get; set; }
        public string email { get; set; }
        public DateTime createdDate { get; set; }
        public string city { get; set; }
        public string leadStatus { get; set; }
        public string description { get; set; }
        public string assignedExecutive { get; set; }
        public string ExecutiveNo { get; set; }
        public string leadCategory { get; set; }
        public string referedBy { get; set; }
        public string enquiredFor { get; set; }
        public string leadSource { get; set; }
        public string pincode { get; set; }
        public string schoolCollegeName { get; set; }

    }
}