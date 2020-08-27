using CDFAPI.Models;
using CDFAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using static CDFAPI.Models.Resources;

namespace CDFAPI.Controllers
{
    [Authorize]
    [EnableCorsAttribute("*", "*", "*")]
    public class ResourcesController : ApiController
    {
        CDFRepository CR = new CDFRepository();
        [Route("api/Resources/ParentResources")]
        public IEnumerable<Resources> GetResources()
        {
            return CR.GetAllResources();
        }

        [Route("api/Resources/ResourcesById")]
        public IEnumerable<Resources> GetResourcesById(int pid)
        {
            return CR.GetResourcesFromId(pid);
        }

        [Route("api/Resources/GetResourcesPathFromId")]
        public IEnumerable<resourcesPath> GetResourcesPathById(int id)
        {
            return CR.GetResourcesPathFromId(id);
        }
    }
}
