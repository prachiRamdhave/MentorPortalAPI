using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CDFAPI.Models;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using log4net;
using static CDFAPI.Models.PreRegistration;
using System.IO;
using System.Threading;

namespace CDFAPI.Repository
{
    public class PreRegistrationRepository
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        DataContex DC = new DataContex();
        public int PostPreReg1(PreRegistration.PreRegistration1 pre)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    int uid = 0;
                    string str = "INSERT INTO tblUserMaster (userTypeId ,fname ,lname,contactNo,email,password,regDateTime,status,userStatus,userSource) VALUES "
                        + "(2,@fname,@lname,@contactNo,@email,@Password,@RegDate,@status,@userStatus,@userSource)";
                                   //+ "('" + 2 + "','" + txt_fname.Text.Trim().ToString() + "','" + txt_lname.Text.Trim().ToString() + "' " 
                                   //+ ", '" + txt_contact.Text.Trim().ToString() + "', '" + Session["email"].ToString() + "',"
                                   //+ "'" + txt_password.Text + "','" + DateTime.Now + "','Reg_Complete','ACTIVE','DHEYA-CDF')";
                    
                    SqlCommand cmd = new SqlCommand(str, con);
                  
                    cmd.Parameters.AddWithValue("@fname", pre.FirstName);
                    cmd.Parameters.AddWithValue("@lname", pre.LastName);
                    cmd.Parameters.AddWithValue("@contactNo", pre.ContactNo);
                    cmd.Parameters.AddWithValue("@email",pre.Email);
                    cmd.Parameters.AddWithValue("@Password", pre.Password);
                    cmd.Parameters.AddWithValue("@RegDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@status", "Reg_Complete");
                    cmd.Parameters.AddWithValue("@userStatus", "ACTIVE");
                    cmd.Parameters.AddWithValue("@userSource", "DHEYA-CDF");

                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                   // con.Close();
                    if (i > 0)
                    {
                        str = "select uId from tblUserMaster where UserTypeId=2 and email='" + pre.Email + "'";
                        SqlCommand command = new SqlCommand(str,con);
                       // command.CommandText = str;

                        SqlDataReader sdr = command.ExecuteReader();
                        // int uid = Convert.ToInt32(command.ExecuteScalar().ToString());

                        //if (uid > 0)
                        if(sdr.HasRows)
                        {
                            sdr.Read();
                            uid = Convert.ToInt32(sdr["uId"]);
                            sdr.Close();
                            str = "";
                            //Insert remaining user's data into tblUserDetails table
                            str = "INSERT INTO tblUserDetails(uId) VALUES" + "(" + uid + ")";
                            command.CommandText = str;
                            int j = command.ExecuteNonQuery();
                            if (j > 0)
                            {
                                // email body
                                string body = this.PrepareBody(pre.FirstName);
                                if (!body.Equals(null))
                                {
                                    //sent email
                                    var task = new Thread(() => DC.sendemail(pre.Email.Trim(), null, null, ConfigurationManager.AppSettings["CDFRegistrationCompleteSubject"].ToString(), body));
                                    task.Start();
                                }
                            }              
                        }
                        return uid;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return 0;
            }
        }

        private string PrepareBody(string name)
        {
            try
            {
                string body = string.Empty;
                using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["CDFRegistrationCompleteTemplatePath"])))
                {
                    body = reader.ReadToEnd();
                }
                body = body.Replace("{UserName}", name);
                body = body.Replace("{CDFDashboardLink}", ConfigurationManager.AppSettings["DashboardLink"]);
                return body;
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                return null;
            }
        }

        public  PreRegistration.ExecutiveDtl GetExeDetail(string emailId)
        {
            try
            {
               
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        string str = "Select B.id, B.exeName,B.exeEmail from tblverifyRegistration as A Inner Join tblExecutive as B on A.executiveId = B.id  where A.status = 'ACTIVE' and A.email = '" + emailId + "'";
                        SqlCommand cmd = new SqlCommand(str, con);
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        ExecutiveDtl ED = new ExecutiveDtl();
                        dr.Read();
                        ED.exeId = Convert.ToInt32(dr["id"]);
                        ED.ExeName = dr["exeName"].ToString();
                        ED.ExeEmailId = dr["exeEmail"].ToString();
                        return ED;
                    }
                    else {
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

        public bool PostPreReg2(PreRegistration.PreRegistration2 pre2)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = con.CreateCommand();
                    SqlTransaction transaction;
                    con.Open();
                    // Start a local transaction.
                    transaction = con.BeginTransaction("RegisterTransaction");
                 
                    cmd.Connection = con;
                    cmd.Transaction = transaction;

                   // string date = DateConvert(pre2.Dob.ToString());

                    string str = "update tblUserMaster set gender=@gender,dob=@dob,cityid=@cityid,address=@address,pincode=@pincode,StateId=@StateId,CountryId=@CountryId,StateName=@StateName,CityName=@CityName where uid=@uid";
                    cmd.Parameters.AddWithValue("@gender", pre2.Gender);
                    cmd.Parameters.AddWithValue("@dob", pre2.Dob);
                    cmd.Parameters.AddWithValue("@cityid", pre2.CityId);
                    cmd.Parameters.AddWithValue("@address", pre2.Address);
                    cmd.Parameters.AddWithValue("@uId", pre2.Uid);
                    cmd.Parameters.AddWithValue("@pincode", pre2.PinCode);
                    cmd.Parameters.AddWithValue("@StateId", pre2.StateId);
                    cmd.Parameters.AddWithValue("@CountryId", pre2.CountryId);
                    cmd.Parameters.AddWithValue("@StateName", pre2.StateName);
                    cmd.Parameters.AddWithValue("@CityName", pre2.CityName);
                    cmd.CommandText = str;
                    int j = cmd.ExecuteNonQuery();
                    if (j > 0)
                    {
                        string str1 = "update tblUserDetails set qualification=@qualification,whyThisOpp=@whyThisOpp,designation=@designation,maritalstatus=@maritalstatus,spouseName=@spouseName,childrenAge=@childrenAge,fieldOfWork=@fieldOfWork,modeOfWork=@modeOfWork,industrySector=@industrySector,aboutSelf=@aboutSelf where uid=@uid";
                        cmd.Parameters.AddWithValue("@qualification",pre2.Qualification);
                        cmd.Parameters.AddWithValue("@whyThisOpp", pre2.WhyThisOpp);
                        cmd.Parameters.AddWithValue("@designation", pre2.designation);
                        cmd.Parameters.AddWithValue("@maritalstatus", pre2.maritalstatus);
                        cmd.Parameters.AddWithValue("@spouseName", pre2.spouseName);
                        cmd.Parameters.AddWithValue("@childrenAge", pre2.childrenAge);
                        cmd.Parameters.AddWithValue("@fieldOfWork", pre2.fieldOfWork);
                        cmd.Parameters.AddWithValue("@modeOfWork", pre2.modeOfWork);
                        cmd.Parameters.AddWithValue("@industrySector", pre2.industrySector);
                        cmd.Parameters.AddWithValue("@aboutSelf", pre2.aboutSelf);
                        cmd.CommandText = str1;
                        int k = cmd.ExecuteNonQuery();
                        if (k > 0)
                        {
                            // Insert user id & executive id into tblRelation table
                            string str2 = "INSERT INTO tblRelation (uId,executiveId) VALUES ('" + pre2.Uid + "','" + pre2.RelationshipManager + "')";
                            cmd.CommandText = str2;
                            int m = cmd.ExecuteNonQuery();
                            if (m > 0)
                            {
                                // Attempt to commit the transaction.
                                transaction.Commit();
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
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

        public string GetVerifyEmail(string emailId)
        {
            try
            {
                string VerifyEmailMsg = "";
               
                    string s = emailId;
                    if (s.Contains("@dheya"))
                    {
                        VerifyEmailMsg = "Dheya emailid is not allowed";
                    }
                    else
                    {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        string str = "select uId,fname,email from tblUserMaster  where UserTypeId=2 and email ='"+ emailId + "'";
                        SqlCommand cmd = new SqlCommand(str, con);
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            dr.Read();
                            //Print msg if already register 
                            VerifyEmailMsg = "You have already registered.";
                        }
                        else
                        {
                            dr.Close();
                            string query_verfiedUser = "Select executiveId,email from tblverifyRegistration where UserType=2 and status ='ACTIVE' and email = '"+ emailId+"'";
                            cmd = new SqlCommand(query_verfiedUser, con);

                            SqlDataReader sdr = cmd.ExecuteReader();

                            if (sdr.HasRows)
                            {
                                sdr.Read();

                                VerifyEmailMsg = "DoRegistration";
                            }
                            else
                            {
                                VerifyEmailMsg = "You are not authorized to register, for any further information please visit https://www.dheya.com  and feel free to contact us for any queries to assist you better at customer support at phone numbers: +91 99 23 400 555 | 020-24223655 / 65007555 or write us at care@dheya.com.";
                            }
                        }

                    }

                    

                }
                return VerifyEmailMsg;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return null;
            }


        }

        public string DateConvert(string date)
        {
            try
            {
                string[] tempdate = date.Split('/');
                if (tempdate.Length == 3)
                {
                    string dd = tempdate[0];
                    string mm = tempdate[1];
                    string yy = tempdate[2];
                    yy = yy.Substring(0, 4);
                    return mm + "/" + dd + "/" + yy;
                }
                else
                {
                    return date;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        // For CDF Test

        public List<PDQuestions> GetPDQuestion()
        {
            try
            {
                List<PDQuestions> questions = new List<PDQuestions>();
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SP_GET_Test_Ques", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Type", "Personality");
                    con.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            PDQuestions question = new PDQuestions();
                            question.qno = Convert.ToInt32(sdr["qno"]);
                            question.questext = sdr["questext"].ToString();
                            question.Most = sdr["Most"].ToString();
                            question.Least = sdr["Least"].ToString();
                            question.Status = sdr["Status"].ToString();
                            question.q_id = Convert.ToInt32(sdr["q_id"]);
                            questions.Add(question);
                        }
                        sdr.Close();
                        return questions;
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