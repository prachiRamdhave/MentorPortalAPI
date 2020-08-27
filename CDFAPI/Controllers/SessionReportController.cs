using CDFAPI.Models;
using CDFAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Net;
using System.Net.Http;


namespace CDFAPI.Controllers
{
    [Authorize]
    [EnableCorsAttribute("*", "*", "*")]
    public class SessionReportController : ApiController
    {
        SessionReportRepository SR = new SessionReportRepository();

        [Route("api/SessionReport/SessionReportDTLPost")]
        public string PostSessionReportDTL([FromBody]SessionReportModel obj)
        {
            return SR.PostSessionReportDTL(obj);
        }

        [Route("api/SessionReport/SessionReportDTLPut")]
        public string PutSessionReportDTL([FromBody]SessionReportModel obj)
        {
            return SR.PutSessionReportDTL(obj);
        }

        //[Route("api/SessionReport/CareerCategory")]
        //public List<CareerCategory> GetCareerCategory()
        //{
        //    return SR.CareerCategory();
        //}

        [Route("api/SessionReport/CareerCategory")]
        public List<CareerCategory> GetCareerCategory(int ca_id)
        {
            return SR.CareerCategory(ca_id);
        }


        //[Route("api/SessionReport/OccupationalCategory")]
        //public List<OccupationalCategory> GetOccupationalCategory()
        //{
        //    return SR.OccupationalCategory();
        //}
        [Route("api/SessionReport/OccupationalCategory")]
        public List<OccupationalCategory> GetOccupationalCategory(int ca_id)
        {
            return SR.OccupationalCategory(ca_id);
        }
        [Route("api/SessionReport/Career")]
        public List<Career> GetCareer()
        {
            return SR.Career();
        }

        [Route("api/SessionReport/GetSessionReportDetails")]
        public SessionReport GetAllSessionDtl(int CDF_ID, int SessionID)
        {
            return SR.GetSessionReportDtlBySesID(CDF_ID, SessionID);
        }
    }
}