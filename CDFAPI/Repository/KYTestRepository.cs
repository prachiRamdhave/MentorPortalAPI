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
    public class KYTestRepository
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        DataContex DC = new DataContex();
        //Get all KY Test Question 90 Question
        public List<KYQuestions> GetKYQuestion()
        {
            try
            {
                List<KYQuestions> questions = new List<KYQuestions>();
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT qno,questext,op1,op2,op3,factor_no,opblue,opred FROM tblKYQuestions where langid=1 ORDER BY NEWID()", con);
                    con.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            KYQuestions question = new KYQuestions();
                            question.qno = Convert.ToInt32(sdr["qno"]);
                            question.questext = sdr["questext"].ToString();
                            question.op1 = sdr["op1"].ToString();
                            question.op2 = sdr["op2"].ToString();
                            question.op3 = sdr["op3"].ToString();
                            question.opblue = Convert.ToInt32(sdr["opblue"].ToString());
                            question.opred = Convert.ToInt32(sdr["opred"].ToString());
                            question.factorno = Convert.ToInt32(sdr["factor_no"]);
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

        //Get all KY Test Question By language 90 Question
        public List<KYQuestions> GetKYQuestionByLang(int langid)
        {
            try
            {
                List<KYQuestions> questions = new List<KYQuestions>();
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT qno,questext,op1,op2,op3,factor_no,opblue,opred FROM tblKYQuestions where langid=" + langid + "  ORDER BY NEWID()", con);

                    con.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            KYQuestions question = new KYQuestions();
                            question.qno = Convert.ToInt32(sdr["qno"]);
                            question.questext = sdr["questext"].ToString();
                            question.op1 = sdr["op1"].ToString();
                            question.op2 = sdr["op2"].ToString();
                            question.op3 = sdr["op3"].ToString();
                            question.opblue = Convert.ToInt32(sdr["opblue"].ToString());
                            question.opred = Convert.ToInt32(sdr["opred"].ToString());
                            question.factorno = Convert.ToInt32(sdr["factor_no"]);
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

        //public List<KYQuestionsByUser> GetKYQuestionsByUserID(int uid, int langid)
        //{
        //    int Qno = 0;
        //    try
        //    {
        //        using (SqlConnection con = new SqlConnection(connectionString))
        //        {
        //            List<KYQuestionsByUser> lst = new List<KYQuestionsByUser>();
        //            int maxQue = 0;
        //            string strmax = "select isnull (max(q_no),0) as qno from tblKYCandAnswers where c_id=" + uid + " and batid=3";
        //            SqlCommand cmd = new SqlCommand(strmax, con);
        //            con.Open();
        //            SqlDataReader dr = cmd.ExecuteReader();
        //            if (dr.HasRows)
        //            {
        //                while (dr.Read())
        //                {
        //                    maxQue = Convert.ToInt32(dr["qno"]);
        //                }
        //                con.Close();
        //                maxQue++;
        //            }
        //            Qno = maxQue;

        //            SqlDataAdapter getfactor = new SqlDataAdapter("SELECT count(c_id)as Cnt_Factor FROM tblKYCandFactors WHERE batid= 3 and c_id = " + uid, con);
        //            DataSet dsfactor = new DataSet();
        //            getfactor.Fill(dsfactor);

        //            if (Convert.ToInt32(Qno) >= 90)
        //            {
        //                if (generateKYTestFactor(uid, 3, 9))
        //                {
        //                    KYQuestionsByUser obj = new KYQuestionsByUser();
        //                    obj.CompleteFlag = true;
        //                    lst.Add(obj);
        //                    return lst;
        //                }
        //            }
        //            else if (dsfactor != null && dsfactor.Tables[0].Rows.Count > 0)
        //            {
        //                int cnt = Convert.ToInt32(dsfactor.Tables[0].Rows[0]["Cnt_Factor"]);
        //                if (cnt == 9)
        //                {
        //                    KYQuestionsByUser obj = new KYQuestionsByUser();
        //                    obj.CompleteFlag = true;
        //                    lst.Add(obj);
        //                    return lst;
        //                }
        //                else
        //                {
        //                    string str = "select qno,factor_no,questext,op1,op2,op3,opblue,opred from tblKYQuestions " +
        //                           "where qno >= " + Qno + " AND qno <= (" + Qno + " + 9) and langid =" + langid;

        //                    SqlDataAdapter da = new SqlDataAdapter(str, con);
        //                    DataSet ds = new DataSet();
        //                    da.Fill(ds);

        //                    if (ds != null && ds.Tables[0].Rows.Count > 0)
        //                    {
        //                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //                        {
        //                            KYQuestionsByUser obj = new KYQuestionsByUser();
        //                            obj.qno = Convert.ToInt32(ds.Tables[0].Rows[i]["qno"]);
        //                            obj.questext = ds.Tables[0].Rows[i]["questext"].ToString();
        //                            obj.factorno = Convert.ToInt32(ds.Tables[0].Rows[i]["factor_no"]);
        //                            obj.op1 = ds.Tables[0].Rows[i]["op1"].ToString();
        //                            obj.op2 = ds.Tables[0].Rows[i]["op2"].ToString();
        //                            obj.op3 = ds.Tables[0].Rows[i]["op3"].ToString();
        //                            obj.opblue = Convert.ToInt32(ds.Tables[0].Rows[i]["opblue"]);
        //                            obj.opred = Convert.ToInt32(ds.Tables[0].Rows[i]["opred"]);
        //                            obj.CompleteFlag = false;
        //                            lst.Add(obj);
        //                        }
        //                        return lst;
        //                    }
        //                    else
        //                    {
        //                        return null;
        //                    }
        //                }
        //            }

        //            return lst;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error(ex);
        //        return null;
        //    }
        //}

        //public Boolean generateKYTestFactor(int c_id, int batid, int testid)
        //{
        //    Boolean flag = false;
        //    try
        //    {
        //        using (SqlConnection con = new SqlConnection(connectionString))
        //        {

        //            SqlDataAdapter getans = new SqlDataAdapter("SELECT c_id FROM tblKYCandAnswers WHERE batid=" + batid + " and  c_id = " + c_id, con);
        //            DataSet dsans = new DataSet();
        //            getans.Fill(dsans);

        //            SqlDataAdapter getfactor = new SqlDataAdapter("SELECT c_id FROM tblKYCandFactors WHERE batid=" + batid + " and c_id = " + c_id, con);
        //            DataSet dsfactor = new DataSet();
        //            getfactor.Fill(dsfactor);

        //            if (dsfactor.Tables[0].Rows.Count == 0 && dsans.Tables[0].Rows.Count == 90)
        //            {
        //                //KY factor Table
        //                DataTable factortbl = new DataTable();
        //                factortbl.Columns.Add("c_id", typeof(int));
        //                factortbl.Columns.Add("factor_no", typeof(int));
        //                factortbl.Columns.Add("score", typeof(int));
        //                factortbl.Columns.Add("rating", typeof(string));
        //                factortbl.Columns.Add("P_rating", typeof(float));
        //                factortbl.Columns.Add("measure", typeof(string));
        //                factortbl.Columns.Add("New_P_rating", typeof(float));
        //                factortbl.Columns.Add("batid", typeof(int));

        //                //get All Factors
        //                SqlDataAdapter getFactors = new SqlDataAdapter("SELECT * FROM tblKYFactors", con);
        //                DataSet dsfactors = new DataSet();
        //                getFactors.Fill(dsfactors);

        //                //get All norming
        //                SqlDataAdapter getnorming = new SqlDataAdapter("Select * FROM tblPersonalityNorming", con);
        //                DataSet dsnorming = new DataSet();
        //                getnorming.Fill(dsnorming);

        //                //get All Factors                       
        //                SqlDataAdapter getRating = new SqlDataAdapter("Select * FROM tblPersonalityRating", con);
        //                DataSet dsRating = new DataSet();
        //                getRating.Fill(dsRating);

        //                //get candidate details                       
        //                SqlDataAdapter getCanddetails = new SqlDataAdapter("Select * FROM tblUserMaster where uId=" + c_id, con);
        //                DataSet dscandDetails = new DataSet();
        //                getCanddetails.Fill(dscandDetails);

        //                int age = DateTime.Now.Year - Convert.ToDateTime(dscandDetails.Tables[0].Rows[0]["dob"]).Year;
        //                string gender = dscandDetails.Tables[0].Rows[0]["gender"].ToString();

        //                //get count of factors from tblKYCandAnswers
        //                for (int fno = 1; fno <= 9; fno++)
        //                {
        //                    #region counting_factor
        //                    SqlDataAdapter getfactormarks = new SqlDataAdapter("SELECT sum(A.marks) , B.factor FROM tblKYCandAnswers as A, tblKYFactors as B WHERE A.batid=" + batid + " AND A.c_id = " + c_id + " AND A.factor_no =" + fno + " AND A.factor_no = B.factor_no group by B.factor", con);
        //                    DataSet dscandmarks = new DataSet();
        //                    getfactormarks.Fill(dscandmarks);

        //                    int score = 0;
        //                    string factor = "";

        //                    if (dscandmarks.Tables[0].Rows.Count > 0)
        //                    {
        //                        score = Convert.ToInt32(dscandmarks.Tables[0].Rows[0][0].ToString());
        //                        factor = dscandmarks.Tables[0].Rows[0][1].ToString();
        //                    }
        //                    dscandmarks.Clear();
        //                    dscandmarks.Dispose();

        //                    double P_rating = 0.0;
        //                    string rating = "";

        //                    if ((age >= 15 && age <= 21) || (age >= 30 && age <= 46)) //15-21 and 30-46
        //                    {
        //                        #region age_b/w_15-21 and 30-46

        //                        DataRow[] PRatings = dsnorming.Tables[0].Select("factor_no = '" + factor + "' AND Gender ='" + gender + "' AND Age =" + age);


        //                        if (PRatings.Length > 0 && PRatings[0][4 + score].ToString() != null)
        //                        {
        //                            P_rating = Convert.ToDouble(PRatings[0][4 + score].ToString());
        //                        }

        //                        DataRow[] ratings = dsRating.Tables[0].Select("factor_no = '" + factor + "' AND Gender ='" + gender + "' AND Age =" + age);


        //                        if (ratings.Length > 0 && ratings[0][4 + score].ToString() != null)
        //                        {
        //                            rating = ratings[0][4 + score].ToString();
        //                        }


        //                        #endregion
        //                    }
        //                    else if (age >= 22 && age <= 29)//age is >=22 and <= 29
        //                    {
        //                        #region age_>=22 and <= 29

        //                        DataRow[] PRatings = dsnorming.Tables[0].Select("factor_no = '" + factor + "' AND Gender ='" + gender + "' AND Age =30");

        //                        if (PRatings.Length > 0 && PRatings[0][4 + score].ToString() != null)
        //                        {
        //                            P_rating = Convert.ToDouble(PRatings[0][4 + score].ToString());
        //                        }

        //                        DataRow[] ratings = dsRating.Tables[0].Select("factor_no = '" + factor + "' AND Gender ='" + gender + "' AND Age =30");

        //                        if (ratings.Length > 0 && ratings[0][4 + score].ToString() != null)
        //                        {
        //                            rating = ratings[0][4 + score].ToString();
        //                        }

        //                        #endregion
        //                    }
        //                    else if (age < 15) //age is less than 15
        //                    {
        //                        #region age_less_15

        //                        DataRow[] PRatings = dsnorming.Tables[0].Select("factor_no = '" + factor + "' AND Gender ='" + gender + "' AND Age =15");

        //                        if (PRatings.Length > 0 && PRatings[0][4 + score].ToString() != null)
        //                        {
        //                            P_rating = Convert.ToDouble(PRatings[0][4 + score].ToString());
        //                        }

        //                        DataRow[] ratings = dsRating.Tables[0].Select("factor_no = '" + factor + "' AND Gender ='" + gender + "' AND Age =15");


        //                        if (ratings.Length > 0 && ratings[0][4 + score].ToString() != null)
        //                        {
        //                            rating = ratings[0][4 + score].ToString();
        //                        }


        //                        #endregion
        //                    }
        //                    else if (age > 46) //age is grater than 46
        //                    {
        //                        #region age_grater_46

        //                        DataRow[] PRatings = dsnorming.Tables[0].Select("factor_no = '" + factor + "' AND Gender ='" + gender + "' AND Age =46");

        //                        if (PRatings.Length > 0 && PRatings[0][4 + score].ToString() != null)
        //                        {
        //                            P_rating = Convert.ToDouble(PRatings[0][4 + score].ToString());
        //                        }

        //                        DataRow[] ratings = dsRating.Tables[0].Select("factor_no = '" + factor + "' AND Gender ='" + gender + "' AND Age =46");


        //                        if (ratings.Length > 0 && ratings[0][4 + score].ToString() != null)
        //                        {
        //                            rating = ratings[0][4 + score].ToString();
        //                        }


        //                        #endregion
        //                    }



        //                    double New_P_rating = 0.0;
        //                    string measure = "";
        //                    if (P_rating <= 50)
        //                    {
        //                        measure = "Low";
        //                        New_P_rating = (50.00 - P_rating) * 2;
        //                    }
        //                    else
        //                    {
        //                        measure = "High";
        //                        New_P_rating = (P_rating - 50.00) * 2;
        //                    }

        //                    factortbl.Rows.Add(c_id, fno, score, rating, P_rating, measure, New_P_rating, batid);

        //                    #endregion
        //                }

        //                if (factortbl.Rows.Count == 9)
        //                {
        //                    string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        //                    using (var bulkcopy = new SqlBulkCopy(connectionString, SqlBulkCopyOptions.KeepIdentity))
        //                    {
        //                        foreach (DataColumn dc in factortbl.Columns)
        //                        {
        //                            bulkcopy.ColumnMappings.Add(dc.ColumnName, dc.ColumnName);
        //                        }
        //                        bulkcopy.BulkCopyTimeout = 100000;
        //                        bulkcopy.DestinationTableName = "tblKYCandFactors";
        //                        bulkcopy.WriteToServer(factortbl);
        //                    }
        //                }

        //                //get top 3 personalities by New_p_rating
        //                SqlDataAdapter gettop3 = new SqlDataAdapter("Select top 3 A.New_P_rating, B.factor, A.measure FROM tblKYCandFactors A,tblKYFactors B WHERE batid=" + batid + " and c_id =" + c_id + " AND A.factor_no = B.factor_no order by New_P_rating desc", con);
        //                DataSet dstop3 = new DataSet();
        //                gettop3.Fill(dstop3);

        //                //get top 3 personalities by p_rating
        //                SqlDataAdapter gettop3p = new SqlDataAdapter("Select top 3 A.P_rating, B.factor FROM tblKYCandFactors A,tblKYFactors B WHERE batid=" + batid + " and c_id =" + c_id + " AND A.factor_no = B.factor_no order by P_rating desc", con);
        //                DataSet dstop3p = new DataSet();
        //                gettop3p.Fill(dstop3p);

        //                if (dstop3.Tables[0].Rows.Count > 0 && dstop3p.Tables[0].Rows.Count > 0)
        //                {
        //                    SqlCommand cmdtop3 = null;
        //                    SqlDataAdapter strcmdchktop3 = new SqlDataAdapter("SELECT * FROM tblCandidateTop3 WHERE c_id = " + c_id, con);
        //                    DataSet dschktop3 = new DataSet();
        //                    strcmdchktop3.Fill(dschktop3);
        //                    if (con.State == ConnectionState.Closed)
        //                        con.Open();
        //                    if (dschktop3.Tables[0].Rows.Count == 0)
        //                        cmdtop3 = new SqlCommand("insert into tblCandidateTop3 values(" + c_id + ",'NULL','NULL','NULL','" + dstop3.Tables[0].Rows[0][1].ToString() + "-" + dstop3.Tables[0].Rows[0][2].ToString() + "','" + dstop3.Tables[0].Rows[1][1].ToString() + "-" + dstop3.Tables[0].Rows[1][2].ToString() + "','" + dstop3.Tables[0].Rows[2][1].ToString() + "-" + dstop3.Tables[0].Rows[2][2].ToString() + "','NULL','NULL','NULL','" + dstop3p.Tables[0].Rows[0][1].ToString() + "','" + dstop3p.Tables[0].Rows[1][1].ToString() + "','" + dstop3p.Tables[0].Rows[2][1].ToString() + "')", con);
        //                    else
        //                        cmdtop3 = new SqlCommand("update tblCandidateTop3 set  personality1='" + dstop3.Tables[0].Rows[0][1].ToString() + "-" + dstop3.Tables[0].Rows[0][2].ToString() + "',personality2='" + dstop3.Tables[0].Rows[1][1].ToString() + "-" + dstop3.Tables[0].Rows[1][2].ToString() + "',personality3='" + dstop3.Tables[0].Rows[2][1].ToString() + "-" + dstop3.Tables[0].Rows[2][2].ToString() + "',personality4='" + dstop3p.Tables[0].Rows[0][1].ToString() + "',personality5='" + dstop3p.Tables[0].Rows[1][1].ToString() + "',personality6='" + dstop3p.Tables[0].Rows[2][1].ToString() + "' where c_id=" + c_id, con);

        //                    int intEffectedRows = cmdtop3.ExecuteNonQuery();


        //                    SqlDataAdapter getstatus = new SqlDataAdapter("SELECT * FROM tblUserTestMaster WHERE batid=" + batid + " and uId = " + c_id + " and testid=" + testid, con);
        //                    DataSet dsstatus = new DataSet();
        //                    getstatus.Fill(dsstatus);
        //                    if (dsstatus.Tables[0].Rows.Count == 0)
        //                    {
        //                        string strcmd = "INSERT INTO tblUserTestMaster(uId,testid,batid,testStatus,factorStatus,dateofcomplete) VALUES (@uId,@testid,@batid,@testStatus,@factorStatus,@dateofcomplete)";
        //                        SqlCommand cmd = new SqlCommand(strcmd, con);
        //                        cmd.Parameters.AddWithValue("@uId", c_id);
        //                        cmd.Parameters.AddWithValue("@testid", testid);
        //                        cmd.Parameters.AddWithValue("@batid", batid);
        //                        cmd.Parameters.AddWithValue("@testStatus", "Complete");
        //                        cmd.Parameters.AddWithValue("@factorStatus", "Complete");
        //                        cmd.Parameters.AddWithValue("@dateofcomplete", DateTime.Now);
        //                        int EffectedRows = cmd.ExecuteNonQuery();
        //                    }
        //                    else
        //                    {
        //                        string strcmd = "update tblUserTestMaster set testStatus=@testStatus,factorStatus=@factorStatus,dateofcomplete=@dateofcomplete WHERE batid=" + batid + " and uId = " + c_id + " and testid=" + testid + "";
        //                        SqlCommand cmd = new SqlCommand(strcmd, con);
        //                        cmd.Parameters.AddWithValue("@testStatus", "Complete");
        //                        cmd.Parameters.AddWithValue("@factorStatus", "Complete");
        //                        cmd.Parameters.AddWithValue("@dateofcomplete", DateTime.Now);
        //                        int EffectedRows = cmd.ExecuteNonQuery();
        //                    }

        //                    getstatus.Dispose();
        //                    dsstatus.Dispose();

        //                    if (intEffectedRows > 0)
        //                    {
        //                        flag = true;
        //                    }
        //                }
        //            }
        //            return flag;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error("" + ex);
        //        return flag;
        //    }
        //}

        //User KY Test answers submit 90 

      

  public List<KYQuestionsByUser> GetKYQuestionsByUserID(int uid, int langid)
        {
            int Qno = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    List<KYQuestionsByUser> lst = new List<KYQuestionsByUser>();
                    int maxQue = 0;
                    string strmax = "select isnull (max(q_no),0) as qno from tblKYCandAnswers where c_id=" + uid + " and batid=3";
                    SqlCommand cmd = new SqlCommand(strmax, con);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            maxQue = Convert.ToInt32(dr["qno"]);
                        }
                        con.Close();
                        maxQue++;
                    }
                    Qno = maxQue;

                    SqlDataAdapter getfactor = new SqlDataAdapter("SELECT count(c_id)as Cnt_Factor FROM tblKYCandFactors WHERE batid= 3 and c_id = " + uid, con);
                    DataSet dsfactor = new DataSet();
                    getfactor.Fill(dsfactor);

                    if (Convert.ToInt32(Qno) >= 90)
                    {
                        if (generateKYTestFactor(uid, 3, 9))
                        {
                            KYQuestionsByUser obj = new KYQuestionsByUser();
                            obj.CompleteFlag = true;
                            lst.Add(obj);
                            return lst;
                        }
                        int cnt = Convert.ToInt32(dsfactor.Tables[0].Rows[0]["Cnt_Factor"]);
                        if (cnt == 9)
                        {
                            KYQuestionsByUser obj = new KYQuestionsByUser();
                            obj.CompleteFlag = true;
                            lst.Add(obj);
                            return lst;
                        }
                    }
                    else if (dsfactor != null && dsfactor.Tables[0].Rows.Count > 0)
                    {
                        int cnt = Convert.ToInt32(dsfactor.Tables[0].Rows[0]["Cnt_Factor"]);
                        if (cnt == 9)
                        {
                            KYQuestionsByUser obj = new KYQuestionsByUser();
                            obj.CompleteFlag = true;
                            lst.Add(obj);
                            return lst;
                        }
                        else
                        {
                            string str = "select qno,factor_no,questext,op1,op2,op3,opblue,opred from tblKYQuestions " +
                                   "where qno >= " + Qno + " AND qno <= (" + Qno + " + 9) and langid =" + langid;

                            SqlDataAdapter da = new SqlDataAdapter(str, con);
                            DataSet ds = new DataSet();
                            da.Fill(ds);

                            if (ds != null && ds.Tables[0].Rows.Count > 0)
                            {
                                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                {
                                    KYQuestionsByUser obj = new KYQuestionsByUser();
                                    obj.qno = Convert.ToInt32(ds.Tables[0].Rows[i]["qno"]);
                                    obj.questext = ds.Tables[0].Rows[i]["questext"].ToString();
                                    obj.factorno = Convert.ToInt32(ds.Tables[0].Rows[i]["factor_no"]);
                                    obj.op1 = ds.Tables[0].Rows[i]["op1"].ToString();
                                    obj.op2 = ds.Tables[0].Rows[i]["op2"].ToString();
                                    obj.op3 = ds.Tables[0].Rows[i]["op3"].ToString();
                                    obj.opblue = Convert.ToInt32(ds.Tables[0].Rows[i]["opblue"]);
                                    obj.opred = Convert.ToInt32(ds.Tables[0].Rows[i]["opred"]);
                                    obj.CompleteFlag = false;
                                    lst.Add(obj);
                                }
                                return lst;
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }

                    return lst;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return null;
            }
        }

        public Boolean generateKYTestFactor(int c_id, int batid, int testid)
        {
            Boolean flag = false;
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {

                    SqlDataAdapter getans = new SqlDataAdapter("SELECT c_id FROM tblKYCandAnswers WHERE batid=" + batid + " and  c_id = " + c_id, con);
                    DataSet dsans = new DataSet();
                    getans.Fill(dsans);

                    SqlDataAdapter getfactor = new SqlDataAdapter("SELECT c_id FROM tblKYCandFactors WHERE batid=" + batid + " and c_id = " + c_id, con);
                    DataSet dsfactor = new DataSet();
                    getfactor.Fill(dsfactor);

                    if (dsfactor.Tables[0].Rows.Count == 0 && dsans.Tables[0].Rows.Count == 90)
                    {
                        //KY factor Table
                        DataTable factortbl = new DataTable();
                        factortbl.Columns.Add("c_id", typeof(int));
                        factortbl.Columns.Add("factor_no", typeof(int));
                        factortbl.Columns.Add("score", typeof(int));
                        factortbl.Columns.Add("rating", typeof(string));
                        factortbl.Columns.Add("P_rating", typeof(float));
                        factortbl.Columns.Add("measure", typeof(string));
                        factortbl.Columns.Add("New_P_rating", typeof(float));
                        factortbl.Columns.Add("batid", typeof(int));

                        //get All Factors
                        SqlDataAdapter getFactors = new SqlDataAdapter("SELECT * FROM tblKYFactors", con);
                        DataSet dsfactors = new DataSet();
                        getFactors.Fill(dsfactors);

                        //get All norming
                        SqlDataAdapter getnorming = new SqlDataAdapter("Select * FROM tblPersonalityNorming", con);
                        DataSet dsnorming = new DataSet();
                        getnorming.Fill(dsnorming);

                        //get All Factors                       
                        SqlDataAdapter getRating = new SqlDataAdapter("Select * FROM tblPersonalityRating", con);
                        DataSet dsRating = new DataSet();
                        getRating.Fill(dsRating);

                        //get candidate details                       
                        SqlDataAdapter getCanddetails = new SqlDataAdapter("Select * FROM tblUserMaster where uId=" + c_id, con);
                        DataSet dscandDetails = new DataSet();
                        getCanddetails.Fill(dscandDetails);

                        int age = DateTime.Now.Year - Convert.ToDateTime(dscandDetails.Tables[0].Rows[0]["dob"]).Year;
                        string gender = dscandDetails.Tables[0].Rows[0]["gender"].ToString();

                        //get count of factors from tblKYCandAnswers
                        for (int fno = 1; fno <= 9; fno++)
                        {
                            #region counting_factor
                            SqlDataAdapter getfactormarks = new SqlDataAdapter("SELECT sum(A.marks) , B.factor FROM tblKYCandAnswers as A, tblKYFactors as B WHERE A.batid=" + batid + " AND A.c_id = " + c_id + " AND A.factor_no =" + fno + " AND A.factor_no = B.factor_no group by B.factor", con);
                            DataSet dscandmarks = new DataSet();
                            getfactormarks.Fill(dscandmarks);

                            int score = 0;
                            string factor = "";

                            if (dscandmarks.Tables[0].Rows.Count > 0)
                            {
                                score = Convert.ToInt32(dscandmarks.Tables[0].Rows[0][0].ToString());
                                factor = dscandmarks.Tables[0].Rows[0][1].ToString();
                            }
                            dscandmarks.Clear();
                            dscandmarks.Dispose();

                            double P_rating = 0.0;
                            string rating = "";

                            if ((age >= 15 && age <= 21) || (age >= 30 && age <= 46)) //15-21 and 30-46
                            {
                                #region age_b/w_15-21 and 30-46

                                DataRow[] PRatings = dsnorming.Tables[0].Select("factor_no = '" + factor + "' AND Gender ='" + gender + "' AND Age =" + age);


                                if (PRatings.Length > 0 && PRatings[0][4 + score].ToString() != null)
                                {
                                    P_rating = Convert.ToDouble(PRatings[0][4 + score].ToString());
                                }

                                DataRow[] ratings = dsRating.Tables[0].Select("factor_no = '" + factor + "' AND Gender ='" + gender + "' AND Age =" + age);


                                if (ratings.Length > 0 && ratings[0][4 + score].ToString() != null)
                                {
                                    rating = ratings[0][4 + score].ToString();
                                }


                                #endregion
                            }
                            else if (age >= 22 && age <= 29)//age is >=22 and <= 29
                            {
                                #region age_>=22 and <= 29

                                DataRow[] PRatings = dsnorming.Tables[0].Select("factor_no = '" + factor + "' AND Gender ='" + gender + "' AND Age =30");

                                if (PRatings.Length > 0 && PRatings[0][4 + score].ToString() != null)
                                {
                                    P_rating = Convert.ToDouble(PRatings[0][4 + score].ToString());
                                }

                                DataRow[] ratings = dsRating.Tables[0].Select("factor_no = '" + factor + "' AND Gender ='" + gender + "' AND Age =30");

                                if (ratings.Length > 0 && ratings[0][4 + score].ToString() != null)
                                {
                                    rating = ratings[0][4 + score].ToString();
                                }

                                #endregion
                            }
                            else if (age < 15) //age is less than 15
                            {
                                #region age_less_15

                                DataRow[] PRatings = dsnorming.Tables[0].Select("factor_no = '" + factor + "' AND Gender ='" + gender + "' AND Age =15");

                                if (PRatings.Length > 0 && PRatings[0][4 + score].ToString() != null)
                                {
                                    P_rating = Convert.ToDouble(PRatings[0][4 + score].ToString());
                                }

                                DataRow[] ratings = dsRating.Tables[0].Select("factor_no = '" + factor + "' AND Gender ='" + gender + "' AND Age =15");


                                if (ratings.Length > 0 && ratings[0][4 + score].ToString() != null)
                                {
                                    rating = ratings[0][4 + score].ToString();
                                }


                                #endregion
                            }
                            else if (age > 46) //age is grater than 46
                            {
                                #region age_grater_46

                                DataRow[] PRatings = dsnorming.Tables[0].Select("factor_no = '" + factor + "' AND Gender ='" + gender + "' AND Age =46");

                                if (PRatings.Length > 0 && PRatings[0][4 + score].ToString() != null)
                                {
                                    P_rating = Convert.ToDouble(PRatings[0][4 + score].ToString());
                                }

                                DataRow[] ratings = dsRating.Tables[0].Select("factor_no = '" + factor + "' AND Gender ='" + gender + "' AND Age =46");


                                if (ratings.Length > 0 && ratings[0][4 + score].ToString() != null)
                                {
                                    rating = ratings[0][4 + score].ToString();
                                }


                                #endregion
                            }



                            double New_P_rating = 0.0;
                            string measure = "";
                            if (P_rating <= 50)
                            {
                                measure = "Low";
                                New_P_rating = (50.00 - P_rating) * 2;
                            }
                            else
                            {
                                measure = "High";
                                New_P_rating = (P_rating - 50.00) * 2;
                            }

                            factortbl.Rows.Add(c_id, fno, score, rating, P_rating, measure, New_P_rating, batid);

                            #endregion
                        }

                        if (factortbl.Rows.Count == 9)
                        {
                            string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
                            using (var bulkcopy = new SqlBulkCopy(connectionString, SqlBulkCopyOptions.KeepIdentity))
                            {
                                foreach (DataColumn dc in factortbl.Columns)
                                {
                                    bulkcopy.ColumnMappings.Add(dc.ColumnName, dc.ColumnName);
                                }
                                bulkcopy.BulkCopyTimeout = 100000;
                                bulkcopy.DestinationTableName = "tblKYCandFactors";
                                bulkcopy.WriteToServer(factortbl);
                            }
                        }

                        //get top 3 personalities by New_p_rating
                        SqlDataAdapter gettop3 = new SqlDataAdapter("Select top 3 A.New_P_rating, B.factor, A.measure FROM tblKYCandFactors A,tblKYFactors B WHERE batid=" + batid + " and c_id =" + c_id + " AND A.factor_no = B.factor_no order by New_P_rating desc", con);
                        DataSet dstop3 = new DataSet();
                        gettop3.Fill(dstop3);

                        //get top 3 personalities by p_rating
                        SqlDataAdapter gettop3p = new SqlDataAdapter("Select top 3 A.P_rating, B.factor FROM tblKYCandFactors A,tblKYFactors B WHERE batid=" + batid + " and c_id =" + c_id + " AND A.factor_no = B.factor_no order by P_rating desc", con);
                        DataSet dstop3p = new DataSet();
                        gettop3p.Fill(dstop3p);

                        if (dstop3.Tables[0].Rows.Count > 0 && dstop3p.Tables[0].Rows.Count > 0)
                        {
                            SqlCommand cmdtop3 = null;
                            SqlDataAdapter strcmdchktop3 = new SqlDataAdapter("SELECT * FROM tblCandidateTop3 WHERE c_id = " + c_id, con);
                            DataSet dschktop3 = new DataSet();
                            strcmdchktop3.Fill(dschktop3);
                            if (con.State == ConnectionState.Closed)
                                con.Open();
                            if (dschktop3.Tables[0].Rows.Count == 0)
                                cmdtop3 = new SqlCommand("insert into tblCandidateTop3 values(" + c_id + ",'NULL','NULL','NULL','" + dstop3.Tables[0].Rows[0][1].ToString() + "-" + dstop3.Tables[0].Rows[0][2].ToString() + "','" + dstop3.Tables[0].Rows[1][1].ToString() + "-" + dstop3.Tables[0].Rows[1][2].ToString() + "','" + dstop3.Tables[0].Rows[2][1].ToString() + "-" + dstop3.Tables[0].Rows[2][2].ToString() + "','NULL','NULL','NULL','" + dstop3p.Tables[0].Rows[0][1].ToString() + "','" + dstop3p.Tables[0].Rows[1][1].ToString() + "','" + dstop3p.Tables[0].Rows[2][1].ToString() + "')", con);
                            else
                                cmdtop3 = new SqlCommand("update tblCandidateTop3 set  personality1='" + dstop3.Tables[0].Rows[0][1].ToString() + "-" + dstop3.Tables[0].Rows[0][2].ToString() + "',personality2='" + dstop3.Tables[0].Rows[1][1].ToString() + "-" + dstop3.Tables[0].Rows[1][2].ToString() + "',personality3='" + dstop3.Tables[0].Rows[2][1].ToString() + "-" + dstop3.Tables[0].Rows[2][2].ToString() + "',personality4='" + dstop3p.Tables[0].Rows[0][1].ToString() + "',personality5='" + dstop3p.Tables[0].Rows[1][1].ToString() + "',personality6='" + dstop3p.Tables[0].Rows[2][1].ToString() + "' where c_id=" + c_id, con);

                            int intEffectedRows = cmdtop3.ExecuteNonQuery();


                            SqlDataAdapter getstatus = new SqlDataAdapter("SELECT * FROM tblUserTestMaster WHERE batid=" + batid + " and uId = " + c_id + " and testid=" + testid, con);
                            DataSet dsstatus = new DataSet();
                            getstatus.Fill(dsstatus);
                            if (dsstatus.Tables[0].Rows.Count == 0)
                            {
                                string strcmd = "INSERT INTO tblUserTestMaster(uId,testid,batid,testStatus,factorStatus,dateofcomplete) VALUES (@uId,@testid,@batid,@testStatus,@factorStatus,@dateofcomplete)";
                                SqlCommand cmd = new SqlCommand(strcmd, con);
                                cmd.Parameters.AddWithValue("@uId", c_id);
                                cmd.Parameters.AddWithValue("@testid", testid);
                                cmd.Parameters.AddWithValue("@batid", batid);
                                cmd.Parameters.AddWithValue("@testStatus", "Complete");
                                cmd.Parameters.AddWithValue("@factorStatus", "Complete");
                                cmd.Parameters.AddWithValue("@dateofcomplete", DateTime.Now);
                                int EffectedRows = cmd.ExecuteNonQuery();
                            }
                            else
                            {
                                string strcmd = "update tblUserTestMaster set testStatus=@testStatus,factorStatus=@factorStatus,dateofcomplete=@dateofcomplete WHERE batid=" + batid + " and uId = " + c_id + " and testid=" + testid + "";
                                SqlCommand cmd = new SqlCommand(strcmd, con);
                                cmd.Parameters.AddWithValue("@testStatus", "Complete");
                                cmd.Parameters.AddWithValue("@factorStatus", "Complete");
                                cmd.Parameters.AddWithValue("@dateofcomplete", DateTime.Now);
                                int EffectedRows = cmd.ExecuteNonQuery();
                            }

                            getstatus.Dispose();
                            dsstatus.Dispose();

                            if (intEffectedRows > 0)
                            {
                                flag = true;
                            }
                        }
                    }
                    return flag;
                }
            }
            catch (Exception ex)
            {
                Log.Error("" + ex);
                return flag;
            }
        }
        //public Boolean SubmitKYTest(int uid, KYAnswers[] kyAnswers)
        //{
        //    int testid = 9;
        //    Boolean flag = false;
        //    int TotalAnsCnt = 0;
        //    try
        //    {
        //        using (SqlConnection con = new SqlConnection(connectionString))
        //        {
        //            con.Open();
        //            try
        //            {
        //                ////Interest Test Answer Table
        //                DataTable answertbl = new DataTable();
        //                answertbl.Columns.Add("c_id", typeof(int));
        //                answertbl.Columns.Add("q_no", typeof(int));
        //                answertbl.Columns.Add("factor_no", typeof(int));
        //                answertbl.Columns.Add("marks", typeof(int));
        //                answertbl.Columns.Add("batid", typeof(int));

        //                for (int j = 0; j < kyAnswers.Length; j++)
        //                {
        //                    int q_no = Convert.ToInt32(kyAnswers[j].qno);
        //                    int factor_no = Convert.ToInt32(kyAnswers[j].factorno);
        //                    int marks = Convert.ToInt32(kyAnswers[j].marks);
        //                    int batid = Convert.ToInt32(kyAnswers[j].batid);
        //                    answertbl.Rows.Add(uid, q_no, factor_no, marks, batid);
        //                }
        //                SqlCommand cmd = new SqlCommand("SELECT count(id) as cnt FROM tblKYCandAnswers where c_id=" + uid + " and batid=" + kyAnswers[0].batid, con);
        //                SqlDataReader sdr = cmd.ExecuteReader();
        //                if (sdr.HasRows)
        //                {
        //                    while (sdr.Read())
        //                    {
        //                        TotalAnsCnt = Convert.ToInt32(sdr["cnt"]);
        //                    }
        //                    sdr.Close();

        //                }
        //                string testStatus = "Incomplete";
        //                if (TotalAnsCnt == 90)
        //                {
        //                    Log.Info("KY Test is already complete..!!");
        //                    testStatus = "Complete";
        //                    flag = true;
        //                }
        //                else
        //                {
        //                    sdr.Close();
        //                    if (answertbl.Rows.Count <= 90)
        //                    {
        //                        using (var bulkcopy = new SqlBulkCopy(connectionString, SqlBulkCopyOptions.KeepIdentity))
        //                        {
        //                            foreach (DataColumn dc in answertbl.Columns)
        //                            {
        //                                bulkcopy.ColumnMappings.Add(dc.ColumnName, dc.ColumnName);
        //                            }
        //                            bulkcopy.BulkCopyTimeout = 100000;
        //                            bulkcopy.DestinationTableName = "tblKYCandAnswers";
        //                            bulkcopy.WriteToServer(answertbl);
        //                        }
        //                        testStatus = "Incomplete";
        //                    }
        //                    string factorStatus = "Incomplete";
        //                    if (generateKYTestFactor(uid, Convert.ToInt32(kyAnswers[0].batid), testid))
        //                    {
        //                        factorStatus = "Complete";
        //                    }
        //                    else
        //                    {
        //                        factorStatus = "Incomplete";
        //                    }

        //                    //insert user test status
        //                    UserTestStatus userTestStatus = new UserTestStatus();
        //                    userTestStatus.uId = uid;
        //                    userTestStatus.testid = 9;
        //                    userTestStatus.batid = Convert.ToInt32(kyAnswers[0].batid);
        //                    userTestStatus.testStatus = testStatus;
        //                    userTestStatus.factorStatus = factorStatus;
        //                    userTestStatus.dataofcomplete = DateTime.Now;
        //                    if (SubmitUserTestStatus(userTestStatus))
        //                        flag = true;
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                Log.Error("" + ex);
        //                return flag;
        //            }
        //        }
        //        return flag;
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error("" + ex);
        //        return flag;
        //    }

        //}

        // submit user test status after test complete

        //User KY Test answers submit 90 
        public Boolean SubmitKYTest(int uid, KYAnswers[] kyAnswers)
        {
            int testid = 9;
            Boolean flag = false;
            int TotalAnsCnt = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    try
                    {
                        ////Interest Test Answer Table
                        DataTable answertbl = new DataTable();
                        answertbl.Columns.Add("c_id", typeof(int));
                        answertbl.Columns.Add("q_no", typeof(int));
                        answertbl.Columns.Add("factor_no", typeof(int));
                        answertbl.Columns.Add("marks", typeof(int));
                        answertbl.Columns.Add("batid", typeof(int));

                        for (int j = 0; j < kyAnswers.Length; j++)
                        {
                            int q_no = Convert.ToInt32(kyAnswers[j].qno);
                            int factor_no = Convert.ToInt32(kyAnswers[j].factorno);
                            int marks = Convert.ToInt32(kyAnswers[j].marks);
                            int batid = Convert.ToInt32(kyAnswers[j].batid);
                            answertbl.Rows.Add(uid, q_no, factor_no, marks, batid);
                        }
                        SqlCommand cmd = new SqlCommand("SELECT count(id) as cnt FROM tblKYCandAnswers where c_id=" + uid + " and batid=" + kyAnswers[0].batid, con);
                        SqlDataReader sdr = cmd.ExecuteReader();
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                TotalAnsCnt = Convert.ToInt32(sdr["cnt"]);
                            }
                            sdr.Close();

                        }
                        //else
                        //{
                        // sdr.Close();
                        if (TotalAnsCnt < 90)
                        {
                            if (answertbl.Rows.Count <= 90)
                            {
                                using (var bulkcopy = new SqlBulkCopy(connectionString, SqlBulkCopyOptions.KeepIdentity))
                                {
                                    foreach (DataColumn dc in answertbl.Columns)
                                    {
                                        bulkcopy.ColumnMappings.Add(dc.ColumnName, dc.ColumnName);
                                    }
                                    bulkcopy.BulkCopyTimeout = 100000;
                                    bulkcopy.DestinationTableName = "tblKYCandAnswers";
                                    bulkcopy.WriteToServer(answertbl);
                                }
                                //  testStatus = "Incomplete";
                            }
                        }
                        //}
                        SqlCommand cmd1 = new SqlCommand("SELECT count(id) as cnt FROM tblKYCandAnswers where c_id=" + uid + " and batid=" + kyAnswers[0].batid, con);
                        SqlDataReader sdr1 = cmd1.ExecuteReader();
                        if (sdr1.HasRows)
                        {
                            while (sdr1.Read())
                            {
                                TotalAnsCnt = Convert.ToInt32(sdr1["cnt"]);
                            }
                            sdr1.Close();

                        }
                        string testStatus = "Incomplete";
                        if (TotalAnsCnt == 90)
                        {
                            Log.Info("KY Test is already complete..!!");
                            testStatus = "Complete";
                            flag = true;
                        }
                        string factorStatus = "Incomplete";
                        if (generateKYTestFactor(uid, Convert.ToInt32(kyAnswers[0].batid), testid))
                        {
                            factorStatus = "Complete";
                        }
                        else
                        {
                            factorStatus = "Incomplete";
                        }

                        //insert user test status
                        UserTestStatus userTestStatus = new UserTestStatus();
                        userTestStatus.uId = uid;
                        userTestStatus.testid = 9;
                        userTestStatus.batid = Convert.ToInt32(kyAnswers[0].batid);
                        userTestStatus.testStatus = testStatus;
                        userTestStatus.factorStatus = factorStatus;
                        userTestStatus.dataofcomplete = DateTime.Now;
                        if (SubmitUserTestStatus(userTestStatus))
                            flag = true;
                    }
                    catch (Exception ex)
                    {
                        Log.Error("" + ex);
                        return flag;
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
        private Boolean SubmitUserTestStatus(UserTestStatus userTestStatus)
        {
            Boolean flag = false;
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
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
                }
                return flag;
            }
            catch (Exception ex)
            {
                Log.Error("" + ex);
                return flag;
            }
        }

        public bool AllTestCompleteKYAndPD(KyAndPD KP)
        {
            bool flag = false;
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string str = "select COUNT(id) from tblUserTestMaster where batid =3 and testStatus='Complete' and factorStatus='Complete' and (testid=9 or testid=10) and uid=" + KP.uid + "";
                    SqlCommand cmd = new SqlCommand(str, con);
                    con.Open();
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    if (count >= 2)
                    {
                        string strcmd = "update tblUserProductMaster set testStatus='Complete',dateoftestcomplete= getdate()  where uId=" + KP.uid + " and prodid=7";
                        SqlCommand cmd2 = new SqlCommand(strcmd, con);
                        int EffectedRows = cmd2.ExecuteNonQuery();
                        flag = true;

                        string fname = "", email = "", contactNo = "", exeEmail = "";
                        //string strdata = "select fname, email, contactNo from tblUserMaster where uId ="+uid+"";
                        string strdata = "select u.fname, u.email, u.contactNo,ISNULL(e.exeEmail,'No') as exeEmail from tblUserMaster as u Left Outer Join tblRelation as R on u.uId = R.uId Left Outer Join tblExecutive as e on R.executiveId = e.id where u.uId =" + KP.uid + "";
                        SqlCommand cmd3 = new SqlCommand(strdata, con);
                        SqlDataReader dr = cmd3.ExecuteReader();
                        

                        if (dr.HasRows)
                        {
                            dr.Read();
                            fname = dr["fname"].ToString();
                            email = dr["email"].ToString();
                            contactNo = dr["contactNo"].ToString();
                            if (dr["exeEmail"].ToString() != "No")
                            {
                                exeEmail = dr["exeEmail"].ToString();
                            }
                            else
                            {
                                exeEmail = null;
                            }
                            
                            //Send Mail
                            string body = this.PopulateBodyTestComplete(fname);
                            var task = new System.Threading.Thread(() => DC.sendemail(email, null, exeEmail, ConfigurationManager.AppSettings["CDFTestCompleteEmailSubject"], body));
                            task.Start();
                            ////send email
                            //string SMSText = ConfigurationManager.AppSettings["CDFTestCompleteSmsTemplate"].ToString();
                            //SMSText = SMSText.Replace("{CDF}", fname);
                            //datacontext.sendSms(contactNo, SMSText);
                        }
                        dr.Close();
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

        private string PopulateBodyTestComplete(string userName)
        {
            try
            {
                string body = string.Empty;
                using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["CDFTestCompleteEmailTemplatePath"])))
                {
                    body = reader.ReadToEnd();
                }
                body = body.Replace("{UserName}", userName);
                return body;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}