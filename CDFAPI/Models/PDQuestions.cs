using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDFAPI.Models
{
    public class PDQuestions
    {
        public int qno { get; set; }
        public string questext { get; set; }
        public string Most { get; set; }
        public string Least { get; set; }
        public string Status { get; set; }
        public int q_id { get; set; }
    }
    public class PDAnswers
    {
        public int Mostqno { get; set; }
        public int Leastqno { get; set; }
        public string MostCode { get; set; }
        public string LeastCode { get; set; }
        public string MostStatus { get; set; }
        public string LeastStatus { get; set; }
        public int qid { get; set; }
        public int batid { get; set; }
    }
    public class UserTestStatus
    {
        public int uId { set; get; }
        public int testid { set; get; }
        public int batid { set; get; }
        public string testStatus { set; get; }
        public string factorStatus { set; get; }
        public DateTime dataofcomplete { set; get; }
    }
    public class PDQuestionsByUser
    {
        public int qno { get; set; }
        public string questext { get; set; }
        public string Most { get; set; }
        public string Least { get; set; }
        public string Status { get; set; }
        public int q_id { get; set; }
        public bool CompleteFlag { get; set; }
    }
    public class TestStatus
    {
        public string KYStatus { get; set; }
        public string PDStatus { get; set; }
    }

}