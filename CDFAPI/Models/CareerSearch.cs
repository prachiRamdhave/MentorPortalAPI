using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDFAPI.Models
{
    public class CareerSearch
    {
        public class Course1 {
            public int CourseID { get; set; }
            public string CourseName { get; set; }
        }
        public class GraduationDtl {
            public string Graduation { get; set; }
        }

        public class CourseDetails
        {
            public int coID { get; set; }
            public string CourseName { get; set; }
            public string CourseCategory { get; set; }
        }
        public class CareerDetails
        {
            public int ca_id { get; set; }
            public string careerTitle { get; set; }
            public string TypeOfWork { get; set; }
            public string FutureRelevance { get; set; }
            public string FieldOfWork{ get; set; }
        }

        public class TypeOFWorkDDL
        {
            public string TypeOfWork { get; set; }
        }

        public class FieldOfWorkDDL
        {
            public string FieldOfWork { get; set; }
        }
        //Bhagyashree

        public class Course
        {
            public int co_id { get; set; }
            public string co_name { get; set; }
        }
        public class SubCourse
        {
            public int Subco_id { get; set; }
            public string Subco_name { get; set; }
        }
        public class State
        {
            public string StateName { get; set; }
        }
        public class City
        {
            public int City_id { get; set; }
            public string CityName { get; set; }
        }
        public class Specialization
        {
            public string SpecializationName { get; set; }
        }
        public class InstituteSearch
        {
            public int Subco_id { get; set; }
            public string Subco_name { get; set; }
            public string CityName { get; set; }
            public string SpecializationName { get; set; }
            public int Inst_ID { get; set; }
            public string InstName { get; set; }
        }
        public class InstituteDetails
        {
            public string instname { get; set; }
            public string catagory { get; set; }
            public string region { get; set; }
            public string state { get; set; }
            public string city { get; set; }
            public string l_website { get; set; }
            public string website { get; set; }
            public string affil { get; set; }
            public string email { get; set; }
            public string contact { get; set; }
            public string address { get; set; }
        }
        public class SubcourseDetails
        {
            public string subconame { get; set; }
            public string catagory { get; set; }
            public string stream { get; set; }
            public string specialization { get; set; }
            public string duration { get; set; }
            public string req { get; set; }
            public string descrip { get; set; }
            public string instreq { get; set; }
            public string rank { get; set; }
            public string indiatodayrank { get; set; }
            public string businesstodayrank { get; set; }
            public string hindustantimesrank { get; set; }
            public string dheya_rank { get; set; }
            public string entranceId { get; set; }
            public string entrancename { get; set; }
            public string OtherSpecialization { get; set; }


        }

        public class AllList
        {
            public List<InstituteDetails> InstituteDetails = new List<InstituteDetails>();
            public List<SubcourseDetails> SubcourseDetails = new List<SubcourseDetails>();
        }

        public class EntranceExamDetails
        {

            public string EntranceName { get; set; }
            public string Detail { get; set; }
            public string Req { get; set; }
            public string Fee { get; set; }
            public string Examdate { get; set; }
            public string Applicationdate { get; set; }
            public string Entrancelink { get; set; }

        }
    }
}