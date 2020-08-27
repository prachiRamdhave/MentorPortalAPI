using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDFAPI.Models
{
    public class DheyaDocuments
    {
        // public int Child_Id { get; set; }
        public string Doc_name { get; set; }
        //  public string Tooltip { get; set; }
        //   public string Preview_path { get; set; }
        public int Parent_Id { get; set; }
        public int[] Child_Id1 { get; set; }
        public int[] Parent_ChildId1 { get; set; }
        public string[] Child_Doc_name { get; set; }
        public string[] Child_Tooltip { get; set; }
        public string[] Child_Preview_path { get; set; }

    }

    public class ChildDoc
    {
        public int[] Child_Id { get; set; }
        public string[] Doc_name { get; set; }
        public string[] Tooltip { get; set; }
        public string[] Preview_path { get; set; }
        public int[] Parent_Id { get; set; }
    }

    public class allList
    {
        public List<DheyaDocuments> ParentDheya = new List<DheyaDocuments>();
        public List<DheyaDocuments> ChildDheya = new List<DheyaDocuments>();
    }

    public class Root
    {
        public int P_id { get; set; }
        public string doc_name { get; set; }
        public object Data { get; set; }
    }
    public class Root2
    {
        public int P_id { get; set; }
        public string doc_name { get; set; }
        public object Data { get; set; }
    }
    public class Child
    {
        public int P_id { get; set; }
        public int C_id { get; set; }
        public string doc_name { get; set; }
        public string tooltip { get; set; }
        public string preview_path { get; set; }
    }
    public class List
    {
        public List<Root> root = new List<Root>();
    }
}