using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDFAPI.Models
{
    public class KYQuestions
    {
        public int qno { get; set; }
        public string questext { get; set; }
        public string op1 { get; set; }
        public string op2 { get; set; }
        public string op3 { get; set; }
        public int factorno { get; set; }
        public int opblue { get; set; }
        public int opred { get; set; }
    }

    public class KYQuestionsByUser
    {
        public int qno { get; set; }
        public string questext { get; set; }
        public string op1 { get; set; }
        public string op2 { get; set; }
        public string op3 { get; set; }
        public int factorno { get; set; }
        public int opblue { get; set; }
        public int opred { get; set; }
        public bool CompleteFlag { get; set; }
    }

    public class KYAnswers
    {
        public int qno { get; set; }
        public int factorno { get; set; }
        public int marks { get; set; }
        public int batid { get; set; }

    }
    public class Assesment
    {
        public string btnPersonality { get; set; }
        public string lblAssesment { get; set; }
        public string lblHeading { get; set; }
        public string lblInfo { get; set; }
        public string lblMainMsg { get; set; }

    }
    public class SeparateTestStatus
    {
        public string click { get; set; }
        public string Heading { get; set; }
        public string know { get; set; }
        public string no1 { get; set; }
        public string no2 { get; set; }
        public string Note { get; set; }
        public string NoteM { get; set; }
        public string notime { get; set; }
        public string persn { get; set; }
        public string personality { get; set; }
        public string status { get; set; }
        public string Page { get; set; }
    }

    public class KyAndPD
    {
        public int uid { get; set; }
    }
}