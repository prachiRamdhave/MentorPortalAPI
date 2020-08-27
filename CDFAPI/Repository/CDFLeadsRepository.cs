using CDFAPI.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CDFAPI.Repository
{
    public class CDFLeadsRepository
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

        public List<Leads> GetLeadsByCDF(int CDF_ID)
        {
            try
            {
                List<Leads> lst = new List<Models.Leads>();
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SP_CDF_Leads", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CDF_ID", CDF_ID);
                    cmd.Parameters.AddWithValue("@type", "GetLeadsByCDFID");
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            Leads ls = new Leads();
                            ls.id = dr["Id"].ToString();
                            ls.fname = dr["FName"].ToString();
                            ls.lname = dr["LName"].ToString();
                            ls.contactNo = dr["ContactNo"].ToString();
                            ls.email = dr["Email"].ToString();
                            ls.createdDate = Convert.ToDateTime(dr["RegDate"]);
                            ls.leadStatus = dr["LeadStatus"].ToString();
                            ls.referedBy = dr["ReferedByEmail"].ToString();
                            ls.leadSource = dr["LeadSource"].ToString();
                            ls.assignedExecutive = dr["ExecutiveName"].ToString();
                            ls.leadCategory = dr["LeadType"].ToString();

                            lst.Add(ls);
                        }
                        return lst;
                    }
                    else { return null; }
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return null;
            }
        }
    }
}