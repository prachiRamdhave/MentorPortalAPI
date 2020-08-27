using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDFAPI.Models
{
    public class CDFProfile
    {
        public int stateID { get; set; }
        public string stateName { get; set; }
        public string cityName { get; set; }
        public string countryCode { get; set; }
        public string address2 { get; set; }
        public int CountryID { get; set; }
        public int uId { get; set; }
        public Nullable<int> userTypeId { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string gender { get; set; }
        public Nullable<System.DateTime> dob { get; set; }
        public string contactNo { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public Nullable<long> cityid { get; set; }
        public string pincode { get; set; }
        public string address { get; set; }
        public string cdfApproved { get; set; }
        public Nullable<int> cdfLevel { get; set; }
        public Nullable<decimal> cdfrating { get; set; }
        public string dheyaEmail { get; set; }
        public Nullable<System.DateTime> regDateTime { get; set; }
        public string standard { get; set; }
        public string schoolName { get; set; }
        public string division { get; set; }
        public string occupation { get; set; }
        public string status { get; set; }
        public string userStatus { get; set; }
        public string userSource { get; set; }
        public Nullable<bool> profileDisplayApproval { get; set; }
        public string cdfId { get; set; }
        public Nullable<bool> profileUpdateApproval { get; set; }
        public Nullable<System.DateTime> dateModified { get; set; }
        public string modifiedBy { get; set; }

        public string city { get; set; }

        //User Details
        public string qualification { get; set; }
        public string whyThisOpp { get; set; }
        public string working { get; set; }
        public string designation { get; set; }
        public string oraganisation { get; set; }
        public string maritalstatus { get; set; }
        public string linkedin { get; set; }
        public string facebook { get; set; }
        public string twitter { get; set; }
        public string aboutSelf { get; set; }
        public string resume { get; set; }
        public string formalImg { get; set; }
        public string casualImg { get; set; }
        public string spouseName { get; set; }
        public string spouseAge { get; set; }
        public string childrenAge { get; set; }
        public string feesPay { get; set; }
        public string ndaStatus { get; set; }
        public string preStatus { get; set; }
        public string postStatus { get; set; }
        public string profilePicDisplay { get; set; }
        public string authCode { get; set; }
        public Nullable<bool> idcard { get; set; }
        public Nullable<bool> certificate { get; set; }
        public Nullable<bool> visitingCard { get; set; }
        public Nullable<bool> ndaCopy { get; set; }
        public Nullable<int> batchId { get; set; }
        public string comments { get; set; }
        public string refundStatus { get; set; }
        public Nullable<int> refundAmount { get; set; }
        public Nullable<bool> childTestStatus { get; set; }
        public Nullable<bool> childSessionStatus { get; set; }
        public Nullable<bool> spouseTestStatus { get; set; }
        public Nullable<int> shadowSession { get; set; }
        public string accountDetails { get; set; }
        public int yearsOfExperience { get; set; }
        public string fieldOfWork { get; set; }
        public string modeOfWork { get; set; }
        public string industrySector { get; set; }
        public string remark { get; set; }
        public string classification { get; set; }

        //Bank Details       
        public string accountHolderName { get; set; }
        public string accountNumber { get; set; }
        public string bankName { get; set; }
        public string branchName { get; set; }
        public string ifscNo { get; set; }
        public string state { get; set; }
        public class CDFLevelDetails
        {
            public int uId { get; set; }
            public int CDFLevel { get; set; }

        }

        public class BankDtl
        {
            public string accountHolderName { get; set; }
            public string accountNumber { get; set; }
            public string bankName { get; set; }
            public string branchName { get; set; }
            public string ifscNo { get; set; }
        }

    }
}