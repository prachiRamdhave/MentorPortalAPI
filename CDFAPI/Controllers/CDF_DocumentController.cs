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
    public class CDF_DocumentController : ApiController
    {
        CDF_DocumentRepository DR = new CDF_DocumentRepository();
        [Route("api/CDF_Document/GetDocument")]
        public List<CDF_DocumentModel> GetDocumentTypeDDL()
        {
            return DR.GetDocumentTypeDDL();
        }

        [Route("api/CDF_Document/GetCDFDocument")]
        public List<CDFDocs> GetCDFDocument(int Uid)
        {
            return DR.GetCDFDocument(Uid);
        }

        [Route("api/CDF_Document/PostCDFDocument")]
        public bool PostCDFDocument([FromBody]CDFDocs cdfdoc)
        {
            return DR.PostCDFDocument(cdfdoc);
        }

        [Route("api/CDF_Document/PutCDFDocument")]
        public bool PutCDFDocument([FromBody]CDFDocs cdfdoc)
        {
            return DR.PutCDFDocument(cdfdoc);
        }
    }
}
