using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDFAPI.Models
{
    public class Resources
    {
        public int id { get; set; }
        public int pId { get; set; }
        public string docName { get; set; }
        public string filePath { get; set; }
        public string tooltip { get; set; }
        public string previewPath { get; set; }

        public class resourcesPath
        {
            public int id { get; set; }
            public string previewPath { get; set; }
        }
    }
}