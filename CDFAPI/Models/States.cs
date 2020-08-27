using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDFAPI.Models
{
    public class States
    {
        public int stateId { get; set; }
        public string stateName { get; set; }
    }
    public class Country
    {
        public int countId { get; set; }
        public string countName { get; set; }
    }
    public class CountryCode
    {
        public string CntryCode { get; set; }
    }

}