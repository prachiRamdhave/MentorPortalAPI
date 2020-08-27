using CDFAPI.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Configuration;

namespace CDFAPI.Repository
{
    public class SchedulingRepository
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        string strcon = ConfigurationManager.ConnectionStrings["career_portalConnectionString"].ConnectionString;
        DataContex DC = new DataContex();
        public List<Scheduling> GetSessionSchedule(int CDF_ID)//, DateTime FromDate, DateTime ToDate
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    List<Scheduling> lst = new List<Scheduling>();
                    SqlCommand cmd = new SqlCommand("SP_SessionDetails", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Type", "GET_CDF_SESSION_DTL");
                    cmd.Parameters.AddWithValue("@CDF_Id", CDF_ID);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            Scheduling s = new Scheduling();
                            s.StudId = Convert.ToInt32(ds.Tables[0].Rows[i]["StudId"]);
                            s.SesId = Convert.ToInt32(ds.Tables[0].Rows[i]["SesId"]);
                            s.SesDate = ds.Tables[0].Rows[i]["SesDate"].ToString();
                            s.SessionDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["Session_Date"]);
                            s.SesTime = ds.Tables[0].Rows[i]["SesTime"].ToString();
                            s.SesDateTime = Convert.ToDateTime(ds.Tables[0].Rows[i]["Ses_DDTT"]);
                            s.SesStatus = ds.Tables[0].Rows[i]["SesStatus"].ToString();
                            s.SesAddress = ds.Tables[0].Rows[i]["SesAddress"].ToString();
                            s.SesCity = ds.Tables[0].Rows[i]["SesCity"].ToString();
                            s.SesCityId = Convert.ToInt32(ds.Tables[0].Rows[i]["SesCityId"]);
                            s.productId = Convert.ToInt32(ds.Tables[0].Rows[i]["productId"]);
                            s.pro_name = ds.Tables[0].Rows[i]["pro_name"].ToString();
                            s.CandName = ds.Tables[0].Rows[i]["CandName"].ToString();
                            s.CandContact = ds.Tables[0].Rows[i]["CandContact"].ToString();
                            s.CandEmail = ds.Tables[0].Rows[i]["CandEmail"].ToString();
                            s.CandGender = ds.Tables[0].Rows[i]["CandGender"].ToString();
                            s.CDF_id = ds.Tables[0].Rows[i]["cdf_id"].ToString();
                            s.CdfAcceptance = ds.Tables[0].Rows[i]["CdfAcceptance"].ToString();
                            s.ConductingCDF = ds.Tables[0].Rows[i]["ConductingCDF"].ToString();
                            s.CDFMOb = ds.Tables[0].Rows[i]["CDFMOb"].ToString();
                            s.LeadType = ds.Tables[0].Rows[i]["LeadType"].ToString();
                            if (ds.Tables[0].Rows[i]["Shadow_id"] == DBNull.Value)
                            {
                                s.Shadow_id = 0;
                            }
                            else
                            {
                                s.Shadow_id = Convert.ToInt32(ds.Tables[0].Rows[i]["Shadow_id"]);
                            }
                            s.ShadowMob = ds.Tables[0].Rows[i]["ShadowMob"].ToString();
                            if (ds.Tables[0].Rows[i]["ShadowCDF"] == DBNull.Value)
                            { s.ShadowCDF = "NA"; }
                            else
                            {
                                s.ShadowCDF = ds.Tables[0].Rows[i]["ShadowCDF"].ToString();
                            }
                            s.shadowacceptance = ds.Tables[0].Rows[i]["shadowacceptance"].ToString();
                            //s.ShadowCDF = (ds.Tables[0].Rows[i]["ShadowCDF"].ToString() == null) ? System.DBNull.Value : ds.Tables[0].Rows[i]["ShadowCDF"].ToString());
                            //s.ShadowMob = (bool)ds.Tables[0].Rows[i]["ShadowMob"] ? DBNull.Value.ToString() : ds.Tables[0].Rows[i]["ShadowMob"].ToString();
                            //s.shadowacceptance = (bool)ds.Tables[0].Rows[i]["shadowacceptance"] ? DBNull.Value.ToString() : ds.Tables[0].Rows[i]["shadowacceptance"].ToString();

                            lst.Add(s);
                        }
                        return lst;
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

        public bool UpdateShadowSessionAcceptReject(AcceptRejShadow SAR)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    bool Res_CDF;
                    if (SAR.ShadowCDFResponse== "Y")
                    {
                        Res_CDF = true;
                    }
                    else
                    {
                        Res_CDF = false;
                    }
                    string strCommand = "update tbl_Session set Shadow_CDF_Acceptance=@ShadowCDF_Acceptance where Shadow_CDFId=@ShadowCDF_Id and Student_Id=@Student_Id";
                    SqlCommand cmd = new SqlCommand(strCommand, con);
                    cmd.Parameters.AddWithValue("@ShadowCDF_Acceptance", Res_CDF);
                    cmd.Parameters.AddWithValue("@ShadowCDF_Id", SAR.ShadowCDFId);
                    cmd.Parameters.AddWithValue("@Student_Id", SAR.StudentId);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
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

        public bool RequestOTPForSessionComp(RequestOTP ROTP)
        {
            try
            {
              
                int OTP = GenerateRandomNo();
                string StudetnName, ContactNo, studentEmail;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd1 = new SqlCommand("Sp_CDFsessionCompletionOTP", con);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@Cid", ROTP.StudentID);
                    cmd1.Parameters.AddWithValue("@token", OTP);
                    cmd1.Parameters.AddWithValue("@SP_Type", "INSERT");
                    con.Open();
                    int i = cmd1.ExecuteNonQuery();
                    con.Close();
                    if (i > 0)
                    {
                        string query = " select c_first_name as fname,c_last_name as lname,c_phone_no as contactNo,c_email_id as email from [cyberind_dheya].[dbo].[tbl_candidate_master] where c_id='" + ROTP.StudentID + "'";
                        SqlCommand cmd = new SqlCommand(query, con);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable ds = new DataTable();
                        da.Fill(ds);
                        if (ds != null)
                        {
                            if (ds.Rows.Count > 0)
                            {
                                StudetnName = ds.Rows[0]["fname"].ToString();
                                ContactNo = ds.Rows[0]["contactNo"].ToString();
                                studentEmail = ds.Rows[0]["email"].ToString();
                                string uname = ROTP.CDFName;

                                //string uname = Session["userName"].ToString();
                                //send OTP on User SMS
                                //string SMSText = WebConfigurationManager.AppSettings["sessionCompleteOTP"].ToString();

                               
                                string SMSText = ConfigurationManager.AppSettings["sessionCompleteOTP"].ToString();
                                SMSText = SMSText.Replace("{Name}", "" + StudetnName);
                                SMSText = SMSText.Replace("{OTP}", "" + OTP);
                                SMSText = SMSText.Replace("{MentorName}", "" + uname);
                                sendMail(StudetnName, studentEmail, SMSText);

                                DC.sendSms(ContactNo, SMSText);

                            }
                        }
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

        public bool UpdateSessionAcceptReject(AcceptRej AR)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    bool Res_CDF;
                    if (AR.CDFResponse == "Y")
                    {
                        Res_CDF = true;
                    }
                    else
                    {
                        Res_CDF = false;
                    }
                    string strCommand = "update tbl_Session set CDF_Acceptance=@CDF_Acceptance where CDF_Id=@CDF_Id and Student_Id=@Student_Id";
                    SqlCommand cmd = new SqlCommand(strCommand, con);
                    cmd.Parameters.AddWithValue("@CDF_Acceptance", Res_CDF);
                    cmd.Parameters.AddWithValue("@CDF_Id", AR.CDFId);
                    cmd.Parameters.AddWithValue("@Student_Id", AR.StudentId);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
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


        //Generate Random number
        public int GenerateRandomNo()
        {
            Random _rdm = new Random();
            return _rdm.Next(1000, 9999);
        }
        private Boolean sendMail(string StudetnName, string studentEmail, string msg)
        {
            try
            {
                {
                    System.Net.Mail.MailMessage message = new MailMessage("support.tech@dheya.com", studentEmail);

                    // stdMailId = message.ToString();
                    StringBuilder str = new StringBuilder();

                    message.Subject = "Dheya Session completion OTP";
                    message.Body = msg;  // ConfigurationManager.AppSettings["assignSessionMail"].ToString();
                    message.Body = message.Body.Replace("{StudetnName}", StudetnName);
                    //msg.Subject = txttitle.Text;
                    //msg.Body = txtdescription.Text;

                    string DateFormat = "yyyyMMddTHHmmssZ";
                    string now = DateTime.Now.ToUniversalTime().ToString(DateFormat);

                    str.AppendLine("BEGIN:VCALENDAR");
                    str.AppendLine("PRODID:-//Schedule a Meeting");
                    str.AppendLine("VERSION:2.0");
                    str.AppendLine("METHOD:REQUEST");
                    str.AppendLine("BEGIN:VEVENT");

                    //str.AppendLine(string.Format("DTSTART:{0:yyyyMMddTHHmmssZ}", DateTime.Parse("2019-12-25 20:00:00").ToUniversalTime()));
                    //str.AppendLine(string.Format("DTSTART:{0:yyyyMMddTHHmmssZ}", DateTime.Parse(SessionDt).ToUniversalTime()));

                    //str.AppendLine(string.Format("DTSTAMP:{0:yyyyMMddTHHmmssZ}", DateTime.Now.ToUniversalTime().ToString(DateFormat)));
                    //str.AppendLine(string.Format("DTEND:{0:yyyyMMddTHHmmssZ}", DateTime.Parse(SessionDt).ToUniversalTime()));


                    //str.AppendLine("LOCATION: " + txtlocation.Text);
                    str.AppendLine(string.Format("UID:{0}", Guid.NewGuid()));
                    str.AppendLine(string.Format("DESCRIPTION:{0}", message.Body));
                    str.AppendLine(string.Format("X-ALT-DESC;FMTTYPE=text/html:{0}", message.Body));
                    str.AppendLine(string.Format("SUMMARY:{0}", message.Subject));
                    str.AppendLine(string.Format("ORGANIZER:MAILTO:{0}", message.From.Address));

                    str.AppendLine(string.Format("ATTENDEE;CN=\"{0}\";RSVP=TRUE:mailto:{1}", message.To[0].DisplayName, message.To[0].Address));

                    str.AppendLine("BEGIN:VALARM");
                    str.AppendLine("TRIGGER:-PT15M");
                    str.AppendLine("ACTION:DISPLAY");
                    str.AppendLine("DESCRIPTION:Reminder");
                    str.AppendLine("END:VALARM");
                    str.AppendLine("END:VEVENT");
                    str.AppendLine("END:VCALENDAR");

                    byte[] byteArray = Encoding.ASCII.GetBytes(str.ToString());
                    MemoryStream stream = new MemoryStream(byteArray);

                    // Attachment attach = new Attachment(stream, "test.ics");

                    // msg.Attachments.Add(attach);

                    System.Net.Mime.ContentType contype = new System.Net.Mime.ContentType("text/calendar");
                    contype.Parameters.Add("method", "REQUEST");
                    //  contype.Parameters.Add("name", "Meeting.ics");
                    AlternateView avCal = AlternateView.CreateAlternateViewFromString(str.ToString(), contype);
                    message.AlternateViews.Add(avCal);

                    //Now sending a mail with attachment ICS file.                     
                    System.Net.Mail.SmtpClient smtpclient = new System.Net.Mail.SmtpClient();
                    smtpclient.Host = "smtp.gmail.com";
                    //smtpclient.Port = 587;
                    //-------this has to given the Mailserver IP
                    smtpclient.EnableSsl = true;
                    smtpclient.Credentials = new System.Net.NetworkCredential("support.tech@dheya.com", "kglnnhvamtojscoa");
                    smtpclient.Send(message);

                    // Response.Write("Done");
                    return true;

                }
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        public bool UpdateSessionStatus(SessionStatus SS)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    bool flag = false, status = false;
                    string query = " select * from tbl_SessionCompletionOTP where status='ACTIVE' and Cid='" + SS.StudentId + "' and token='" + SS.OTP + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        flag = true;
                    }
                    else { flag = false; }
                    con.Close();
                    if (flag == true)
                    {
                        SqlCommand cmd1 = new SqlCommand("Sp_CDFsessionCompletionOTP", con);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.AddWithValue("@Cid", SS.StudentId);
                        cmd1.Parameters.AddWithValue("@token", SS.OTP);
                        cmd1.Parameters.AddWithValue("@SP_Type", "UPDATE");
                        con.Open();
                        int i = cmd1.ExecuteNonQuery();
                        con.Close();
                        if (i > 0)
                        {
                            SqlCommand cmd2 = new SqlCommand("Sp_CDFsessionCompletionOTP", con);
                            cmd2.CommandType = CommandType.StoredProcedure;
                            cmd2.Parameters.AddWithValue("@Cid", SS.StudentId);
                            cmd2.Parameters.AddWithValue("@CDFid", SS.CDFId);
                            cmd2.Parameters.AddWithValue("@SP_Type", "UPDATE_SESSION_STATUS");
                            con.Open();
                            int j = cmd2.ExecuteNonQuery();
                            con.Close();
                            if (j > 0)
                            {
                                status = true;
                            }
                        }
                        else
                        {
                            status = false;
                        }
                    }
                    return status;
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