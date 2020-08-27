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
    public class ChangePasswordRepository
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public bool PutChangePassword(int id, ChangePassword cdfpass)
        {
            bool flag = false;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("spCDFChangePassword", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id",id);
                    cmd.Parameters.AddWithValue("@newPassword",cdfpass.newPassword);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    if(i > 0)
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
                Log.Error("" + ex);
                return flag;    
            }
        }
    }
}