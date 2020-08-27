using Common.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using CDFAPI.Models;
using System.Data.SqlClient;
using System.Data;

namespace CDFAPI.Repository
{
    public class CourseDtlRepository
    {
        string connectionString1 = ConfigurationManager.ConnectionStrings["career_portalConnectionString"].ConnectionString;
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

        public CourseDtl GetCourseDtlById(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString1))
            {
                string strcmd = "SELECT * FROM tbl_newcourse_master Where co_id ="+ id;
               // SqlCommand cmd = new SqlCommand(strcmd, con);
                con.Open();
              //  List<CourseDtl> li = new List<CourseDtl>();
                CourseDtl CN = new CourseDtl();
                SqlDataAdapter da = new SqlDataAdapter(strcmd, con);
                DataSet ds = new DataSet();
                da.Fill(ds);

                CN.CourseName= ds.Tables[0].Rows[0][1].ToString();
                CN.WhatThisCourseAllAbout = ds.Tables[0].Rows[0][2].ToString();
                CN.JobMarket = ds.Tables[0].Rows[0][3].ToString();
                CN.IndiaDemand = ds.Tables[0].Rows[0][4].ToString();
                CN.OverseasDemand = ds.Tables[0].Rows[0][5].ToString();
                CN.CourseCategory = ds.Tables[0].Rows[0][6].ToString();
                CN.StreamIn = ds.Tables[0].Rows[0][8].ToString();

                if (ds.Tables[0].Rows[0][9].ToString() == "Yes")
                    CN.Physics = "Mandatory Requirement";
                if (ds.Tables[0].Rows[0][9].ToString() == "No")
                    CN.Physics = "Not a Mandatory Requirement";
                if (ds.Tables[0].Rows[0][9].ToString() == "Either")
                    CN.Physics = "No Specific Requirement";

                if (ds.Tables[0].Rows[0][10].ToString() == "Yes")
                    CN.Chemistry = "Mandatory Requirement";
                if (ds.Tables[0].Rows[0][10].ToString() == "No")
                   CN.Chemistry = "Not a Mandatory Requirement";
                if (ds.Tables[0].Rows[0][10].ToString() == "Either")
                   CN.Chemistry = "No Specific Requirement";

                if (ds.Tables[0].Rows[0][11].ToString() == "Yes")
                   CN.Mathematics = "Mandatory Requirement";
                if (ds.Tables[0].Rows[0][11].ToString() == "No")
                   CN.Mathematics = "Not a Mandatory Requirement";
                if (ds.Tables[0].Rows[0][11].ToString() == "Either")
                    CN.Mathematics = "No Specific Requirement";

                if (ds.Tables[0].Rows[0][12].ToString() == "Yes")
                   CN.Biology = "Mandatory Requirement";
                if (ds.Tables[0].Rows[0][12].ToString() == "No")
                    CN.Biology = "Not a Mandatory Requirement";
                if (ds.Tables[0].Rows[0][12].ToString() == "Either")
                   CN.Biology = "No Specific Requirement";

                if (ds.Tables[0].Rows[0][13].ToString() != "NULL" && ds.Tables[0].Rows[0][13].ToString() != "")
                    CN.OtherSubjects = ds.Tables[0].Rows[0][13].ToString();
                else
                    CN.OtherSubjects = "No Specific Choice of Subjects.";
                
              //  li.Add(CN);
             
                return CN;
                
            }
        }

        public List<InstZone> GetAllInstituteZone()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString1))
                {
                    string strcomm = "select distinct zone from tbl_institute order by zone ASC";
                    SqlCommand cmd = new SqlCommand(strcomm,con);
                    con.Open();
                    List<InstZone> li = new List<InstZone>();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            InstZone iz = new InstZone();
                            iz.zone = sdr["zone"].ToString();
                            li.Add(iz);
                        }
                        return li;
                    }
                    return null;
                }

            }
            catch (Exception ex)
            {
                Log.Error("" + ex);
                return null;
            }
        }

        public List<careerList> GetCourseList(int id)
        {
            try
            {

                using (SqlConnection con = new SqlConnection(connectionString1))
                {
                    con.Open();
                    string strcmd1 = "";
                    strcmd1 = "SELECT A.ca_id, A.basic_info1 FROM tbl_career_master A, tbl_career_course_bridge B ";
                    strcmd1 += " Where A.ca_id = B.ca_id AND B.co_id = " + id;
                    SqlDataAdapter da1 = new SqlDataAdapter(strcmd1, con);
                    DataSet ds11 = new DataSet();
                    da1.Fill(ds11);

                     List<careerList> li2 = new List<careerList>();
                    for (int i = 0; i < ds11.Tables[0].Rows.Count; i++)
                    {
                        careerList cl = new careerList();
                         cl.co_id= Convert.ToInt32(ds11.Tables[0].Rows[i][0]);
                         cl.co_text = ds11.Tables[0].Rows[i][1].ToString();
                        li2.Add(cl);

                    }
                    return li2;
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