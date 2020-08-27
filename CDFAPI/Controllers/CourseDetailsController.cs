using CDFAPI.Models;
using CDFAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CDFAPI.Controllers
{
    [Authorize]
    [EnableCorsAttribute("*", "*", "*")]
    public class CourseDetailsController : ApiController
    {
        CourseDtlRepository CR = new CourseDtlRepository();
        [Route("api/CourseDetails/GetCourseDetailsById")]
        public CourseDtl GetCourseDtl(int id)
        {
            return CR.GetCourseDtlById(id);
        }

        [Route("api/CourseDetails/GetCourseListById")]
        public IEnumerable<careerList> GetCourseListbyId(int id)
        {
            return CR.GetCourseList(id);
        }
        [Route("api/CourseDetails/GetInstitutesZone")]
        public IEnumerable<InstZone> GetInstZone()
        {
            return CR.GetAllInstituteZone();
        }
    }
}
