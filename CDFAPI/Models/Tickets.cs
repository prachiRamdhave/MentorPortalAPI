using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDFAPI.Models
{
    public class Tickets
    {
        public string id { get; set; }
        public string state { get; set; }
        public string priority { get; set; }
        public string status { get; set; }
        public string name { get; set; }
        public string issue { get; set; }
        public DateTime createdDate { get; set; }
        public string description { get; set; }
        public string solution { get; set; }
        public Nullable<DateTime>  modifiedDate { get; set; }
        public string ticketCreatedBy { get; set; }
        public string type { get; set; }
    }
}