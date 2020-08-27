using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CDFAPI.Models;
using System.Configuration;
using log4net;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace CDFAPI.Repository
{
    public class DheyaDocumentsRepository
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public List<DheyaDocuments> GetParentDocList()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    List<DheyaDocuments> li = new List<DheyaDocuments>();
                    string strQuery = "SELECT id, doc_name,tooltip,preview_path FROM tblDocDirectory where status='ACTIVE' and p_id=0";
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    con.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            DheyaDocuments DD = new DheyaDocuments();
                            // DD.Child_Id = Convert.ToInt32(sdr["id"]);
                            DD.Doc_name = sdr["doc_name"].ToString();
                            //DD.Tooltip = sdr["tooltip"].ToString();
                            li.Add(DD);
                        }
                        return li;
                    }
                    return null;
                }

            }
            catch (Exception ex)
            {
                Log.Error("" + ex);
                return null;
            }

        }


        public List<DheyaDocuments> GetParentDocList1()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    List<DheyaDocuments> li = new List<DheyaDocuments>();
                    List<DheyaDocuments> Child = new List<DheyaDocuments>();
                    // List<DheyaDocuments> li = new List<DheyaDocuments>();
                    // allList li = new allList();
                    string strQuery = "SELECT id, doc_name,tooltip,preview_path FROM tblDocDirectory where status='ACTIVE' and p_id=0";
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    con.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            DheyaDocuments DD = new DheyaDocuments();
                            DD.Parent_Id = Convert.ToInt32(sdr["id"]);
                            DD.Doc_name = sdr["doc_name"].ToString();
                            //  DD.Tooltip = sdr["tooltip"].ToString();
                            li.Add(DD);
                            // ------------------New code For child details end

                            SqlConnection con1 = new SqlConnection(connectionString);
                            string strQuery1 = "SELECT id, doc_name,tooltip,preview_path,p_id FROM tblDocDirectory where status='ACTIVE' and p_id=" + DD.Parent_Id;
                            SqlCommand cmd1 = new SqlCommand(strQuery1, con1);
                            con1.Open();
                            SqlDataAdapter sda = new SqlDataAdapter(cmd1);
                            DataSet ds = new DataSet();
                            sda.Fill(ds);
                            //  SqlDataReader sdr1 = cmd.ExecuteReader();
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                {
                                    DheyaDocuments DD1 = new DheyaDocuments();
                                    //ChildDoc DD1 = new ChildDoc();
                                    DD1.Parent_ChildId1 = new int[] { Convert.ToInt32(ds.Tables[0].Rows[i]["p_id"]) };
                                    DD1.Child_Id1 = new int[] { Convert.ToInt32(ds.Tables[0].Rows[i]["id"]) };
                                    DD1.Child_Doc_name = new string[] { ds.Tables[0].Rows[i]["doc_name"].ToString() };
                                    DD1.Child_Tooltip = new string[] { ds.Tables[0].Rows[i]["tooltip"].ToString() };
                                    string path = ds.Tables[0].Rows[i]["preview_path"].ToString();
                                    DD1.Child_Preview_path = new string[] { "https://dheya.com/cdf-dashboard/doc/cdf-doc/" + path };
                                    // li.ChildDheya.Add(DD1);
                                    li.Add(DD1);
                                }
                                //  li.AddRange(Child);

                            }


                            //-------------------New code for child details end 

                        }
                        return li;
                    }
                    return null;
                }

            }
            catch (Exception ex)
            {
                Log.Error("" + ex);
                return null;
            }

        }

        public List<DheyaDocuments> GetChildDoc(int parentDocId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    List<DheyaDocuments> li = new List<DheyaDocuments>();
                    string strQuery = "SELECT id, doc_name,tooltip,preview_path,p_id FROM tblDocDirectory where status='ACTIVE' and p_id=" + parentDocId;
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    con.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            DheyaDocuments DD = new DheyaDocuments();
                            DD.Parent_Id = Convert.ToInt32(sdr["p_id"]);
                            // DD.Child_Id = Convert.ToInt32(sdr["id"]);
                            DD.Doc_name = sdr["doc_name"].ToString();
                            //DD.Tooltip = sdr["tooltip"].ToString();
                            string path = sdr["preview_path"].ToString();
                            // DD.Preview_path = "https://dheya.com/cdf-dashboard/doc/cdf-doc/" + path;
                            li.Add(DD);
                        }
                        return li;
                    }
                    return null;
                }

            }
            catch (Exception ex)
            {
                Log.Error("" + ex);
                return null;
            }

        }

        public List GetDocList()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    List obj = new List();
                    List<Root> li = new List<Root>();
                    List<Child> Child1 = new List<Child>();
                    List<Child> Child2 = new List<Child>();
                    List<Child> Child3 = new List<Child>();
                    List<Child> Child4 = new List<Child>();
                    List<Child> Child5 = new List<Child>();
                    List<Child> Child6 = new List<Child>();
                    List<Child> Child7 = new List<Child>();
                    Root _objLI1 = new Root();
                    Root _objLI2 = new Root();
                    Root _objLI3 = new Root();
                    Root _objLI4 = new Root();
                    Root _objLI5 = new Root();
                    Root _objLI6 = new Root();
                    Root _objLI7 = new Root();
                    // List<DheyaDocuments> li = new List<DheyaDocuments>();
                    // allList li = new allList();
                    string strQuery = "SELECT id, doc_name,tooltip,preview_path FROM tblDocDirectory where status='ACTIVE' and p_id=0";
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    con.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    //SqlDataAdapter da = new SqlDataAdapter(cmd);
                    //DataSet ds1 = new DataSet();
                    //da.Fill(ds1);

                    while (sdr.Read())
                    //for (int a = 0; a < ds1.Tables[0].Rows.Count; a++)
                    {
                        Root _objLI = new Root();
                        //  Root DD = new Root();
                        _objLI.P_id = Convert.ToInt32(sdr["id"]);
                        _objLI.doc_name = sdr["doc_name"].ToString();
                        //  DD.Tooltip = sdr["tooltip"].ToString();
                        li.Add(_objLI);
                        if (Convert.ToInt32(sdr["id"])==1)
                        {
                            _objLI1.P_id = Convert.ToInt32(sdr["id"]);
                            _objLI1.doc_name = sdr["doc_name"].ToString();
                            //  DD.Tooltip = sdr["tooltip"].ToString();
                            li.Add(_objLI1);
                        }
                        if (Convert.ToInt32(sdr["id"]) == 26)
                        {
                            _objLI2.P_id = Convert.ToInt32(sdr["id"]);
                            _objLI2.doc_name = sdr["doc_name"].ToString();
                            //  DD.Tooltip = sdr["tooltip"].ToString();
                            li.Add(_objLI2);
                        }
                        if (Convert.ToInt32(sdr["id"]) == 65)
                        {
                            _objLI3.P_id = Convert.ToInt32(sdr["id"]);
                            _objLI3.doc_name = sdr["doc_name"].ToString();
                            //  DD.Tooltip = sdr["tooltip"].ToString();
                            li.Add(_objLI3);
                        }
                        if (Convert.ToInt32(sdr["id"]) == 86)
                        {
                            _objLI4.P_id = Convert.ToInt32(sdr["id"]);
                            _objLI4.doc_name = sdr["doc_name"].ToString();
                            //  DD.Tooltip = sdr["tooltip"].ToString();
                            li.Add(_objLI4);
                        }
                        if (Convert.ToInt32(sdr["id"]) == 193)
                        {
                            _objLI5.P_id = Convert.ToInt32(sdr["id"]);
                            _objLI5.doc_name = sdr["doc_name"].ToString();
                            //  DD.Tooltip = sdr["tooltip"].ToString();
                            li.Add(_objLI5);
                        }
                        if (Convert.ToInt32(sdr["id"]) == 195)
                        {
                            _objLI6.P_id = Convert.ToInt32(sdr["id"]);
                            _objLI6.doc_name = sdr["doc_name"].ToString();
                            //  DD.Tooltip = sdr["tooltip"].ToString();
                            li.Add(_objLI6);
                        }
                        if (Convert.ToInt32(sdr["id"]) == 202)
                        {
                            _objLI7.P_id = Convert.ToInt32(sdr["id"]);
                            _objLI7.doc_name = sdr["doc_name"].ToString();
                            //  DD.Tooltip = sdr["tooltip"].ToString();
                            li.Add(_objLI7);
                        }

                        SqlConnection con1 = new SqlConnection(connectionString);
                        string strQuery1 = "SELECT id, doc_name,tooltip,preview_path,p_id FROM tblDocDirectory where status='ACTIVE' and p_id=" + _objLI.P_id;
                        SqlCommand cmd1 = new SqlCommand(strQuery1, con1);
                        con1.Open();
                        SqlDataAdapter sda = new SqlDataAdapter(cmd1);
                        DataSet ds = new DataSet();
                        sda.Fill(ds);
                        //  SqlDataReader sdr1 = cmd.ExecuteReader();
                        if (ds.Tables[0].Rows.Count > 0)
                        {

                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {

                                //ChildDoc DD1 = new ChildDoc();
                                if (Convert.ToInt32(ds.Tables[0].Rows[0]["p_id"]) == 1)
                                {
                                    Child DD1 = new Child();
                                    DD1.P_id = Convert.ToInt32(ds.Tables[0].Rows[i]["p_id"]);
                                    DD1.C_id = Convert.ToInt32(ds.Tables[0].Rows[i]["id"]);
                                    DD1.doc_name = ds.Tables[0].Rows[i]["doc_name"].ToString();
                                    DD1.tooltip = ds.Tables[0].Rows[i]["tooltip"].ToString();
                                    string path = ds.Tables[0].Rows[i]["preview_path"].ToString();
                                    DD1.preview_path = "https://dheya.com/cdf-dashboard/doc/cdf-doc/" + path;
                                    // li.ChildDheya.Add(DD1);
                                    // li.Add(DD1);
                                    Child1.Add(DD1);
                                    _objLI1.Data = Child1;
                                    //Child.Clear();
                                }
                                if (Convert.ToInt32(ds.Tables[0].Rows[i]["p_id"]) == 26)
                                {
                                    Child DD1 = new Child();
                                    DD1.P_id = Convert.ToInt32(ds.Tables[0].Rows[i]["p_id"]);
                                    DD1.C_id = Convert.ToInt32(ds.Tables[0].Rows[i]["id"]);
                                    DD1.doc_name = ds.Tables[0].Rows[i]["doc_name"].ToString();
                                    DD1.tooltip = ds.Tables[0].Rows[i]["tooltip"].ToString();
                                    string path = ds.Tables[0].Rows[i]["preview_path"].ToString();
                                    DD1.preview_path = "https://dheya.com/cdf-dashboard/doc/cdf-doc/" + path;
                                    // li.ChildDheya.Add(DD1);
                                    // li.Add(DD1);
                                    Child2.Add(DD1);
                                       _objLI2.Data = Child2;
                                    //Child.Clear();
                                }
                                if (Convert.ToInt32(ds.Tables[0].Rows[i]["p_id"]) == 65)
                                {
                                    Child DD1 = new Child();
                                    DD1.P_id = Convert.ToInt32(ds.Tables[0].Rows[i]["p_id"]);
                                    DD1.C_id = Convert.ToInt32(ds.Tables[0].Rows[i]["id"]);
                                    DD1.doc_name = ds.Tables[0].Rows[i]["doc_name"].ToString();
                                    DD1.tooltip = ds.Tables[0].Rows[i]["tooltip"].ToString();
                                    string path = ds.Tables[0].Rows[i]["preview_path"].ToString();
                                    DD1.preview_path = "https://dheya.com/cdf-dashboard/doc/cdf-doc/" + path;
                                    // li.ChildDheya.Add(DD1);
                                    // li.Add(DD1);
                                    Child3.Add(DD1);
                                    _objLI3.Data = Child3;
                                    //Child.Clear();
                                }
                                if (Convert.ToInt32(ds.Tables[0].Rows[i]["p_id"]) == 86)
                                {
                                    Child DD1 = new Child();
                                    DD1.P_id = Convert.ToInt32(ds.Tables[0].Rows[i]["p_id"]);
                                    DD1.C_id = Convert.ToInt32(ds.Tables[0].Rows[i]["id"]);
                                    DD1.doc_name = ds.Tables[0].Rows[i]["doc_name"].ToString();
                                    DD1.tooltip = ds.Tables[0].Rows[i]["tooltip"].ToString();
                                    string path = ds.Tables[0].Rows[i]["preview_path"].ToString();
                                    DD1.preview_path = "https://dheya.com/cdf-dashboard/doc/cdf-doc/" + path;
                                    // li.ChildDheya.Add(DD1);
                                    // li.Add(DD1);
                                    Child4.Add(DD1);
                                    _objLI4.Data = Child4;
                                    //Child.Clear();
                                }
                                if (Convert.ToInt32(ds.Tables[0].Rows[i]["p_id"]) == 193)
                                {
                                    Child DD1 = new Child();
                                    DD1.P_id = Convert.ToInt32(ds.Tables[0].Rows[i]["p_id"]);
                                    DD1.C_id = Convert.ToInt32(ds.Tables[0].Rows[i]["id"]);
                                    DD1.doc_name = ds.Tables[0].Rows[i]["doc_name"].ToString();
                                    DD1.tooltip = ds.Tables[0].Rows[i]["tooltip"].ToString();
                                    string path = ds.Tables[0].Rows[i]["preview_path"].ToString();
                                    DD1.preview_path = "https://dheya.com/cdf-dashboard/doc/cdf-doc/" + path;
                                    // li.ChildDheya.Add(DD1);
                                    // li.Add(DD1);
                                    Child5.Add(DD1);
                                    _objLI5.Data = Child5;
                                    //Child.Clear();
                                }
                                if (Convert.ToInt32(ds.Tables[0].Rows[i]["p_id"]) == 195)
                                {
                                    Child DD1 = new Child();
                                    DD1.P_id = Convert.ToInt32(ds.Tables[0].Rows[i]["p_id"]);
                                    DD1.C_id = Convert.ToInt32(ds.Tables[0].Rows[i]["id"]);
                                    DD1.doc_name = ds.Tables[0].Rows[i]["doc_name"].ToString();
                                    DD1.tooltip = ds.Tables[0].Rows[i]["tooltip"].ToString();
                                    string path = ds.Tables[0].Rows[i]["preview_path"].ToString();
                                    DD1.preview_path = "https://dheya.com/cdf-dashboard/doc/cdf-doc/" + path;
                                    // li.ChildDheya.Add(DD1);
                                    // li.Add(DD1);
                                    Child6.Add(DD1);
                                    _objLI6.Data = Child6;
                                    //Child.Clear();
                                }
                                if (Convert.ToInt32(ds.Tables[0].Rows[i]["p_id"]) == 202)
                                {
                                    Child DD1 = new Child();
                                    DD1.P_id = Convert.ToInt32(ds.Tables[0].Rows[i]["p_id"]);
                                    DD1.C_id = Convert.ToInt32(ds.Tables[0].Rows[i]["id"]);
                                    DD1.doc_name = ds.Tables[0].Rows[i]["doc_name"].ToString();
                                    DD1.tooltip = ds.Tables[0].Rows[i]["tooltip"].ToString();
                                    string path = ds.Tables[0].Rows[i]["preview_path"].ToString();
                                    DD1.preview_path = "https://dheya.com/cdf-dashboard/doc/cdf-doc/" + path;
                                    // li.ChildDheya.Add(DD1);
                                    // li.Add(DD1);
                                    Child7.Add(DD1);
                                    _objLI7.Data = Child7;
                                    //Child.Clear();
                                }

                            }
                            //  li.AddRange(Child);
                           // _objLI.Data = Child;
                            //  obj.root.Add(_objLI);
                        }
                        obj.root.Add(_objLI1);
                        obj.root.Add(_objLI2);
                        obj.root.Add(_objLI3);
                        obj.root.Add(_objLI4);
                        obj.root.Add(_objLI5);
                        obj.root.Add(_objLI6);
                        obj.root.Add(_objLI7);
                        // _objLI1.Data = "";
                    }

                    return obj;
                }

            }
            catch (Exception ex)
            {
                Log.Error("" + ex);
                return null;
            }

        }
    }
}