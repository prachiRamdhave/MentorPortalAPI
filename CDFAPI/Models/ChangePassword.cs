using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDFAPI.Models
{
    public class ChangePassword
    {
        public int uId { get; set; }
        public string newPassword { get; set; }
    }
}