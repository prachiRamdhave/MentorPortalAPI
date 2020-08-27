using CDFAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using static CDFAPI.Models.CareerSearch;

namespace CDFAPI.Controllers
{
    [Authorize]
    [EnableCorsAttribute("*", "*", "*")]
    public class CareerSearchDropDownController : ApiController
    {
        CareerSearchRepository CR = new CareerSearchRepository();
        // Search Course
        [HttpGet]
        [Route("api/CareerSearchDropDown/GetAllCourse")]
        public List<Course1> GetCourse()
        {
            return CR.GetAllCourseddl();
        }

        // career Explorer 

        [HttpGet]
        [Route("api/CareerSearchDropDown/GetCareerExplorerGraduation")]

        public List<GraduationDtl> GetCExplorerGraduation()
        {
            return CR.GetAllGraduation();

        }
        [HttpGet]
        [Route("api/CareerSearchDropDown/GetAllTypeOfWork")]
        public List<TypeOFWorkDDL> GetTypeOfWork()
        {
            return CR.GetTypeOfWorkForCareer();
        }

        [HttpGet]
        [Route("api/CareerSearchDropDown/GetAllFieldOfWork")]
        public List<FieldOfWorkDDL> GetFieldOfWorkDDL()
        {
            return CR.GetAllFieldOfWorkDDL();
        }

        //Bhagyashree

        [Route("api/CareerSearchDropDown/DDL_Course")]
        public List<Course> GetDDL_Course()
        {
            return CR.GetDDL_Course();
        }

        [Route("api/CareerSearchDropDown/DDL_CoursebyCareer")]
        public List<Course> GetDDL_CoursebyCareer(int CareerID)
        {
            return CR.GetDDL_CoursebyCareer(CareerID);
        }

        [Route("api/CareerSearchDropDown/DDL_SubCourse")]
        public List<SubCourse> GetDDL_SubCourse()
        {
            return CR.GetDDL_SubCourse();
        }

        [Route("api/CareerSearchDropDown/DDL_SubCourseByCourse")]
        public List<SubCourse> GetDDL_SubCourseByCourse(int CourseID)
        {
            return CR.GetDDL_SubCourseByCourse(CourseID);
        }

        [Route("api/CareerSearchDropDown/DDL_Specialization")]
        public List<Specialization> GetDDL_Specialization(int Subco_id, int Co_id)
        {
            return CR.GetDDL_Specialization(Subco_id, Co_id);
        }

        [Route("api/CareerSearchDropDown/DDL_State")]
        public List<State> GetDDL_State()
        {
            return CR.GetDDL_State();
        }

        [Route("api/CareerSearchDropDown/DDL_City")]
        public List<City> GetDDL_City()
        {
            return CR.GetDDL_City();
        }

        [Route("api/CareerSearchDropDown/DDL_CityByState")]
        public List<City> GetDDL_CityByState(string State)
        {
            return CR.GetDDL_CityByState(State);
        }

        [Route("api/CareerSearchDropDown/List_SearchInstitute")]
        public List<InstituteSearch> GetList_SearchInstitute(int career, int course, int subcourse, string specialization, string state, string city)
        {
            return CR.GetList_SearchInstitute(career, course, subcourse, specialization, state, city);
        }
        [Route("api/CareerSearchDropDown/List_InstituteSearchByCareerCourse")]
        public List<InstituteSearch> GetList_InstituteSearchByCareerCourse(int career, int course)
        {
            return CR.GetList_InstituteSearchByCareerCourse(career, course);
        }

        [Route("api/CareerSearchDropDown/List_InstituteSubCourseDetails")]
        public AllList GetList_InstituteSubCourseDetails(int inst_id, int Subcourse_id, string Specialization)
        {
            return CR.GetList_InstituteSubCourseDetails(inst_id, Subcourse_id, Specialization);
        }
        [Route("api/CareerSearchDropDown/List_EntranceExamDetails")]
        public List<EntranceExamDetails> GetList_List_EntranceExamDetails(int EntranceId)
        {
            return CR.GetList_List_EntranceExamDetails(EntranceId);
        }
    }
}
