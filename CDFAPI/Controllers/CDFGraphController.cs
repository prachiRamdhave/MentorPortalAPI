using CDFAPI.Models;
using CDFAPI.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.UI.WebControls;
using log4net;

namespace CDFAPI.Controllers
{
    [Authorize]
    [EnableCorsAttribute("*", "*", "*")]
    public class CDFGraphController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        CDFGraphRepository GR = new CDFGraphRepository();
        [HttpGet]
        [Route("api/CDFGraph/GetCDFGraphDtl")]

        public CDFGraph GetCDFGraph(int cId)
        {
            return GR.GetcdfGraph(cId);
        }

        [Route("api/CDFGraph/GetStudentGraphDtl")]
        public StudentGraph GetStudentGraph(int cId)
        {
            return GR.GetStudentGraph(cId);
        }

        //[HttpGet]
        //[Route("api/CDFGraph/DownloadFile")]
        //public void DownloadFile(string fname)
        //{
        //    try
        //    {

        //        // HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
        //        bool forceDownload = true;
        //        string path = HttpContext.Current.Server.MapPath("./Reports_pdf/" + fname);
        //        string name = Path.GetFileName(path);
        //        string ext = Path.GetExtension(path);
        //        string type = "";
        //        // set known types based on file extension  
        //        if (ext != null)
        //        {
        //            switch (ext.ToLower())
        //            {
        //                case ".htm":
        //                case ".html":
        //                    type = "text/HTML";
        //                    break;

        //                case ".txt":
        //                    type = "text/plain";
        //                    break;

        //                case ".doc":
        //                case ".rtf":
        //                    type = "Application/msword";
        //                    break;

        //                case ".pdf":
        //                    type = "Application/pdf";
        //                    break;
        //            }
        //        }
        //        if (forceDownload)
        //        {
        //            HttpContext.Current.Response.AppendHeader("content-disposition",
        //                "attachment; filename=" + name);

        //            //Response.AppendHeader("content-disposition",
        //            //                " inline; attachment; filename=" + name + ".pdf");
        //        }
        //        if (type != "")
        //            HttpContext.Current.Response.ContentType = type;
        //        HttpContext.Current.Response.WriteFile(path);
        //        HttpContext.Current.Response.End();
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error(ex);
        //      //  return null;
        //    }
        //    }


    }
}
