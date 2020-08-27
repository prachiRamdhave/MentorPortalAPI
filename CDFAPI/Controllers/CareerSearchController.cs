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
    public class CareerSearchController : ApiController
    {
        CareerSearchRepository CR = new CareerSearchRepository();

        [HttpGet]
        [Route("api/CareerSearch/CareerExplorerCourseDtl")]
        public List<CourseDetails> GetCourseDtl(string CurEducation, string course)
        {
            return CR.GetAllCourseDtl(CurEducation, course);
        }
        [HttpGet]
        [Route("api/CareerSearch/SearchCourseInformation")]
        public List<CourseDetails> GetCourse(string Course)
        {
            return CR.GetCourseByCourseName(Course);
        }
        [HttpGet]
        [Route("api/CareerSearch/SearchCareer")]
        public List<CareerDetails> GetAllCareerDetails()
        {
            return CR.GetCareerDtl();
        }
    }
}