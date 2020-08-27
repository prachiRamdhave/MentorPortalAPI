using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CDFAPI.Models
{
    public class PreRegistration
    {
        public class PreRegistration1
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string ContactNo { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
         
        }
        public class PreRegistration2
        {
            public string Gender { get; set; }
            [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

            public DateTime Dob { get; set; }
            public int CountryId { get; set; }
            public int StateId { get; set; }
            public int CityId { get; set; }
            public string StateName { get; set; }
            public string CityName { get; set; }
            public string Address { get; set; }
            public int Uid { get; set; }
            public string PinCode { get; set; }
            public string Qualification { get; set; }
            public string WhyThisOpp { get; set; }
            public string designation { get; set; }
            public string maritalstatus { get; set; }
            public string spouseName { get; set; }
            public string childrenAge { get; set; }
            public string fieldOfWork { get; set; }
            public string modeOfWork { get; set; }
            public string industrySector { get; set; }
            public string aboutSelf { get; set; }
            public int RelationshipManager { get; set; }
        }
        public class ExecutiveDtl
        {
            public int exeId { get; set; }
            public string ExeName { get; set; }
            public string ExeEmailId { get; set; }
        }
    }
}