using CDFAPI.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace CDFAPI.Repository
{
    public class PersonalityRepository
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //Get all PD Test Question 96 Questions
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

        //Get all PD Test Question By language 96 Questions
        public List<PDQuestions> GetPDQuestionByLang(int langid)
        {
            try
            {
                List<PDQuestions> questions = new List<PDQuestions>();
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SP_GET_Test_Ques", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Type", "PersonalityByLanguage");
                    cmd.Parameters.AddWithValue("@LangID", langid);
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


        public string GetUserCount(int Uid)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string msg = "";
                    int cnt = 0;
                    SqlCommand cmd = new SqlCommand("SP_ViewCount_PDTest", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Type", "USER_MASTER_CNT");
                    cmd.Parameters.AddWithValue("@Uid", Uid);
                    con.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            cnt = Convert.ToInt32(sdr["UID_CNT"]);
                        }
                        sdr.Close();
                        if (cnt == 0)
                        {
                            return msg = "User Registered";
                        }
                        else
                        {
                            return msg = "User Not Complete Registration";
                        }
                    }
                    else
                        return msg = "";
                }

            }
            catch (Exception ex)
            {
                Log.Error("" + ex);
                return "";
            }
        }

        public bool GetProductMasterCount(int Uid)   // using ProductID=7
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    int cnt = 0;
                    int Prodid = 7;
                    SqlCommand cmd = new SqlCommand("SP_ViewCount_PDTest", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Type", "USER_PRODUCTMASTER_CNT");
                    cmd.Parameters.AddWithValue("@Uid", Uid);
                    cmd.Parameters.AddWithValue("@Prodid", Prodid);
                    con.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            cnt = Convert.ToInt32(sdr["ID_CNT"]);
                        }
                        sdr.Close();
                        if (cnt == 0)
                        {
                            if (GenerateAuthcode(Uid, Prodid))
                            {
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
                        return false;
                }

            }
            catch (Exception ex)
            {
                Log.Error("" + ex);
                return false;
            }
        }

        private bool GenerateAuthcode(int uId, int prodid)
        {
            try
            {
                string authcode = GenerateRandomString(4) + GenerateRandomNo() + DateTime.Now.Millisecond.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    //Insert prodid, authcode, date and status in tblAuthcode table
                    string strcmd = "INSERT INTO tblAuthcode(prodid,authcode,date,status) VALUES (@prodid,@authcode,@date,@status)";

                    SqlCommand cmd = new SqlCommand(strcmd, con);

                    cmd.Parameters.AddWithValue("@prodid", prodid);

                    cmd.Parameters.AddWithValue("@authcode", authcode);

                    cmd.Parameters.AddWithValue("@date", DateTime.Now);

                    cmd.Parameters.AddWithValue("@status", "APR");

                    con.Open();

                    int intEffectedRows = cmd.ExecuteNonQuery();

                    if (intEffectedRows > 0)

                    {
                        //Insert uId,prodid,authcode,Status and dateofpurchase in tblUserProductMaster table
                        string strcmd1 = "INSERT INTO tblUserProductMaster(uId,prodid,authcode,Status,dateofpurchase) VALUES (@uId,@prodid,@authcode,@Status,@dateofpurchase)";

                        cmd = new SqlCommand(strcmd1, con);

                        cmd.Parameters.AddWithValue("@uId", uId);

                        cmd.Parameters.AddWithValue("@prodid", prodid);

                        cmd.Parameters.AddWithValue("@authcode", authcode);

                        cmd.Parameters.AddWithValue("@Status", "APR");

                        cmd.Parameters.AddWithValue("@dateofpurchase", DateTime.Now);

                        int i = cmd.ExecuteNonQuery();

                        if (i > 0)
                            return true;
                        else
                            return false;
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
                return false;
            }
        }

        private string GenerateRandomString(int size)

        {
            try
            {
                Random random = new Random((int)DateTime.Now.Ticks);

                StringBuilder builder = new StringBuilder();

                char ch;

                for (int i = 0; i < size; i++)

                {
                    ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));

                    builder.Append(ch);
                }

                return builder.ToString().ToUpper();
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
        public string GetProductMasterCompleteCount(int Uid)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string msg = "";
                    int cnt = 0;
                    int Prodid = 7;
                    SqlCommand cmd = new SqlCommand("SP_ViewCount_PDTest", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Type", "USER_PRODUCTMASTER_COMPLETE_CNT");
                    cmd.Parameters.AddWithValue("@Uid", Uid);
                    cmd.Parameters.AddWithValue("@Prodid", Prodid);
                    con.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            cnt = Convert.ToInt32(sdr["ID_CNT"]);
                        }
                        sdr.Close();
                        if (cnt == 0)
                        {
                            return msg = "Test Not Complete";
                        }
                        else
                        {
                            return msg = "Test Complete";
                        }
                    }
                    else
                        return msg = null;
                }

            }
            catch (Exception ex)
            {
                Log.Error("" + ex);
                return "";
            }
        }


        //User PD Test answers submit 24 
        public Boolean SubmitPDTest(int c_id, PDAnswers[] pdAnswers)
        {
            Boolean flag = false;
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    //PD Test Answer Table
                    DataTable answertbl = new DataTable();
                    answertbl.Columns.Add("c_id", typeof(int));
                    answertbl.Columns.Add("q_id", typeof(int));
                    answertbl.Columns.Add("most_qno", typeof(int));
                    answertbl.Columns.Add("least_qno", typeof(int));
                    answertbl.Columns.Add("most_code", typeof(string));
                    answertbl.Columns.Add("least_code", typeof(string));
                    answertbl.Columns.Add("most_status", typeof(string));
                    answertbl.Columns.Add("least_status", typeof(string));
                    answertbl.Columns.Add("batid", typeof(int));

                    for (int j = 0; j < pdAnswers.Length; j++)
                    {
                        int q_id = Convert.ToInt32(pdAnswers[j].qid);
                        int most_qid = Convert.ToInt32(pdAnswers[j].Mostqno);
                        int least_qid = Convert.ToInt32(pdAnswers[j].Leastqno);
                        string most_code = pdAnswers[j].MostCode;
                        string least_code = pdAnswers[j].LeastCode;
                        string most_status = pdAnswers[j].MostStatus;
                        string least_status = pdAnswers[j].LeastStatus;
                        int batid = Convert.ToInt32(pdAnswers[j].batid);

                        answertbl.Rows.Add(c_id, q_id, most_qid, least_qid, most_code, least_code, most_status, least_status, batid);
                    }
                    //old comment on 17-7-2020 by bhagyashri
                    //SqlCommand cmd = new SqlCommand("SELECT * FROM tblPersonalityCandAnswers where c_id=" + c_id + " and batid=" + pdAnswers[0].batid, con);
                    //con.Open();
                    //SqlDataReader sdr = cmd.ExecuteReader();
                    //if (sdr.HasRows)
                    //{
                    //    sdr.Close();
                    //    Log.Info("PD Test is already complete..!!");
                    //    flag = true;
                    //}
                    //else
                    //{
                    //    sdr.Close();
                    //    if (answertbl.Rows.Count == 24)
                    //    {
                    //        using (var bulkcopy = new SqlBulkCopy(connectionString, SqlBulkCopyOptions.KeepIdentity))
                    //        {
                    //            foreach (DataColumn dc in answertbl.Columns)
                    //            {
                    //                bulkcopy.ColumnMappings.Add(dc.ColumnName, dc.ColumnName);
                    //            }
                    //            bulkcopy.BulkCopyTimeout = 100000;
                    //            bulkcopy.DestinationTableName = "tblPersonalityCandAnswers";
                    //            bulkcopy.WriteToServer(answertbl);
                    //        }

                    //        // insert user test status
                    //        UserTestStatus userTestStatus = new UserTestStatus();      
                    //        userTestStatus.uId = c_id;
                    //        userTestStatus.testid = 10;
                    //        userTestStatus.batid = Convert.ToInt32(pdAnswers[0].batid);
                    //        userTestStatus.testStatus = "Complete";
                    //        userTestStatus.factorStatus = "Complete";
                    //        userTestStatus.dataofcomplete = DateTime.Now;
                    //        if (SubmitUserTestStatus(userTestStatus))
                    //            flag = true;
                    //    }
                    //} //old comment complete
                    //New code by bhagyashri
                    #region New code
                    SqlCommand cmd = new SqlCommand("SELECT count(id)as CandAnsCnt FROM tblPersonalityCandAnswers where c_id=" + c_id + " and batid=" + pdAnswers[0].batid, con);
                    con.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        int CandAnsCnt = 0;
                        while (sdr.Read())
                        {
                            CandAnsCnt = Convert.ToInt32(sdr["CandAnsCnt"]);
                        }
                        if (CandAnsCnt == 24)
                        {
                            Log.Info("PD Test is already complete..!!");
                            sdr.Close();
                            flag = true;
                        }

                        else
                        {
                            if (answertbl.Rows.Count < 24)
                            {
                                using (var bulkcopy = new SqlBulkCopy(connectionString, SqlBulkCopyOptions.KeepIdentity))
                                {
                                    foreach (DataColumn dc in answertbl.Columns)
                                    {
                                        bulkcopy.ColumnMappings.Add(dc.ColumnName, dc.ColumnName);
                                    }
                                    bulkcopy.BulkCopyTimeout = 100000;
                                    bulkcopy.DestinationTableName = "tblPersonalityCandAnswers";
                                    bulkcopy.WriteToServer(answertbl);
                                }
                                flag = true;
                            }
                            else if (answertbl.Rows.Count == 24)
                            {
                                using (var bulkcopy = new SqlBulkCopy(connectionString, SqlBulkCopyOptions.KeepIdentity))
                                {
                                    foreach (DataColumn dc in answertbl.Columns)
                                    {
                                        bulkcopy.ColumnMappings.Add(dc.ColumnName, dc.ColumnName);
                                    }
                                    bulkcopy.BulkCopyTimeout = 100000;
                                    bulkcopy.DestinationTableName = "tblPersonalityCandAnswers";
                                    bulkcopy.WriteToServer(answertbl);
                                }

                                // insert user test status
                                UserTestStatus userTestStatus = new UserTestStatus();
                                userTestStatus.uId = c_id;
                                userTestStatus.testid = 10;
                                userTestStatus.batid = Convert.ToInt32(pdAnswers[0].batid);
                                userTestStatus.testStatus = "Complete";
                                userTestStatus.factorStatus = "Complete";
                                userTestStatus.dataofcomplete = DateTime.Now;
                                if (SubmitUserTestStatus(userTestStatus))
                                    flag = true;
                            }
                        }
                    }
                    else
                    {
                        if (answertbl.Rows.Count <= 24)
                        {
                            using (var bulkcopy = new SqlBulkCopy(connectionString, SqlBulkCopyOptions.KeepIdentity))
                            {
                                foreach (DataColumn dc in answertbl.Columns)
                                {
                                    bulkcopy.ColumnMappings.Add(dc.ColumnName, dc.ColumnName);
                                }
                                bulkcopy.BulkCopyTimeout = 100000;
                                bulkcopy.DestinationTableName = "tblPersonalityCandAnswers";
                                bulkcopy.WriteToServer(answertbl);
                            }

                            // insert user test status
                            UserTestStatus userTestStatus = new UserTestStatus();
                            userTestStatus.uId = c_id;
                            userTestStatus.testid = 10;
                            userTestStatus.batid = Convert.ToInt32(pdAnswers[0].batid);
                            userTestStatus.testStatus = "Complete";
                            userTestStatus.factorStatus = "Complete";
                            userTestStatus.dataofcomplete = DateTime.Now;
                            if (SubmitUserTestStatus(userTestStatus))
                                flag = true;
                        }
                    }
                    #endregion
                }
                return flag;

            }
            catch (Exception ex)
            {
                Log.Error("" + ex);
                return flag;
            }

        }

        // submit user test status after test complete
        private Boolean SubmitUserTestStatus(UserTestStatus userTestStatus)
        {
            Boolean flag = false;
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd1 = new SqlCommand("SELECT count(id)as CandAnsCnt FROM tblPersonalityCandAnswers where c_id=" + userTestStatus.uId + " and batid=" + userTestStatus.batid, con);
                    con.Open();
                    SqlDataReader sdr = cmd1.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        int CandAnsCnt = 0;
                        while (sdr.Read())
                        {
                            CandAnsCnt = Convert.ToInt32(sdr["CandAnsCnt"]);
                        }
                        con.Close();
                        if (CandAnsCnt == 24)
                        {
                            con.Open();
                            int intEffectedRows = 0;
                            SqlDataAdapter getstatus = new SqlDataAdapter("SELECT * FROM tblUserTestMaster WHERE batid=" + userTestStatus.batid + " and uId = " + userTestStatus.uId + " and testid=" + userTestStatus.testid, con);
                            DataSet dsstatus = new DataSet();
                            getstatus.Fill(dsstatus);
                            if (dsstatus.Tables[0].Rows.Count == 0)
                            {
                                string strcmd = "INSERT INTO tblUserTestMaster(uId,testid,batid,testStatus,factorStatus,dateofcomplete) VALUES (@uId,@testid,@batid,@testStatus,@factorStatus,@dateofcomplete)";
                                SqlCommand cmd = new SqlCommand(strcmd, con);
                                cmd.Parameters.AddWithValue("@uId", userTestStatus.uId);
                                cmd.Parameters.AddWithValue("@testid", userTestStatus.testid);
                                cmd.Parameters.AddWithValue("@batid", userTestStatus.batid);
                                cmd.Parameters.AddWithValue("@testStatus", userTestStatus.testStatus);
                                cmd.Parameters.AddWithValue("@factorStatus", userTestStatus.factorStatus);
                                cmd.Parameters.AddWithValue("@dateofcomplete", DateTime.Now);
                                intEffectedRows = cmd.ExecuteNonQuery();
                                flag = true;
                            }
                            else
                            {
                                string strcmd = "update tblUserTestMaster set testStatus=@testStatus,factorStatus=@factorStatus,dateofcomplete=@dateofcomplete where uId=@uId and testid=@testid and batid=@batid";
                                SqlCommand cmd = new SqlCommand(strcmd, con);
                                cmd.Parameters.AddWithValue("@uId", userTestStatus.uId);
                                cmd.Parameters.AddWithValue("@testid", userTestStatus.testid);
                                cmd.Parameters.AddWithValue("@batid", userTestStatus.batid);
                                cmd.Parameters.AddWithValue("@testStatus", userTestStatus.testStatus);
                                cmd.Parameters.AddWithValue("@factorStatus", userTestStatus.factorStatus);
                                cmd.Parameters.AddWithValue("@dateofcomplete", DateTime.Now);
                                intEffectedRows = cmd.ExecuteNonQuery();
                                flag = true;
                            }
                            con.Close();
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

        public List<PDQuestionsByUser> GetPDQuestionByUserID(int uid, int langid)
        {
            try
            {
                int testid = 10;
                int batid = 3;
                int total_answers = 0;
                int qid = 0;
                bool flag = false;
                List<PDQuestionsByUser> questions = new List<PDQuestionsByUser>();
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string QueryData = "select count(c_id) as total_answers from tblPersonalityCandAnswers where c_id = " + uid + " and batid=" + batid + "";
                    SqlCommand cmd = new SqlCommand(QueryData, con);
                    DataSet ds = new DataSet();
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);



                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        total_answers = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                    }
                    ds.Clear();
                    ds.Dispose();
                    con.Close();
                    // if candidate is giving the test for the first time
                    if (total_answers == 0)
                    {
                        if (qid == 0)
                        {
                            qid = 1;
                        }
                    }
                    else
                    {
                        // if session expires or candidate log out then resume the test when he log in again 
                        if (total_answers < 24)
                        {
                            QueryData = "";
                            QueryData = "select q_id from tblPersonalityCandAnswers where c_id= " + uid + "  and batid=" + batid + " order by q_id desc";
                            SqlCommand cmd1 = new SqlCommand(QueryData, con);
                            DataSet ds1 = new DataSet();
                            con.Open();
                            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                            da1.Fill(ds1);



                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                // HwordId.Value = (Convert.ToInt32(ds.Tables[0].Rows[0][10]) + 4).ToString();
                                qid = (Convert.ToInt32(ds1.Tables[0].Rows[0][0]) + 1);
                            }
                            ds1.Clear();
                            ds1.Dispose();
                            con.Close();
                        }
                        else
                        {
                            PDQuestionsByUser obj = new PDQuestionsByUser();
                            UserTestStatus userTestStatus = new UserTestStatus();
                            userTestStatus.batid = batid;
                            userTestStatus.testid = testid;
                            userTestStatus.uId = uid;
                            userTestStatus.testStatus = "Complete";
                            userTestStatus.factorStatus = "Complete";
                            if (SubmitUserTestStatus(userTestStatus))
                                flag = true;
                            obj.CompleteFlag = flag;
                            questions.Add(obj);
                            return questions;
                        }
                    }
                    if (Convert.ToInt32(qid) > 24)
                    {
                        PDQuestionsByUser obj = new PDQuestionsByUser();
                        UserTestStatus userTestStatus = new UserTestStatus();
                        userTestStatus.batid = batid;
                        userTestStatus.testid = testid;
                        userTestStatus.uId = uid;
                        userTestStatus.testStatus = "Complete";
                        userTestStatus.factorStatus = "Complete";
                        if (SubmitUserTestStatus(userTestStatus))
                            flag = true;
                        obj.CompleteFlag = flag;
                        questions.Add(obj);
                        return questions;
                    }
                    else
                    {
                        QueryData = "";
                        QueryData = "SELECT qno,questext,M as Most,L as Least,Status,q_id from tblPersonalityQuestions where q_id=" + qid + " and langid=" + langid + "";
                        SqlCommand cmd2 = new SqlCommand(QueryData, con);
                        DataSet ds2 = new DataSet();
                        con.Open();
                        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                        da2.Fill(ds2);
                        if (ds2.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
                            {
                                PDQuestionsByUser question = new PDQuestionsByUser();
                                question.qno = Convert.ToInt32(ds2.Tables[0].Rows[i]["qno"]);
                                question.questext = ds2.Tables[0].Rows[i]["questext"].ToString();
                                question.Most = ds2.Tables[0].Rows[i]["Most"].ToString();
                                question.Least = ds2.Tables[0].Rows[i]["Least"].ToString();
                                question.Status = ds2.Tables[0].Rows[i]["Status"].ToString();
                                question.q_id = Convert.ToInt32(ds2.Tables[0].Rows[i]["q_id"]);
                                question.CompleteFlag = false;
                                questions.Add(question);
                            }
                            return questions;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return null;
            }
        }

        public List<TestStatus> GetTestStatus(int uid, int langid)
        {
            int batid = 3, KYtestid = 9, PDtestid = 10, countKY = 0, countPD = 0;
            string sts_KY = "", sts_PD = "";
            List<TestStatus> lst = new List<TestStatus>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                #region individual_KY_test
                TestStatus obj = new TestStatus();
                string strQuery = "SELECT count(id) as cnt FROM tblUserTestMaster WHERE batid=" + batid + " and uId = " + uid + " and testid=" + KYtestid + " and testStatus='Complete' and factorStatus='Complete'";
                SqlCommand cmd = new SqlCommand(strQuery, con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        countKY = Convert.ToInt32(dr["cnt"]);
                    }
                }
                con.Close();
                if (countKY != 0)
                {
                    if (langid == 2)
                    {
                        sts_KY = "संपन्न";
                    }
                    else if (langid == 3)
                    {
                        sts_KY = "समाप्त ";
                    }
                    else if (langid == 4)
                    {
                        sts_KY = "પૂર્ણ";
                    }
                    else
                    {
                        sts_KY = "Completed";
                    }



                }
                else
                {
                    sts_KY = "Not Complete";
                }
                //  Msg = sts_KY;
                obj.KYStatus = sts_KY;
                #endregion



                #region individual_PD_test_sts




                strQuery = "SELECT count(id) as cnt FROM tblUserTestMaster WHERE batid=" + batid + " and uId = " + uid + " and testid=" + PDtestid + " and testStatus='Complete' and factorStatus='Complete'";
                SqlCommand cmd1 = new SqlCommand(strQuery, con);
                con.Open();
                SqlDataReader dr1 = cmd1.ExecuteReader();
                if (dr1.HasRows)
                {
                    while (dr1.Read())
                    {
                        countPD = Convert.ToInt32(dr1["cnt"]);
                    }
                }
                con.Close();
                if (countPD != 0)
                {
                    if (langid == 2)
                    {
                        sts_PD = "संपन्न";
                    }
                    else if (langid == 3)
                    {
                        sts_PD = "समाप्त ";
                    }
                    else if (langid == 4)
                    {
                        sts_PD = "પૂર્ણ";
                    }
                    else
                    {
                        sts_PD = "Completed";
                    }



                }
                else
                {
                    sts_PD = "Not Complete";
                }
                // Msg = sts_PD;
                obj.PDStatus = sts_PD;
                #endregion



                lst.Add(obj);
            }
            return lst;
        }

    }
}