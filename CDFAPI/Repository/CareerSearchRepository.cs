using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CDFAPI.Models;
using log4net;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using static CDFAPI.Models.CareerSearch;
namespace CDFAPI.Repository
{
    public class CareerSearchRepository
    {

        string ConnectionStringDheya = ConfigurationManager.ConnectionStrings["career_portalConnectionString"].ConnectionString;
        string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);    
       // string conString = ConfigurationManager.ConnectionStrings["career_portalConnectionString"].ConnectionString;
   


        public List<CareerSearch.Course1> GetAllCourseddl()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string strQry = "select distinct co_id,course_name from tbl_newcourse_master order by course_name ASC";
                    SqlCommand cmd = new SqlCommand(strQry, con);
                    con.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    List<Course1> li = new List<Course1>();
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            Course1 CR = new Course1();
                            CR.CourseID = Convert.ToInt32(sdr["co_id"]);
                            CR.CourseName = sdr["course_name"].ToString();
                            li.Add(CR);

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

        public List<FieldOfWorkDDL> GetAllFieldOfWorkDDL()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionStringDheya))
                {
                    string strQry = "select distinct Occupational_category  from tbl_career_master";
                    SqlCommand cmd = new SqlCommand(strQry, con);
                    con.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    List<FieldOfWorkDDL> li = new List<FieldOfWorkDDL>();
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            FieldOfWorkDDL FW = new FieldOfWorkDDL();

                            FW.FieldOfWork = sdr["Occupational_category"].ToString();
                            li.Add(FW);

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

        public List<TypeOFWorkDDL> GetTypeOfWorkForCareer()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionStringDheya))
                {
                    string strQry = "select distinct Career_Category from tbl_career_master order by Career_Category ASC";
                    SqlCommand cmd = new SqlCommand(strQry, con);
                    con.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    List<TypeOFWorkDDL> li = new List<TypeOFWorkDDL>();
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            TypeOFWorkDDL TW = new TypeOFWorkDDL();

                            TW.TypeOfWork = sdr["Career_Category"].ToString();
                            li.Add(TW);

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

        public  List<CareerSearch.CareerDetails> GetCareerDtl()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionStringDheya))
                {
                    string strQry = "select ca_id, basic_info1 as career_Title,basic_info6 as TypeOfWork,Career_scope as FutureRelevance,Occupational_category as FieldOfWork from tbl_career_master order by basic_info6";
                    SqlCommand cmd = new SqlCommand(strQry, con);
                    con.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    List<CareerSearch.CareerDetails> li = new List<CareerSearch.CareerDetails>();
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            CareerSearch.CareerDetails CD = new CareerSearch.CareerDetails();
                            CD.ca_id = Convert.ToInt32(sdr["ca_id"]);
                            CD.careerTitle = sdr["career_Title"].ToString();
                            CD.TypeOfWork = sdr["TypeOfWork"].ToString();
                            CD.FutureRelevance = sdr["FutureRelevance"].ToString();
                            CD.FieldOfWork = sdr["FieldOfWork"].ToString();
                            li.Add(CD);

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

        public List<CourseDetails> GetCourseByCourseName(string course)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string strcmd = "";

                    strcmd = "select co_id, course_name, field_of_work  from tbl_newcourse_master where course_name like '"+ course + "' order by course_name";

                    SqlCommand cmd = new SqlCommand(strcmd, con);
                    con.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    List<CourseDetails> li = new List<CourseDetails>();
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            CourseDetails CD = new CourseDetails();
                            CD.coID = Convert.ToInt32(sdr["co_id"]);
                            CD.CourseName = sdr["course_name"].ToString();
                            CD.CourseCategory = sdr["field_of_work"].ToString();
                            li.Add(CD);
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

        public List<CourseDetails> GetAllCourseDtl(string curEducation, string course)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string strcmd = "";
                    if (curEducation == "10th")
                    {

                        strcmd = "select co_id, course1,course6 from tbl_course_master where course7 like '" + course + "'order by course1";

                    }
                    if (curEducation == "12th")
                    {
                        strcmd = "select co_id, course1,course6 from tbl_course_master where course7 like '" + course + "'order by course1";

                    }
                    if (curEducation == "Graduation")
                    {
                        strcmd = "select co_id, course1,course6 from tbl_course_master where course13 like '" + course + "'order by course1";

                    }

                    SqlCommand cmd = new SqlCommand(strcmd, con);
                    con.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    List<CourseDetails> li = new List<CourseDetails>();
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            CourseDetails CD = new CourseDetails();
                            CD.coID = Convert.ToInt32(sdr["co_id"]);
                            CD.CourseName = sdr["course1"].ToString();
                            CD.CourseCategory = sdr["course6"].ToString();
                            li.Add(CD);
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

        public List<GraduationDtl> GetAllGraduation()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string strQry = "select distinct course13 from tbl_course_master where course13 != '-' order by course13 asc";
                    SqlCommand cmd = new SqlCommand(strQry, con);
                    con.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    List<GraduationDtl> li = new List<GraduationDtl>();
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            GraduationDtl GD = new GraduationDtl();
                           
                            GD.Graduation = sdr["course13"].ToString();
                            li.Add(GD);

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


        //Bhagyashree
        public List<Course> GetDDL_Course()
        {
            try
            {
                List<Course> li = new List<Course>();
                using (SqlConnection con = new SqlConnection(ConnectionStringDheya))
                {
                    SqlCommand cmd = new SqlCommand("SP_InstituteSearch", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Type", "DDL_Course");
                    cmd.Parameters.AddWithValue("@State", "");
                    cmd.Parameters.AddWithValue("@Subco_id", "");
                    cmd.Parameters.AddWithValue("@Co_id", "");
                    cmd.Parameters.AddWithValue("@Ca_id", "");

                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            Course obj = new Course();
                            obj.co_id = Convert.ToInt32(dr["co_id"]);
                            obj.co_name = dr["course_name"].ToString();
                            li.Add(obj);
                        }
                        con.Close();
                        return li;
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

        public List<Course> GetDDL_CoursebyCareer(int CareerID)
        {
            try
            {
                List<Course> li = new List<Course>();
                using (SqlConnection con = new SqlConnection(ConnectionStringDheya))
                {
                    SqlCommand cmd = new SqlCommand("SP_InstituteSearch", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Type", "DDL_CourseByCareer");
                    cmd.Parameters.AddWithValue("@State", "");
                    cmd.Parameters.AddWithValue("@Subco_id", "");
                    cmd.Parameters.AddWithValue("@Co_id", "");
                    cmd.Parameters.AddWithValue("@Ca_id", CareerID);

                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            Course obj = new Course();
                            obj.co_id = Convert.ToInt32(dr["co_id"]);
                            obj.co_name = dr["course_name"].ToString();
                            li.Add(obj);
                        }
                        con.Close();
                        return li;
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

        public List<SubCourse> GetDDL_SubCourse()
        {
            try
            {
                List<SubCourse> li = new List<SubCourse>();
                using (SqlConnection con = new SqlConnection(ConnectionStringDheya))
                {
                    SqlCommand cmd = new SqlCommand("SP_InstituteSearch", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Type", "DDL_SubCourse");
                    cmd.Parameters.AddWithValue("@State", "");
                    cmd.Parameters.AddWithValue("@Subco_id", "");
                    cmd.Parameters.AddWithValue("@Co_id", "");
                    cmd.Parameters.AddWithValue("@Ca_id", "");
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            SubCourse obj = new SubCourse();
                            obj.Subco_id = Convert.ToInt32(dr["subco_id"]);
                            obj.Subco_name = dr["subco_name"].ToString();
                            li.Add(obj);
                        }
                        con.Close();
                        return li;
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

        public List<SubCourse> GetDDL_SubCourseByCourse(int CourseID)
        {
            try
            {
                List<SubCourse> li = new List<SubCourse>();
                using (SqlConnection con = new SqlConnection(ConnectionStringDheya))
                {
                    SqlCommand cmd = new SqlCommand("SP_InstituteSearch", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Type", "DDL_SubCourseByCourse");
                    cmd.Parameters.AddWithValue("@State", "");
                    cmd.Parameters.AddWithValue("@Subco_id", "");
                    cmd.Parameters.AddWithValue("@Co_id", CourseID);
                    cmd.Parameters.AddWithValue("@Ca_id", "");

                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            SubCourse obj = new SubCourse();
                            obj.Subco_id = Convert.ToInt32(dr["subco_id"]);
                            obj.Subco_name = dr["subco_name"].ToString();
                            li.Add(obj);
                        }
                        con.Close();
                        return li;
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

        public List<Specialization> GetDDL_Specialization(int Subco_id, int Co_id)
        {
            try
            {
                List<Specialization> li = new List<Specialization>();
                using (SqlConnection con = new SqlConnection(ConnectionStringDheya))
                {
                    SqlCommand cmd = new SqlCommand("SP_InstituteSearch", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Type", "DDL_SpcializationBySubcourse");
                    cmd.Parameters.AddWithValue("@State", "");
                    cmd.Parameters.AddWithValue("@Subco_id", Subco_id);
                    cmd.Parameters.AddWithValue("@Co_id", Co_id);
                    cmd.Parameters.AddWithValue("@Ca_id", "");

                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            Specialization obj = new Specialization();
                            obj.SpecializationName = dr["specialization"].ToString();
                            li.Add(obj);
                        }
                        con.Close();
                        return li;
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

        public List<State> GetDDL_State()
        {
            try
            {
                List<State> li = new List<State>();
                using (SqlConnection con = new SqlConnection(ConnectionStringDheya))
                {
                    SqlCommand cmd = new SqlCommand("SP_InstituteSearch", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Type", "DDL_STATE");
                    cmd.Parameters.AddWithValue("@State", "");
                    cmd.Parameters.AddWithValue("@Subco_id", "");
                    cmd.Parameters.AddWithValue("@Co_id", "");
                    cmd.Parameters.AddWithValue("@Ca_id", "");

                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            State obj = new State();
                            obj.StateName = dr["state"].ToString();
                            li.Add(obj);
                        }
                        con.Close();
                        return li;
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

        public List<City> GetDDL_City()
        {
            try
            {
                List<City> li = new List<City>();
                using (SqlConnection con = new SqlConnection(ConnectionStringDheya))
                {
                    SqlCommand cmd = new SqlCommand("SP_InstituteSearch", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Type", "DDL_CITY");
                    cmd.Parameters.AddWithValue("@State", "");
                    cmd.Parameters.AddWithValue("@Subco_id", "");
                    cmd.Parameters.AddWithValue("@Co_id", "");
                    cmd.Parameters.AddWithValue("@Ca_id", "");

                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            City obj = new City();
                            obj.City_id = Convert.ToInt32(dr["city_id"]);
                            obj.CityName = dr["District"].ToString();
                            li.Add(obj);
                        }
                        con.Close();
                        return li;
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

        public List<City> GetDDL_CityByState(string State)
        {
            try
            {
                List<City> li = new List<City>();
                using (SqlConnection con = new SqlConnection(ConnectionStringDheya))
                {
                    SqlCommand cmd = new SqlCommand("SP_InstituteSearch", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Type", "DDL_STATE_SELECTION");
                    cmd.Parameters.AddWithValue("@State", State);
                    cmd.Parameters.AddWithValue("@Subco_id", "");
                    cmd.Parameters.AddWithValue("@Co_id", "");
                    cmd.Parameters.AddWithValue("@Ca_id", "");

                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            City obj = new City();
                            obj.City_id = Convert.ToInt32(dr["city_id"]);
                            obj.CityName = dr["District"].ToString();
                            li.Add(obj);
                        }
                        con.Close();
                        return li;
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

        public List<InstituteSearch> GetList_SearchInstitute(int career, int course, int subcourse, string specialization, string state, string city)
        {
            try
            {
                List<InstituteSearch> li = new List<InstituteSearch>();
                using (SqlConnection con = new SqlConnection(ConnectionStringDheya))
                {
                    SqlCommand cmd = new SqlCommand("SP_SearchInstitute_Career_Course_Subcourse", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Career", career);
                    cmd.Parameters.AddWithValue("@Course", course);
                    cmd.Parameters.AddWithValue("@SubCourse", subcourse);
                    cmd.Parameters.AddWithValue("@Special", specialization);
                    cmd.Parameters.AddWithValue("@State", state);
                    cmd.Parameters.AddWithValue("@City", city);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            InstituteSearch obj = new InstituteSearch();
                            obj.Inst_ID = Convert.ToInt32(dr["inst_id"]);
                            obj.InstName = dr["inst_name"].ToString();
                            obj.CityName = dr["city"].ToString();
                            obj.Subco_id = Convert.ToInt32(dr["subco_id"]);
                            obj.Subco_name = dr["subco_name"].ToString();
                            obj.SpecializationName = dr["specialization"].ToString();
                            li.Add(obj);
                        }
                        con.Close();
                        return li;
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

        public List<InstituteSearch> GetList_InstituteSearchByCareerCourse(int career, int course)
        {
            try
            {
                List<InstituteSearch> li = new List<InstituteSearch>();
                using (SqlConnection con = new SqlConnection(ConnectionStringDheya))
                {
                    SqlCommand cmd = new SqlCommand("SP_InstituteSearchAndDetails", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Type", "InstituteSearchByCareerCourse");
                    cmd.Parameters.AddWithValue("@Career", career);
                    cmd.Parameters.AddWithValue("@Course", course);
                    cmd.Parameters.AddWithValue("@Inst", "");
                    cmd.Parameters.AddWithValue("@Subco", "");

                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            InstituteSearch obj = new InstituteSearch();
                            obj.Inst_ID = Convert.ToInt32(dr["inst_id"]);
                            obj.InstName = dr["inst_name"].ToString();
                            obj.CityName = dr["city"].ToString();
                            obj.Subco_id = Convert.ToInt32(dr["subco_id"]);
                            obj.Subco_name = dr["subco_name"].ToString();
                            obj.SpecializationName = dr["specialization"].ToString();
                            li.Add(obj);
                        }
                        con.Close();
                        return li;
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

        public AllList GetList_InstituteSubCourseDetails(int inst_id, int Subcourse_id, string Specialization)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionStringDheya))
                {
                    AllList list = new AllList();
                    List<InstituteDetails> li = new List<InstituteDetails>();
                    SqlCommand cmd = new SqlCommand("SP_InstituteSearchAndDetails", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Type", "InstituteSubCourseDTL");
                    cmd.Parameters.AddWithValue("@Career", "");
                    cmd.Parameters.AddWithValue("@Course", "");
                    cmd.Parameters.AddWithValue("@Inst", inst_id);
                    cmd.Parameters.AddWithValue("@Subco", Subcourse_id);
                    cmd.Parameters.AddWithValue("@specialization", Specialization);

                    //   con.Open();
                    // SqlDataReader dr = cmd.ExecuteReader();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        InstituteDetails obj = new InstituteDetails();
                        obj.instname = ds.Tables[0].Rows[0]["inst_name"].ToString();
                        obj.catagory = ds.Tables[0].Rows[0]["category"].ToString();
                        obj.region = ds.Tables[0].Rows[0]["region"].ToString();
                        obj.state = ds.Tables[0].Rows[0]["state"].ToString();
                        obj.city = ds.Tables[0].Rows[0]["city"].ToString();
                        obj.website = ds.Tables[0].Rows[0]["website"].ToString();
                        obj.affil = ds.Tables[0].Rows[0]["Affiliation"].ToString();
                        obj.email = ds.Tables[0].Rows[0]["emailid"].ToString();
                        obj.contact = ds.Tables[0].Rows[0]["contact_no"].ToString();
                        obj.address = ds.Tables[0].Rows[0]["address"].ToString();

                        list.InstituteDetails.Add(obj);
                    }
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        SubcourseDetails obj = new SubcourseDetails();
                        obj.subconame = ds.Tables[1].Rows[0]["subco_name"].ToString();
                        obj.catagory = ds.Tables[1].Rows[0]["category"].ToString();
                        obj.stream = ds.Tables[1].Rows[0]["stream"].ToString();
                        obj.specialization = ds.Tables[1].Rows[0]["specialization"].ToString();
                        obj.duration = ds.Tables[1].Rows[0]["subco_duration"].ToString();
                        obj.req = ds.Tables[1].Rows[0]["basic_req"].ToString();
                        obj.descrip = ds.Tables[1].Rows[0]["descrip"].ToString();
                        obj.instreq = ds.Tables[1].Rows[0]["inst_req"].ToString();
                        obj.rank = ds.Tables[1].Rows[0]["rank"].ToString();
                        obj.indiatodayrank = ds.Tables[1].Rows[0]["indiatodayrank"].ToString();
                        obj.businesstodayrank = ds.Tables[1].Rows[0]["businesstodayrank"].ToString();
                        obj.hindustantimesrank = ds.Tables[1].Rows[0]["hindustantimesrank"].ToString();
                        obj.dheya_rank = ds.Tables[1].Rows[0]["dheya_rank"].ToString();
                        obj.entranceId = ds.Tables[1].Rows[0]["entrance_id"].ToString();
                        obj.entrancename = ds.Tables[1].Rows[0]["entrance_name"].ToString();

                        if (ds.Tables[2].Rows.Count > 0)
                        {
                            string OtherSpe = "";

                            for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                            {
                                OtherSpe += ds.Tables[2].Rows[i][0].ToString() + " , ";
                            }
                            OtherSpe.Substring(0, OtherSpe.LastIndexOf(','));
                            obj.OtherSpecialization = OtherSpe;

                            //list.SubcourseDetails.Add(obj);
                        }
                        list.SubcourseDetails.Add(obj);
                    }


                    else
                    {
                        return null;
                    }
                    return list;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return null;
            }
        }

        public List<EntranceExamDetails> GetList_List_EntranceExamDetails(int EntranceId)
        {
            try
            {
                List<EntranceExamDetails> li = new List<EntranceExamDetails>();
                using (SqlConnection con = new SqlConnection(ConnectionStringDheya))
                {
                    SqlCommand cmd = new SqlCommand("SP_InstituteSearchAndDetails", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Type", "EntranceExamDetails");
                    cmd.Parameters.AddWithValue("@EntranceId", EntranceId);


                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            EntranceExamDetails obj = new EntranceExamDetails();
                            obj.EntranceName = dr["entrance_name"].ToString();
                            obj.Detail = dr["exam_detail"].ToString();
                            obj.Req = dr["exam_req"].ToString();
                            obj.Fee = dr["exam_fee"].ToString();
                            obj.Examdate = dr["exam_date"].ToString();
                            obj.Applicationdate = dr["app_date"].ToString();
                            obj.Entrancelink = dr["app_link"].ToString();
                            li.Add(obj);
                        }
                        con.Close();
                        return li;
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

    }
}