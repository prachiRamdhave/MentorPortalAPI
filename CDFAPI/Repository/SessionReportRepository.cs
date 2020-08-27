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
    public class SessionReportRepository
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        string conString = ConfigurationManager.ConnectionStrings["career_portalConnectionString"].ConnectionString;

        //public string PostSessionReportDTL(SessionReportModel obj)
        //{
        //    try
        //    {
        //        using (SqlConnection con = new SqlConnection(connectionString))
        //        {
        //            SqlCommand cmd = new SqlCommand("SP_Insert_Udate_Delete_SessionReport", con);
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@Type", "INSERT");
        //            cmd.Parameters.AddWithValue("@CDF_ID", obj.CDF_ID);
        //            cmd.Parameters.AddWithValue("@Session_ID", obj.Session_ID);
        //            cmd.Parameters.AddWithValue("@ReportVersion", obj.ReportVersion);
        //            cmd.Parameters.AddWithValue("@ReportStatus", obj.ReportStatus);
        //            cmd.Parameters.AddWithValue("@ReportPreparedBy", obj.ReportPreparedBy);
        //            cmd.Parameters.AddWithValue("@Stud_Strengths", obj.Stud_Strengths);
        //            cmd.Parameters.AddWithValue("@Stud_Personality_Traits", obj.Stud_Personality_Traits);
        //            cmd.Parameters.AddWithValue("@AreaAdvantage", obj.AreaAdvantage);
        //            cmd.Parameters.AddWithValue("@Motivators", obj.Motivators);
        //            cmd.Parameters.AddWithValue("@GeneralAbilities", obj.GeneralAbilities);
        //            cmd.Parameters.AddWithValue("@LeadershipType", obj.LeadershipType);
        //            cmd.Parameters.AddWithValue("@WaySetGoals", obj.WaySetGoals);
        //            cmd.Parameters.AddWithValue("@WayStudy", obj.WayStudy);
        //            cmd.Parameters.AddWithValue("@WayCommunicate", obj.WayCommunicate);
        //            cmd.Parameters.AddWithValue("@MentalAbilities", obj.MentalAbilities);
        //            cmd.Parameters.AddWithValue("@Creativities", obj.Creativities);
        //            cmd.Parameters.AddWithValue("@PhysicalAbilities", obj.PhysicalAbilities);
        //            cmd.Parameters.AddWithValue("@Skills", obj.Skills);
        //            cmd.Parameters.AddWithValue("@CareerCategory", obj.CareerCategory);
        //            cmd.Parameters.AddWithValue("@OccupationalCategory", obj.OccupationalCategory);
        //            cmd.Parameters.AddWithValue("@Career", obj.Career);
        //            // cmd.Parameters.AddWithValue("@Recommendations", obj.Recommendations);
        //            con.Open();
        //            int i = cmd.ExecuteNonQuery();
        //            if (i > 0)
        //            {
        //                return "Success";
        //            }
        //            else
        //            {
        //                return "Fail";
        //            }

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error(ex);
        //        return "Fail";
        //    }
        //}

        public  SessionReport GetSessionReportDtlBySesID(int CDF_ID, int sessionID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string strQry = "select  * from tblSessionReport where CDF_ID=" + CDF_ID + " and Session_ID=" + sessionID;
                    SqlCommand cmd = new SqlCommand(strQry,con);
                    con.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    SessionReport SR = new SessionReport();
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            SR.Report_ID = Convert.ToInt32(sdr["Report_ID"]);
                            SR.CDF_ID = Convert.ToInt32(sdr["CDF_ID"]);
                            SR.Session_ID = Convert.ToInt32(sdr["Session_ID"]);
                            SR.ReportVersion = Convert.ToInt32(sdr["ReportVersion"]);
                            SR.ReportStatus = sdr["ReportStatus"].ToString();
                            SR.ReportPreparedBy = Convert.ToInt32(sdr["ReportPreparedBy"]);
                            SR.Stud_Strengths = sdr["Stud_Strengths"].ToString();
                            SR.Stud_Personality_Traits = sdr["Stud_Personality_Traits"].ToString();
                            SR.AreaAdvantage = sdr["AreaAdvantage"].ToString();
                            SR.Motivators = sdr["Motivators"].ToString();
                            SR.GeneralAbilities = sdr["GeneralAbilities"].ToString();
                            SR.LeadershipType = sdr["LeadershipType"].ToString();
                            SR.WaySetGoals = sdr["WaySetGoals"].ToString();
                            SR.WayStudy = sdr["WayStudy"].ToString();
                            SR.WayCommunicate = sdr["WayCommunicate"].ToString();
                            SR.MentalAbilities = sdr["MentalAbilities"].ToString();
                            SR.Creativities = sdr["Creativities"].ToString();
                            SR.PhysicalAbilities = sdr["PhysicalAbilities"].ToString();
                            SR.Skills = sdr["Skills"].ToString();
                            SR.CareerCategory = sdr["CareerCategory"].ToString();
                            SR.OccupationalCategory = sdr["OccupationalCategory"].ToString();
                            SR.Career = sdr["Career"].ToString();
                            SR.Recommendations = sdr["Recommendations"].ToString();
                            SR.ReviewedAndApprovedBy = sdr["ReviewedAndApprovedBy"].ToString();
                            SR.ReviewComment = sdr["ReviewComment"].ToString();
                        }
                        return SR;
                    }
                    return null;

                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return null;
            }
        }

        //public string PutSessionReportDTL(SessionReportModel obj)
        //{
        //    try
        //    {
        //        using (SqlConnection con = new SqlConnection(connectionString))
        //        {
        //            SqlCommand cmd = new SqlCommand("SP_Insert_Udate_Delete_SessionReport", con);
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@Type", "UPDATE");
        //            cmd.Parameters.AddWithValue("@CDF_ID", obj.CDF_ID);
        //            cmd.Parameters.AddWithValue("@Session_ID", obj.Session_ID);
        //            cmd.Parameters.AddWithValue("@ReportVersion", obj.ReportVersion);
        //            cmd.Parameters.AddWithValue("@ReportStatus", obj.ReportStatus);
        //            cmd.Parameters.AddWithValue("@ReportPreparedBy", obj.ReportPreparedBy);
        //            cmd.Parameters.AddWithValue("@Stud_Strengths", obj.Stud_Strengths);
        //            cmd.Parameters.AddWithValue("@Stud_Personality_Traits", obj.Stud_Personality_Traits);
        //            cmd.Parameters.AddWithValue("@AreaAdvantage", obj.AreaAdvantage);
        //            cmd.Parameters.AddWithValue("@Motivators", obj.Motivators);
        //            cmd.Parameters.AddWithValue("@GeneralAbilities", obj.GeneralAbilities);
        //            cmd.Parameters.AddWithValue("@LeadershipType", obj.LeadershipType);
        //            cmd.Parameters.AddWithValue("@WaySetGoals", obj.WaySetGoals);
        //            cmd.Parameters.AddWithValue("@WayStudy", obj.WayStudy);
        //            cmd.Parameters.AddWithValue("@WayCommunicate", obj.WayCommunicate);
        //            cmd.Parameters.AddWithValue("@MentalAbilities", obj.MentalAbilities);
        //            cmd.Parameters.AddWithValue("@Creativities", obj.Creativities);
        //            cmd.Parameters.AddWithValue("@PhysicalAbilities", obj.PhysicalAbilities);
        //            cmd.Parameters.AddWithValue("@Skills", obj.Skills);
        //            cmd.Parameters.AddWithValue("@CareerCategory", obj.CareerCategory);
        //            cmd.Parameters.AddWithValue("@OccupationalCategory", obj.OccupationalCategory);
        //            cmd.Parameters.AddWithValue("@Career", obj.Career);
        //            cmd.Parameters.AddWithValue("@Recommendations", obj.Recommendations);
        //            con.Open();
        //            int i = cmd.ExecuteNonQuery();
        //            if (i > 0)
        //            {
        //                return "Success";
        //            }
        //            else
        //            {
        //                return "Fail";
        //            }

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error(ex);
        //        return "Fail";
        //    }
        //}

        public List<CareerCategory> CareerCategory(int ca_id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    List<CareerCategory> li = new List<CareerCategory>();
                    SqlCommand cmd = new SqlCommand("SP_DDLCareers", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Type", "CareerCategories");
                    cmd.Parameters.AddWithValue("@ca_Id", ca_id);
                    con.Open();
                    SqlDataReader da = cmd.ExecuteReader();
                    if (da.HasRows)
                    {
                        while (da.Read())
                        {
                            CareerCategory obj = new CareerCategory();
                            obj.Category = da["Career_category"].ToString();
                            li.Add(obj);
                        }
                        con.Close();
                        return li;
                    }
                    else
                    {
                        con.Close();
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


        public List<OccupationalCategory> OccupationalCategory(int ca_id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    List<OccupationalCategory> li = new List<OccupationalCategory>();
                    SqlCommand cmd = new SqlCommand("SP_DDLCareers", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Type", "OccupationalCategory");
                    cmd.Parameters.AddWithValue("@ca_Id", ca_id);
                    con.Open();
                    SqlDataReader da = cmd.ExecuteReader();
                    if (da.HasRows)
                    {
                        while (da.Read())
                        {
                            OccupationalCategory obj = new OccupationalCategory();
                            obj.Occupation = da["Occupational_category"].ToString();
                            li.Add(obj);
                        }
                        con.Close();
                        return li;
                    }
                    else
                    {
                        con.Close();
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

        public List<Career> Career()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    List<Career> li = new List<Career>();
                    SqlCommand cmd = new SqlCommand("SP_DDLCareers", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Type", "Career");

                    con.Open();
                    SqlDataReader da = cmd.ExecuteReader();
                    if (da.HasRows)
                    {
                        while (da.Read())
                        {
                            Career obj = new Career();
                            obj.ca_Id = Convert.ToInt32(da["ca_Id"]);
                            obj.Mode_Work = da["Career"].ToString();
                            li.Add(obj);
                        }
                        con.Close();
                        return li;
                    }
                    else
                    {
                        con.Close();
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
        public string PostSessionReportDTL(SessionReportModel obj)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SP_Insert_Udate_Delete_SessionReport", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Type", "INSERT");
                    cmd.Parameters.AddWithValue("@CDF_ID", obj.CDF_ID);
                    cmd.Parameters.AddWithValue("@Session_ID", obj.Session_ID);
                    // cmd.Parameters.AddWithValue("@ReportVersion", obj.ReportVersion);
                    cmd.Parameters.AddWithValue("@ReportStatus", obj.ReportStatus);
                    // cmd.Parameters.AddWithValue("@ReportPreparedBy", obj.CDF_ID);
                    // cmd.Parameters.AddWithValue("@Stud_Strengths", obj.Stud_Strengths);
                    string Strengths = "";
                    for (int a = 0; a < obj.Stud_Strengths.Count(); a++)
                    {
                        Strengths = Strengths + "," + obj.Stud_Strengths[a];
                        //  cmd.Parameters.AddWithValue("@Stud_Strengths", ans);
                    }
                    cmd.Parameters.AddWithValue("@Stud_Strengths", Strengths);
                    //  cmd.Parameters.AddWithValue("@Stud_Personality_Traits", obj.Stud_Personality_Traits);
                    string Traits = "";
                    for (int b = 0; b < obj.Stud_Personality_Traits.Count(); b++)
                    {
                        Traits = Traits + "," + obj.Stud_Personality_Traits[b];

                    }
                    cmd.Parameters.AddWithValue("@Stud_Personality_Traits", Traits);

                    cmd.Parameters.AddWithValue("@AreaAdvantage", obj.AreaAdvantage);
                    cmd.Parameters.AddWithValue("@Motivators", obj.Motivators);
                    cmd.Parameters.AddWithValue("@GeneralAbilities", obj.GeneralAbilities);
                    cmd.Parameters.AddWithValue("@LeadershipType", obj.LeadershipType);
                    cmd.Parameters.AddWithValue("@WaySetGoals", obj.WaySetGoals);
                    cmd.Parameters.AddWithValue("@WayStudy", obj.WayStudy);
                    cmd.Parameters.AddWithValue("@WayCommunicate", obj.WayCommunicate);
                    cmd.Parameters.AddWithValue("@MentalAbilities", obj.MentalAbilities);
                    cmd.Parameters.AddWithValue("@Creativities", obj.Creativities);
                    cmd.Parameters.AddWithValue("@PhysicalAbilities", obj.PhysicalAbilities);
                    cmd.Parameters.AddWithValue("@Skills", obj.Skills);
                    cmd.Parameters.AddWithValue("@CareerCategory", obj.CareerCategory);
                    cmd.Parameters.AddWithValue("@OccupationalCategory", obj.OccupationalCategory);
                    cmd.Parameters.AddWithValue("@Career", obj.Career);
                    cmd.Parameters.AddWithValue("@ReviewComment", obj.ReviewComment);
                    // cmd.Parameters.AddWithValue("@Recommendations", obj.Recommendations);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        return "Success";
                    }
                    else
                    {
                        return "Fail";
                    }

                }

            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return "Fail";
            }
        }

        public string PutSessionReportDTL(SessionReportModel obj)
        {
            try
            {
                string Msg = "";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    if (obj.ReportStatus == "Draft" || obj.ReportStatus == "Approved")
                    {
                        SqlCommand cmd = new SqlCommand("SP_Insert_Udate_Delete_SessionReport", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Type", "UPDATE");
                        cmd.Parameters.AddWithValue("@CDF_ID", obj.CDF_ID);
                        cmd.Parameters.AddWithValue("@Session_ID", obj.Session_ID);
                        //  cmd.Parameters.AddWithValue("@ReportVersion", obj.ReportVersion);
                        cmd.Parameters.AddWithValue("@ReportStatus", obj.ReportStatus);
                        //  cmd.Parameters.AddWithValue("@ReportPreparedBy", obj.CDF_ID);
                        // cmd.Parameters.AddWithValue("@Stud_Strengths", obj.Stud_Strengths);
                        string Strengths = "";
                        for (int a = 0; a < obj.Stud_Strengths.Count(); a++)
                        {
                            Strengths = Strengths + "," + obj.Stud_Strengths[a];
                            //  cmd.Parameters.AddWithValue("@Stud_Strengths", ans);
                        }
                        cmd.Parameters.AddWithValue("@Stud_Strengths", Strengths);
                        //  cmd.Parameters.AddWithValue("@Stud_Personality_Traits", obj.Stud_Personality_Traits);
                        string Traits = "";
                        for (int b = 0; b < obj.Stud_Personality_Traits.Count(); b++)
                        {
                            Traits = Traits + "," + obj.Stud_Personality_Traits[b];

                        }
                        cmd.Parameters.AddWithValue("@Stud_Personality_Traits", Traits);
                        cmd.Parameters.AddWithValue("@AreaAdvantage", obj.AreaAdvantage);
                        cmd.Parameters.AddWithValue("@Motivators", obj.Motivators);
                        cmd.Parameters.AddWithValue("@GeneralAbilities", obj.GeneralAbilities);
                        cmd.Parameters.AddWithValue("@LeadershipType", obj.LeadershipType);
                        cmd.Parameters.AddWithValue("@WaySetGoals", obj.WaySetGoals);
                        cmd.Parameters.AddWithValue("@WayStudy", obj.WayStudy);
                        cmd.Parameters.AddWithValue("@WayCommunicate", obj.WayCommunicate);
                        cmd.Parameters.AddWithValue("@MentalAbilities", obj.MentalAbilities);
                        cmd.Parameters.AddWithValue("@Creativities", obj.Creativities);
                        cmd.Parameters.AddWithValue("@PhysicalAbilities", obj.PhysicalAbilities);
                        cmd.Parameters.AddWithValue("@Skills", obj.Skills);
                        cmd.Parameters.AddWithValue("@CareerCategory", obj.CareerCategory);
                        cmd.Parameters.AddWithValue("@OccupationalCategory", obj.OccupationalCategory);
                        cmd.Parameters.AddWithValue("@Career", obj.Career);
                        cmd.Parameters.AddWithValue("@ReviewComment", obj.ReviewComment);
                        //  cmd.Parameters.AddWithValue("@Recommendations", obj.Recommendations);
                        con.Open();
                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            Msg = "Success";
                        }
                        else
                        {
                            Msg = "Fail";
                        }
                        return Msg;
                    }
                    if (obj.ReportStatus == "Rejected")
                    {
                        SqlCommand cmd = new SqlCommand("SP_Insert_Udate_Delete_SessionReport", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Type", "REJECTED");
                        cmd.Parameters.AddWithValue("@CDF_ID", obj.CDF_ID);
                        cmd.Parameters.AddWithValue("@Session_ID", obj.Session_ID);
                        //  cmd.Parameters.AddWithValue("@ReportVersion", obj.ReportVersion);
                        cmd.Parameters.AddWithValue("@ReportStatus", obj.ReportStatus);
                        //    cmd.Parameters.AddWithValue("@ReportPreparedBy", obj.CDF_ID);
                        cmd.Parameters.AddWithValue("@Stud_Strengths", obj.Stud_Strengths);
                        cmd.Parameters.AddWithValue("@Stud_Personality_Traits", obj.Stud_Personality_Traits);
                        cmd.Parameters.AddWithValue("@AreaAdvantage", obj.AreaAdvantage);
                        cmd.Parameters.AddWithValue("@Motivators", obj.Motivators);
                        cmd.Parameters.AddWithValue("@GeneralAbilities", obj.GeneralAbilities);
                        cmd.Parameters.AddWithValue("@LeadershipType", obj.LeadershipType);
                        cmd.Parameters.AddWithValue("@WaySetGoals", obj.WaySetGoals);
                        cmd.Parameters.AddWithValue("@WayStudy", obj.WayStudy);
                        cmd.Parameters.AddWithValue("@WayCommunicate", obj.WayCommunicate);
                        cmd.Parameters.AddWithValue("@MentalAbilities", obj.MentalAbilities);
                        cmd.Parameters.AddWithValue("@Creativities", obj.Creativities);
                        cmd.Parameters.AddWithValue("@PhysicalAbilities", obj.PhysicalAbilities);
                        cmd.Parameters.AddWithValue("@Skills", obj.Skills);
                        cmd.Parameters.AddWithValue("@CareerCategory", obj.CareerCategory);
                        cmd.Parameters.AddWithValue("@OccupationalCategory", obj.OccupationalCategory);
                        cmd.Parameters.AddWithValue("@Career", obj.Career);
                        cmd.Parameters.AddWithValue("@ReviewComment", obj.ReviewComment);
                        //  cmd.Parameters.AddWithValue("@Recommendations", obj.Recommendations);
                        con.Open();
                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            Msg = "Success";
                        }
                        else
                        {
                            Msg = "Fail";
                        }
                    }
                    return Msg;
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return "Fail";
            }
        }

    }
}