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
    public class DheyaDocumentsController : ApiController
    {
        DheyaDocumentsRepository DR = new DheyaDocumentsRepository();
        [Route("api/DheyaDocuments/GetparentDocumentList")]
        [HttpGet]
        public List<DheyaDocuments> GetParentDocumentsDtl()
        {
            return DR.GetParentDocList1();
        }
        [Route("api/DheyaDocuments/GetChildDocumentList")]
        [HttpGet]
        public List<DheyaDocuments> GetChildDocList(int parentDocId)
        {
            return DR.GetChildDoc(parentDocId);
        }

        [Route("api/DheyaDocuments/GetDocumentList")]
        [HttpGet]
        public List GetDocList()
        {
            return DR.GetDocList();
        }

    }
}
