using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CDFAPI.Models
{
    public class ForgotPassword
    {
        [Required]
        public string email { get; set; }

       //public string userTypeId { get; set; }
    }

    public class CheckOTP
    {
        [Required]
        public int OTP { get; set; }

        //public string userTypeId { get; set; }
    }
}