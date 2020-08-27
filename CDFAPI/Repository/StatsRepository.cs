using CDFAPI.Models;
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
    public class StatsRepository
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        public Stats GetStats(string dheyaEmail, int uId)
        {
            Stats st = new Stats();
            string constr = ConfigurationManager.ConnectionStrings["CRM_ConnectionString"].ConnectionString;
            try
            {
                using (MySqlConnection con = new MySqlConnection(constr))
                {
                    DataTable dt2 = new DataTable();
                    string strcmd1 = "SELECT count(id) FROM suitecrm.leads as A inner join suitecrm.leads_cstm as B on A.id=B.id_c where A.refered_by='" + dheyaEmail + "' and deleted=0";
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand(strcmd1, con);
                    MySqlDataReader dr1 = cmd.ExecuteReader();
                    //Check if table has rows for required query
                    if (dr1.HasRows)
                    {
                        dr1.Read();
                        st.leadCreated = dr1.GetInt32(0);
                    }
                    dr1.Close();
                 
                }
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    //Check payment status of respective user from tblPayment table
                    string query_visit = "  select COUNT(log_id) as count from tblLog where uId='" + uId + "' and log_type = 'in'";
                    SqlCommand cmdvisit = new SqlCommand(query_visit, connection);
                    //visits_count.Text = cmdvisit.ExecuteScalar().ToString();
                    st.visitCount = cmdvisit.ExecuteScalar().ToString();
                    return st;
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