using CDFAPI.Models;
using CDFAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Threading.Tasks;
using System.Web;
using System.Configuration;
using System.IO;
using System.Drawing;

namespace CDFAPI.Controllers
{
    [Authorize]                                     
    [EnableCorsAttribute("*", "*", "*")]
    public class UpdateProfilePicController : ApiController
    {
        CDFRepository CR = new CDFRepository();

        [RoutePrefix("api/Upload")]
        public class UploadController : ApiController
        {
            CDFRepository cdf = new CDFRepository();

            [Route("user/PostUserImage")]
            [AllowAnonymous]
            public async Task<HttpResponseMessage> PostUserImage()
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();
                try
                {

                    var httpRequest = HttpContext.Current.Request;

                    foreach (string file in httpRequest.Files)
                    {
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);

                        var postedFile = httpRequest.Files[file];
                        if (postedFile != null && postedFile.ContentLength > 0)
                        {

                            int MaxContentLength = 1024 * 1024 * 1; //Size = 1 MB  

                            IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
                            var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                            var extension = ext.ToLower();
                            if (!AllowedFileExtensions.Contains(extension))
                            {

                                var message = string.Format("Please Upload image of type .jpg,.gif,.png.");

                                dict.Add("error", message);
                                return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                            }
                            else if (postedFile.ContentLength > MaxContentLength)
                            {

                                var message = string.Format("Please Upload a file upto 1 mb.");

                                dict.Add("error", message);
                                return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                            }
                            else
                            {
                                string queryString = @"http://www.dheya.com/cdf-dashboard/doc/demo";
                                 var filePath = HttpContext.Current.Server.MapPath(queryString + postedFile.FileName);
                                //var filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, queryString + postedFile.FileName );

                                //var fileName = Path.GetFileName(postedFile.FileName);
                                // postedFile.SaveAs(Path.Combine(@queryString, fileName));

                               
                               postedFile.SaveAs(filePath);

                            }
                        }

                        var message1 = string.Format("Image Updated Successfully.");
                        return Request.CreateErrorResponse(HttpStatusCode.Created, message1); ;
                    }
                    var res = string.Format("Please Upload a image.");
                    dict.Add("error", res);
                    return Request.CreateResponse(HttpStatusCode.NotFound, dict);
                }
                catch (Exception ex)
                {
                    var res = string.Format("some Message");
                    dict.Add("error", res);
                    return Request.CreateResponse(HttpStatusCode.NotFound, dict);
                }
            }

            [Route("user/updateUserImage")]
            [HttpPut]
            public bool UpdatePic(int uId, string ImageName)
            {
                 return cdf.UpdateUserProfilePic(uId, ImageName);
                //return cdf.UpdateImg(email,uId, ImageName);
            }
            [Route("user/GetUserProfilePic")]
            public string GetUserProfilePicURL(int uId)
            {
                return cdf.getUserPic(uId);
            }

            //[Route("user/updateProgilePic")]
            //[HttpPut]
            //public bool UpdateProfileImage(int uid, [FromBody] profilePic pc)
            //{
            //    return cdf.PutProImg(uid, pc);
            //}
        }
    }

}
