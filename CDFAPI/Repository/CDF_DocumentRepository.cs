using CDFAPI.Models;
using log4net;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CDFAPI.Repository
{
    public class CDF_DocumentRepository
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public List<CDF_DocumentModel> GetDocumentTypeDDL()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    List<CDF_DocumentModel> li = new List<Models.CDF_DocumentModel>();
                    SqlCommand cmd = new SqlCommand("SP_CDF_DocumentType", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Type", "DDL_DocType");
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            CDF_DocumentModel obj = new CDF_DocumentModel();
                            obj.doc_id = Convert.ToInt32(dr["DocTypeID"]);
                            obj.doc_type = dr["DocType"].ToString();
                            li.Add(obj);
                        }
                        return li;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return null;
            }
        }

        public List<CDFDocs> GetCDFDocument(int Uid)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    List<CDFDocs> li = new List<Models.CDFDocs>();
                    SqlCommand cmd = new SqlCommand("SP_GET_InsertUpdate_CDF_Doc", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Type", "GET");
                    cmd.Parameters.AddWithValue("@UID", Uid);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            CDFDocs obj = new CDFDocs();
                            obj.doc_TypeId = Convert.ToInt32(dr["DocTypeID"]);
                            obj.doc_Number = dr["Doc_Number"].ToString();
                            obj.doc_Img = dr["Doc_Img"].ToString();
                            obj.Doc_Verification = dr["Doc_Verification"].ToString();
                            li.Add(obj);
                        }
                        return li;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return null;
            }
        }

        public bool PostCDFDocument(CDFDocs cdfdoc)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SP_GET_InsertUpdate_CDF_Doc", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Type", "INSERT");
                    cmd.Parameters.AddWithValue("@DocTypeID", cdfdoc.doc_TypeId);
                    cmd.Parameters.AddWithValue("@UID", cdfdoc.UID);
                    cmd.Parameters.AddWithValue("@Doc_Number", cdfdoc.doc_Number);
                    cmd.Parameters.AddWithValue("@Doc_Img", cdfdoc.doc_Img);
                    cmd.Parameters.AddWithValue("@Doc_Verification", cdfdoc.Doc_Verification);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    if (i > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return false;
            }
        }

        public bool PutCDFDocument(CDFDocs cdfdoc)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SP_GET_InsertUpdate_CDF_Doc", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Type", "UPDATE");
                    cmd.Parameters.AddWithValue("@DocTypeID", cdfdoc.doc_TypeId);
                    cmd.Parameters.AddWithValue("@UID", cdfdoc.UID);
                    cmd.Parameters.AddWithValue("@Doc_Number", cdfdoc.doc_Number);
                    cmd.Parameters.AddWithValue("@Doc_Img", cdfdoc.doc_Img);
                    cmd.Parameters.AddWithValue("@Doc_Verification", cdfdoc.Doc_Verification);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    if (i > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return false;
            }
        }

    }
}