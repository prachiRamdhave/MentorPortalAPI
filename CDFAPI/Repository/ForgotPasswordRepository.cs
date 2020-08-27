using CDFAPI.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;

namespace CDFAPI.Repository
{
    public class ForgotPasswordRepository
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        DataContex dc = new DataContex();

        // Forgot password send OPT to user email
        public int ForgotPassword(ForgotPassword userEmail)
        {
            int flag = 0;
            try
            {
                //get User Id
                //int uId = GetUserId(userEmail);
                string[] data = GetUserId(userEmail);
                int uId = 0;
                string contactNo = "";
                string email = "";

                if (data != null)
                {
                    uId = Convert.ToInt32(data[0]);
                    contactNo = data[1].ToString();
                    email = data[2].ToString();
                }

                if (uId != 0)
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        //Generate random token
                        int token = Convert.ToInt32(GenerateRandomNo());// + "" + uId);
                        string body = string.Empty;
                        using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["resetpassword"])))
                        {
                            body = reader.ReadToEnd();
                        }
                        body = body.Replace("{UserName}", email);
                        body = body.Replace("{OTP}", "" + token);

                        SqlCommand cmd = new SqlCommand("spCDFInsertForgotPassword", con);
                        cmd.CommandType = CommandType.StoredProcedure;                       
                        cmd.Parameters.AddWithValue("@uId", uId);
                        cmd.Parameters.AddWithValue("@token", token);

                        con.Open();
                        int intEffectedRows = cmd.ExecuteNonQuery();
                        if (intEffectedRows > 0)
                        {
                            //if (dc.SendEmail(email, ConfigurationManager.AppSettings["resetpwdsubject"].ToString(), body))
                            //{
                            //    flag = uId;
                            //}
                            if (dc.SendEmail(email, ConfigurationManager.AppSettings["resetpwdsubject"].ToString(), body))
                            {
                                flag = uId;
                            }
                            //send OTP on User SMS
                            string SMSText = ConfigurationManager.AppSettings["resetpwdOPTSMS"].ToString();
                            SMSText = SMSText.Replace("{OTP}", "" + token);
                            //UserRegister userRegister = GetAllUsersDetailsByid(uId);
                            dc.sendSms(contactNo, SMSText);
                        }
                    }
                }
                return flag;
            }
            catch (Exception ex)
            {
                Log.Error("" + ex);
                return flag;
            }
        }

        //get user id if exist
        public string[] GetUserId(ForgotPassword userEmail)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("spCDFGetUserId", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@email", userEmail.email);                    
                    con.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        sdr.Read();
                        string[] data = new string[3];
                        data[0] = sdr[0].ToString();
                        data[1] = sdr[1].ToString();
                        data[2] = sdr[2].ToString();

                        return data;
                        //return Convert.ToInt32(sdr[0]);
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

        //Generate Random number
        private int GenerateRandomNo()
        {
            Random _rdm = new Random();
            return _rdm.Next(1000, 9999);
        }

        // Check OPT
        public Boolean CheckOTP(int uId, CheckOTP checkOTP)
        {
            Boolean flag = false;
            try
            {
                //get User Id
                int otp = checkOTP.OTP;
                if (uId != 0 && otp != 0)
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        SqlCommand cmd = new SqlCommand("spCDFGetOTP", con);
                        cmd.CommandType = CommandType.StoredProcedure;                        
                        cmd.Parameters.AddWithValue("@token", checkOTP.OTP);
                        cmd.Parameters.AddWithValue("@uId", uId);
                        con.Open();
                        SqlDataReader sdr = cmd.ExecuteReader();
                        if (sdr.HasRows)
                        {
                            flag = true;
                        }
                        else
                        {
                            flag = false;
                        }
                    }
                }
                return flag;
            }
            catch (Exception ex)
            {
                Log.Error("" + ex);
                return flag;
            }
        }
    }
}