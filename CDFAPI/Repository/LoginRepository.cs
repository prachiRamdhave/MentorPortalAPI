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
    public class LoginRepository
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        ////UserLoginRepository

        //login user with own credential
        public Output PostUserLogin(UserLogin ul)
        {
            Output op = new Output();
            op.flag = false;
            op.uid = 0;
            op.email = null;
            try
            {
                if (ul != null && ul.email != "" && ul.password != "" )
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {                     
                        SqlCommand cmd = new SqlCommand("spCDFUserlogin", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@username", ul.email);
                        cmd.Parameters.AddWithValue("@password", ul.password);
                        cmd.Parameters.AddWithValue("@usertype", ConfigurationManager.AppSettings["userTypeId"]);
                        con.Open();
                        SqlDataReader sdr = cmd.ExecuteReader();
                        if (sdr.HasRows)
                        {
                            sdr.Read();
                            op.uid = Convert.ToInt32(sdr[0].ToString());
                            op.email = sdr[4].ToString();
                            if (sdr[2].ToString() == "ACTIVE")
                            {
                                op.flag = true;
                                op.msg = "SUCCESS";
                            }
                            else
                            {
                                op.msg = "Your account has been deactivated!";
                            }
                            sdr.Close();

                        }
                        else
                            op.msg = "Invalid username or password!";

                        if (op.flag == true)
                        {
                            string str2 = "insert into tblLog (uId,log_type,log_time) values ( " + op.uid + ",'in',(SWITCHOFFSET(SYSDATETIMEOFFSET(), '+05:30')))";
                            SqlCommand cmd2 = new SqlCommand(str2, con);
                            int i = cmd2.ExecuteNonQuery();
                        }
                    }
                }
                return op;
            }
            catch (Exception ex)
            {
                Log.Error("" + ex);
                op.msg = "Something went wrong!Try again!";
                return op;
            }
        }

        public UserLoginNew GetUserLoginNew(string userName, string password)
        {
            //[sp_UserLoginNew]
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_UserLoginNew", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@username",userName);
                    cmd.Parameters.AddWithValue("@password", password);
                    con.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        UserLoginNew UL = new UserLoginNew();
                        while (sdr.Read())
                        {
                            UL.UserName = sdr["fname"].ToString() + " " + sdr["lname"].ToString();
                            UL.FName = sdr["fname"].ToString();
                            UL.LName = sdr["lname"].ToString();
                            UL.uId = Convert.ToInt32(sdr["uid"]);
                            UL.EmailId = sdr["email"].ToString();
                            UL.UserStatus = sdr["userStatus"].ToString();
                            UL.CdfLevel = Convert.ToInt32(sdr["cdfLevel"]);
                            UL.DheyaEmailId = sdr["dheyaEmail"].ToString();
                            UL.IsAllowReport = sdr["IsAllowReport"].ToString();
                            UL.CdfApproved = sdr["cdfApproved"].ToString();
                            UL.Status = sdr["status"].ToString();
                            UL.ContactNo = sdr["contactNo"].ToString();
                            string casualImg = sdr["casualImg"].ToString();
                            if (casualImg == "No")                
                                UL.casualImg = "~/images/Avatar.png";
                            else
                                UL.casualImg= "~/doc/images/" + sdr["casualImg"].ToString();
                            string formalImg = sdr["formalImg"].ToString();
                            if (formalImg == "No")
                                UL.formalImg = "~/images/Avatar.png"; 
                            else
                                UL.formalImg= "~/doc/formalImg/" + sdr["formalImg"].ToString();
                        }
                        return UL;
                    }
                    else
                    {
                        return null;
                    }

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