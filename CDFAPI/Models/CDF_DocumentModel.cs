using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDFAPI.Models
{
    public class CDF_DocumentModel
    {
        public int doc_id { get; set; }
        public string doc_type { get; set; }
    }

    public class CDFDocs
    {
        public int doc_TypeId { get; set; }
        public int UID { get; set; }
        public string doc_Number { get; set; }
        public string doc_Img { get; set; }
        public string Doc_Verification { get; set; }
    }
}