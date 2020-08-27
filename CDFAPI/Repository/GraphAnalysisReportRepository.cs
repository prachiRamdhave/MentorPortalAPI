using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CDFAPI.Models;
using System.Configuration;
using log4net;
using System.Data;
using System.Data.SqlClient;

namespace CDFAPI.Repository
{
    public class GraphAnalysisReportRepository
    {

        string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public GraphAnalysisReport GetSessionDtl(int CDFId, int SessionId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    
                    SqlCommand cmd = new SqlCommand("sp_GetSessionDetails",con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CDF_Id", CDFId);
                    cmd.Parameters.AddWithValue("@SessionId", SessionId);
                    con.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    //  List<GraphAnalysisReport> li = new List<GraphAnalysisReport>();
                    GraphAnalysisReport GR = new GraphAnalysisReport();
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                           
                            GR.StudId = Convert.ToInt32(sdr["Student_Id"]);
                            GR.StudName = sdr["StudentName"].ToString();
                            GR.sesId = Convert.ToInt32(sdr["Session_Id"]);
                            GR.SesDate = Convert.ToDateTime(sdr["Session_Date"]);
                            GR.SesTime = sdr["Session_Time"].ToString();
                            GR.SesAdd = sdr["SesAddress"].ToString();
                            GR.CDFId = Convert.ToInt32(sdr["CDFId"]);
                            GR.CDFName = sdr["CDFName"].ToString();
                            GR.ShadowCDFId = Convert.ToInt32(sdr["ShadowCDFId"]);
                            GR.ShadowCDFName = sdr["ShadowCDFName"].ToString();
                            //li.Add(GR);
                        }
                        return GR;
                    }
                    else
                        return null;

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