using CDFAPI.Models;
using CDFAPI.Repository;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
namespace CDFAPI.Controllers
{
    [Authorize]
    [EnableCorsAttribute("*", "*", "*")]
    public class ProfilePicController : ApiController
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        CDFRepository cdf = new CDFRepository();
        [Route("api/ProfilePic/updateProgilePic")]
        [HttpPut]
        public bool UpdateProfileImage(int uid, [FromBody] profilePic pc)
        {
            return cdf.PutProImg(uid, pc);
        }


        //[Route("api/ProfilePic/UploadProfile")]
        //public async Task<bool> Upload(string email, int uId)
        //{
        //    try
        //    {
        //        // var fileuploadPath = ConfigurationManager.AppSettings["FileUploadLocation"];

        //        var fullPath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/ProfileImages");

        //        var provider = new MultipartFormDataStreamProvider(fullPath);
        //        var content = new StreamContent(HttpContext.Current.Request.GetBufferlessInputStream(true));
        //        foreach (var header in Request.Content.Headers)
        //        {
        //            content.Headers.TryAddWithoutValidation(header.Key, header.Value);
        //        }

        //        await content.ReadAsMultipartAsync(provider);

        //        //Code for renaming the random file to Original file name
        //        // string uploadingFileName = provider.FileData.Select(x => x.LocalFileName).FirstOrDefault();
        //        //      string originalFileName = String.Concat(fileuploadPath, "\\" + (provider.Contents[0].Headers.ContentDisposition.FileName).Trim(new Char[] { '"' }));

        //        string fileName = email + "_formal_" + uId + ".jpg";
        //        string originalFileName = String.Concat(fullPath, "\\" + fileName);

        //        if (File.Exists(originalFileName))
        //        {
        //            File.Delete(originalFileName);
        //        }

        //        //File.Move(uploadingFileName, originalFileName);
        //        //return fileName;
        //        // Code renaming ends...
        //        bool flag = cdf.UpdateProfilePic(uId, fileName);
        //        if (flag == true)
        //        {
        //            string uploadingFileName = provider.FileData.Select(x => x.LocalFileName).FirstOrDefault();
        //            File.Move(uploadingFileName, originalFileName);
        //            return true;
        //        }
        //        else { return false; }
        //    }
        //    catch (Exception ex)
        //    {
        //        string msg = ex.Message;
        //        return false;
        //    }

        //}

        [Route("api/ProfilePic/UploadProfile")]
        public async Task<bool> Upload()
        {
            try
            {
                // var fileuploadPath = ConfigurationManager.AppSettings["FileUploadLocation"];



                var fullPath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/ProfileImages");



                var provider = new MultipartFormDataStreamProvider(fullPath);
                var content = new StreamContent(HttpContext.Current.Request.GetBufferlessInputStream(true));
                foreach (var header in Request.Content.Headers)
                {
                    content.Headers.TryAddWithoutValidation(header.Key, header.Value);
                }



                await content.ReadAsMultipartAsync(provider);



                //Code for renaming the random file to Original file name
                // string uploadingFileName = provider.FileData.Select(x => x.LocalFileName).FirstOrDefault();
                //      string originalFileName = String.Concat(fileuploadPath, "\\" + (provider.Contents[0].Headers.ContentDisposition.FileName).Trim(new Char[] { '"' }));



                int uId = int.Parse(HttpContext.Current.Request.Params.Get("uid"));
                string email = HttpContext.Current.Request.Params.Get("email");



                string fileName = email + "_formal_" + uId + ".jpg";
                string originalFileName = String.Concat(fullPath, "\\" + fileName);



                if (File.Exists(originalFileName))
                {
                    File.Delete(originalFileName);
                }



                //File.Move(uploadingFileName, originalFileName);
                //return fileName;
                // Code renaming ends...
                bool flag = cdf.UpdateProfilePic(uId, fileName);
                if (flag == true)
                {
                    string uploadingFileName = provider.FileData.Select(x => x.LocalFileName).FirstOrDefault();
                    File.Move(uploadingFileName, originalFileName);
                    return true;
                }
                else { return false; }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return false;
            }



        }

        [Route("api/ProfilePic/UpdateProfileImg")]
        [HttpPut]
        public bool UpdateProfileImg(int uid, string ImgFileName)
        {
            return cdf.UpdateProfilePic(uid, ImgFileName);
        }



        [HttpGet]
        [Route("api/ProfilePic/GetFile")]
        public HttpResponseMessage GetFile(int uid)
        {
            string fileName = cdf.GetImg(uid);
            //Create HTTP Response.
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

            //Set the File Path.
            string filePath = HttpContext.Current.Server.MapPath("~/App_Data/ProfileImages/") + fileName;

            //Check whether File exists.
            if (!File.Exists(filePath))
            {
                //Throw 404 (Not Found) exception if File not found.
                response.StatusCode = HttpStatusCode.NotFound;
                response.ReasonPhrase = string.Format("File not found: {0} .", fileName);
                throw new HttpResponseException(response);
            }

            //Read the File into a Byte Array.
            byte[] bytes = File.ReadAllBytes(filePath);

            //Set the Response Content.
            response.Content = new ByteArrayContent(bytes);

            //Set the Response Content Length.
            response.Content.Headers.ContentLength = bytes.LongLength;

            //Set the Content Disposition Header Value and FileName.
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = fileName;

            //Set the File Content Type.
            response.Content.Headers.ContentType = new MediaTypeHeaderValue(MimeMapping.GetMimeMapping(fileName));
            return response;
        }
    }
}
