using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CDFAPI.Models
{
    public class UserLogin
    {
        //(ErrorMessage = "email is Must")
        [Required]
        public string email { get; set; }

        //(ErrorMessage = "Password is Must")
        [Required]
        public string password { get; set; }

        
    }
    public class UserLoginNew
    {
        public int uId{ get; set; }
        public string EmailId { get; set; }

        public string DheyaEmailId { get; set; }
        public string UserStatus { get; set; }
        public string CdfApproved { get; set; }
        public int CdfLevel { get; set; }
        public string UserName { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
         public string IsAllowReport { get; set; }
        public string casualImg { get; set; }
        public string formalImg { get; set; }
        public string Status { get; set; }
        public string ContactNo { get; set; }

    }
}