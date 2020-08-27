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
    public class CareerToolsRepository
    {
        //string connectionString = ConfigurationManager.ConnectionStrings["career_portalConnectionString"].ConnectionString;
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        string connectionString1 = ConfigurationManager.ConnectionStrings["career_portalConnectionString"].ConnectionString;
        public CareerToolsModel GetAllFilters(string ability1, string ability2, string ability3, string interest1, string interest2, string Personality1, string Personality2, string Personality3)
        {            
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    // Query For Ability 
                    CareerToolsModel ctm = new CareerToolsModel();
                    string AbilityQuery = " SELECT distinct A.ca_id As CareerID, B.Career_category As CareerCategory,B.Occupational_category As OccupationalCategory," +
                         " B.basic_info1 As Career FROM tbl_career_ability_master AS A INNER JOIN tbl_career_master As B ON A.ca_id = B.ca_id " +
                         " WHERE (ability1 = '" + ability1 + "' or ability2 = '" + ability1 + "' or ability3 = '" + ability1 + "')  AND A.ca_id in " +
                         " (SELECT [ca_id] FROM tbl_career_ability_master WHERE (ability1 = '" + ability2 + "'  or ability2 = '" + ability2 + "' or ability3 = '" + ability2 + "') AND " +
                         " A.ca_id in  (SELECT [ca_id] FROM tbl_career_ability_master where ability1 = '" + ability3 + "' " +
                         " or ability2 = '" + ability3 + "' or ability3 = '" + ability3 + "') )  AND B.Career_category IS NOT NULL ";

                    SqlCommand cmd = new SqlCommand(AbilityQuery, con);
                    con.Open();

                    List<CareerToolsAbility> lcta = new List<CareerToolsAbility>();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            CareerToolsAbility cta = new CareerToolsAbility();
                            cta.caId = Convert.ToInt32(dr["CareerID"]);
                            cta.CareerCategory = dr["CareerCategory"].ToString();
                            cta.OccupationalCategory = dr["OccupationalCategory"].ToString();
                            cta.Career = dr["Career"].ToString();

                            ctm.lcta.Add(cta);
                          
                        }

                        // return lcta;
                    }
                    else
                    {
                        return null;
                    }
                    dr.Close();

                    // Query For Interest

                    string InterestQuery = "SELECT distinct ca_id As CareerID, Career_category As CareerCategory,Occupational_category As OccupationalCategory,basic_info1 As Career  FROM tbl_career_master" +
                         "  WHERE (basic_info4 = '" + interest1 + "' or basic_info5 = '" + interest1 + "')  " +
                         " AND ca_id in (SELECT distinct ca_id FROM tbl_career_master where basic_info4 = '" + interest2 + "' or basic_info5 = '" + interest2 + "')AND Career_category IS NOT NULL";

                    cmd = new SqlCommand(InterestQuery, con);
                    //con.Open();
                    List<CareerToolsInterest> lcti = new List<CareerToolsInterest>();
                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            CareerToolsInterest cti = new CareerToolsInterest();
                            cti.caId = Convert.ToInt32(dr["CareerID"]);
                            cti.CareerCategory = dr["CareerCategory"].ToString();
                            cti.OccupationalCategory = dr["OccupationalCategory"].ToString();
                            cti.Career = dr["Career"].ToString();
                            ctm.lcti.Add(cti);
                        }

                       // return ;
                    }
                    else
                    {
                       // return null;
                    }
                    dr.Close();
                    // Query For Personality

                    string PersonalityQuery = "SELECT distinct A.ca_id As CareerID, B.Career_category As CareerCategory,B.Occupational_category As OccupationalCategory, B.basic_info1 As Career FROM tbl_career_personality_master AS A INNER JOIN tbl_career_master As B ON A.ca_id = B.ca_id " +
                           " where (personality1='" + Personality1 + "' OR personality2='" + Personality1 + "' OR personality3='" + Personality1 + "') " +
                           " AND A.ca_id in(SELECT [ca_id] FROM tbl_career_personality_master WHERE (personality1 = '" + Personality2 + "' or personality2 = '" + Personality2 + "' or personality3 = '" + Personality2 + "'))" +
                           " AND A.ca_id in  (SELECT [ca_id] FROM tbl_career_personality_master WHERE (personality1 = '" + Personality3 + "' or personality2 = '" + Personality3 + "' or personality3 = '" + Personality3 + "'))" +
                           " AND B.Career_category IS NOT NULL";

                    cmd = new SqlCommand(PersonalityQuery, con);
                    //con.Open();
                    List<CareerToolsPersonality> lctp = new List<CareerToolsPersonality>();
                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            CareerToolsPersonality ct = new CareerToolsPersonality();
                            ct.caId = Convert.ToInt32(dr["CareerID"]);
                            ct.CareerCategory = dr["CareerCategory"].ToString();
                            ct.OccupationalCategory = dr["OccupationalCategory"].ToString();
                            ct.Career = dr["Career"].ToString();
                            ctm.lctp.Add(ct);
                        }

                       // return lct;
                    }
                    else
                    {
                        //return null;
                    }
                    dr.Close();

                    //Query For Combined OF ABILITY Interest And Personality

                    string CombinedQuery = " SELECT distinct A.ca_id As CareerID, B.Career_category As CareerCategory,B.Occupational_category As OccupationalCategory," +
                       " B.basic_info1 As Career FROM tbl_career_ability_master AS A INNER JOIN tbl_career_master As B ON A.ca_id = B.ca_id " +
                       " WHERE (ability1 = '" + ability1 + "' or ability2 = '" + ability1 + "' or ability3 = '" + ability1 + "')  AND A.ca_id in " +
                       " (SELECT [ca_id] FROM tbl_career_ability_master WHERE (ability1 = '" + ability2 + "'  or ability2 = '" + ability2 + "' or ability3 = '" + ability2 + "') AND " +
                       " A.ca_id in  (SELECT [ca_id] FROM tbl_career_ability_master where ability1 = '" + ability3 + "' " +
                       " or ability2 = '" + ability3 + "' or ability3 = '" + ability3 + "') )  AND B.Career_category IS NOT NULL " +
                       " UNION  " +
                       " (SELECT distinct ca_id As CareerID, Career_category As CareerCategory,Occupational_category As OccupationalCategory,basic_info1 As Career  FROM tbl_career_master" +
                       "  WHERE (basic_info4 = '" + interest1 + "' or basic_info5 = '" + interest1 + "')  " +
                       " AND ca_id in (SELECT distinct ca_id FROM tbl_career_master where basic_info4 = '" + interest2 + "' or basic_info5 = '" + interest2 + "')AND Career_category IS NOT NULL)" +
                       " UNION " +
                       " (SELECT distinct A.ca_id As CareerID, B.Career_category As CareerCategory,B.Occupational_category As OccupationalCategory, B.basic_info1 As Career FROM tbl_career_personality_master AS A INNER JOIN tbl_career_master As B ON A.ca_id = B.ca_id " +
                       " where (personality1='" + Personality1 + "' OR personality2='" + Personality1 + "' OR personality3='" + Personality1 + "') " +
                       " AND A.ca_id in(SELECT [ca_id] FROM tbl_career_personality_master WHERE (personality1 = '" + Personality2 + "' or personality2 = '" + Personality2 + "' or personality3 = '" + Personality2 + "'))" +
                       " AND A.ca_id in  (SELECT [ca_id] FROM tbl_career_personality_master WHERE (personality1 = '" + Personality3 + "' or personality2 = '" + Personality3 + "' or personality3 = '" + Personality3 + "'))" +
                       " AND B.Career_category IS NOT NULL) order by B.Career_category";

                    cmd = new SqlCommand(CombinedQuery, con);
                    //con.Open();
                    List<CareerToolsCombined> lctc = new List<CareerToolsCombined>();
                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            CareerToolsCombined ct = new CareerToolsCombined();
                            ct.caId = Convert.ToInt32(dr["CareerID"]);
                            ct.CareerCategory = dr["CareerCategory"].ToString();
                            ct.OccupationalCategory = dr["OccupationalCategory"].ToString();
                            ct.Career = dr["Career"].ToString();
                            ctm.lctc.Add(ct);
                        }

                       // return lct;
                    }
                    else
                    {
                        //return null;
                    }
                    dr.Close();

                    //Query For Ability Partial

                    string AbilityPartialQuery = " SELECT distinct A.ca_id As CareerID, B.Career_category As CareerCategory,B.Occupational_category As OccupationalCategory,B.basic_info1 As Career " +
                         " FROM tbl_career_ability_master AS A INNER JOIN tbl_career_master As B ON A.ca_id = B.ca_id WHERE A.ca_id in " +
                         " ( SELECT distinct ca_id FROM tbl_career_ability_master where (ability1 = '" + ability1 + "' or ability2 = '" + ability1 + "' or ability3 = '" + ability1 + "') " +
                         " AND A.ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where ability1 = '" + ability2 + "' or ability2 = '" + ability2 + "' or ability3 = '" + ability2 + "') " +
                         " AND B.Career_category IS NOT NULL" +
                         " UNION " +
                         " SELECT distinct ca_id FROM tbl_career_ability_master where (ability1 = '" + ability1 + "' or ability2 = '" + ability1 + "' or ability3 = '" + ability1 + "') " +
                         " AND A.ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where ability1 = '" + ability3 + "' or ability2 = '" + ability3 + "' or ability3 = '" + ability3 + "') " +
                         " AND B.Career_category IS NOT NULL" +
                         " UNION " +
                         " SELECT distinct ca_id FROM tbl_career_ability_master where (ability1 = '" + ability2 + "' or ability2 = '" + ability2 + "' or ability3 = '" + ability2 + "') " +
                         " AND ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where ability1 = '" + ability3 + "' or ability2 = '" + ability3 + "' or ability3 = '" + ability3 + "') " +
                         " AND B.Career_category IS NOT NULL) " +
                         " AND A.ca_id not in ( SELECT [ca_id] FROM tbl_career_ability_master where (ability1 = '" + ability1 + "' or ability2 = '" + ability1 + "' or ability3 = '" + ability1 + "')" +
                         " AND ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where (ability1 = '" + ability2 + "' or ability2 = '" + ability2 + "' or ability3 = '" + ability2 + "') " +
                         " AND ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where ability1 = '" + ability3 + "' or ability2 = '" + ability3 + "' or ability3 = '" + ability3 + "') ) " +
                         " AND B.Career_category IS NOT NULL)";

                    cmd = new SqlCommand(AbilityPartialQuery, con);
                    //con.Open();
                    List<CareerToolsAbility> lctap = new List<CareerToolsAbility>();
                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            CareerToolsAbility ct = new CareerToolsAbility();
                            ct.caId = Convert.ToInt32(dr["CareerID"]);
                            ct.CareerCategory = dr["CareerCategory"].ToString();
                            ct.OccupationalCategory = dr["OccupationalCategory"].ToString();
                            ct.Career = dr["Career"].ToString();
                            ctm.lctap.Add(ct);
                        }

                      //  return lct;
                    }
                    else
                    {
                        //return null;
                    }
                    dr.Close();
                    //Query For Interest

                    string InterestPartialQuery = " SELECT distinct ca_id As CareerID ,Career_category As CareerCategory,Occupational_category As OccupationalCategory,basic_info1 As Career  FROM tbl_career_master where ca_id in " +
                         " (SELECT distinct ca_id FROM tbl_career_master where (basic_info4 = '" + interest1 + "' or basic_info5 = '" + interest1 + "')" +
                         " OR ca_id in (SELECT distinct ca_id FROM tbl_career_master where basic_info4 = '" + interest2 + "' or basic_info5 = '" + interest2 + "') )" +
                         " AND ca_id not in " +
                         " (SELECT distinct ca_id FROM tbl_career_master where (basic_info4 = '" + interest1 + "' or basic_info5 = '" + interest1 + "')" +
                         " AND ca_id in (SELECT distinct ca_id FROM tbl_career_master where basic_info4 = '" + interest2 + "' or basic_info5 = '" + interest2 + "' )	)" +
                         " AND Career_category IS NOT NULL";

                    cmd = new SqlCommand(InterestPartialQuery, con);
                    //con.Open();
                    List<CareerToolsInterest> lctip = new List<CareerToolsInterest>();
                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            CareerToolsInterest ct = new CareerToolsInterest();
                            ct.caId = Convert.ToInt32(dr["CareerID"]);
                            ct.CareerCategory = dr["CareerCategory"].ToString();
                            ct.OccupationalCategory = dr["OccupationalCategory"].ToString();
                            ct.Career = dr["Career"].ToString();
                            ctm.lctip.Add(ct);
                        }

                       // return lct;
                    }
                    else
                    {
                       
                    }
                    dr.Close();
                    //Query For Personality

                    string PersonalityPartialQuery = " SELECT distinct A.ca_id As CareerID, B.Career_category As CareerCategory,B.Occupational_category As OccupationalCategory,B.basic_info1 As Career FROM tbl_career_personality_master AS A " +
                         " INNER JOIN tbl_career_master As B    ON A.ca_id = B.ca_id where (personality1='" + Personality1 + "' OR personality2='" + Personality1 + "' OR personality3='" + Personality1 + "') " +
                         " AND A.ca_id in(SELECT [ca_id] FROM tbl_career_personality_master where  (personality1 = '" + Personality2 + "' or personality2 = '" + Personality2 + "' or personality3 = '" + Personality2 + "')) " +
                         " AND A.ca_id in " +
                         " (SELECT [ca_id] FROM tbl_career_personality_master where  (personality1 = '" + Personality3 + "' or personality2 = '" + Personality3 + "' or personality3 = '" + Personality3 + "') )" +
                         " AND B.Career_category IS NOT NULL";

                    cmd = new SqlCommand(PersonalityPartialQuery, con);
                    //con.Open();
                    List<CareerToolsPersonality> lctpp = new List<CareerToolsPersonality>();
                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            CareerToolsPersonality ct = new CareerToolsPersonality();
                            ct.caId = Convert.ToInt32(dr["CareerID"]);
                            ct.CareerCategory = dr["CareerCategory"].ToString();
                            ct.OccupationalCategory = dr["OccupationalCategory"].ToString();
                            ct.Career = dr["Career"].ToString();
                            ctm.lctpp.Add(ct);
                        }

                        //return lct;
                    }
                    else
                    {
                        //return null;
                    }
                    dr.Close();
                    //Query For Combined OF ABILITY Interest And Personality

                    string CombinedPartialQuery = " SELECT distinct A.ca_id As CareerID, B.Career_category As CareerCategory,B.Occupational_category As OccupationalCategory,B.basic_info1 As Career " +
                       " FROM tbl_career_ability_master AS A INNER JOIN tbl_career_master As B ON A.ca_id = B.ca_id WHERE A.ca_id in " +
                       " ( SELECT distinct ca_id FROM tbl_career_ability_master where (ability1 = '" + ability1 + "' or ability2 = '" + ability1 + "' or ability3 = '" + ability1 + "') " +
                       " AND A.ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where ability1 = '" + ability2 + "' or ability2 = '" + ability2 + "' or ability3 = '" + ability2 + "') " +
                       " AND B.Career_category IS NOT NULL" +
                       " UNION " +
                       " SELECT distinct ca_id FROM tbl_career_ability_master where (ability1 = '" + ability1 + "' or ability2 = '" + ability1 + "' or ability3 = '" + ability1 + "') " +
                       " AND A.ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where ability1 = '" + ability3 + "' or ability2 = '" + ability3 + "' or ability3 = '" + ability3 + "') " +
                       " AND B.Career_category IS NOT NULL" +
                       " UNION " +
                       " SELECT distinct ca_id FROM tbl_career_ability_master where (ability1 = '" + ability2 + "' or ability2 = '" + ability2 + "' or ability3 = '" + ability2 + "') " +
                       " AND ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where ability1 = '" + ability3 + "' or ability2 = '" + ability3 + "' or ability3 = '" + ability3 + "') " +
                       " AND B.Career_category IS NOT NULL) " +
                       " AND A.ca_id not in ( SELECT [ca_id] FROM tbl_career_ability_master where (ability1 = '" + ability1 + "' or ability2 = '" + ability1 + "' or ability3 = '" + ability1 + "')" +
                       " AND ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where (ability1 = '" + ability2 + "' or ability2 = '" + ability2 + "' or ability3 = '" + ability2 + "') " +
                       " AND ca_id in (SELECT [ca_id] FROM tbl_career_ability_master where ability1 = '" + ability3 + "' or ability2 = '" + ability3 + "' or ability3 = '" + ability3 + "') ) " +
                       " AND B.Career_category IS NOT NULL)" +
                       " UNION " +
                       "(SELECT distinct ca_id ,Career_category As CareerCategory,Occupational_category As OccupationalCategory,basic_info1 As Career  FROM tbl_career_master where ca_id in " +
                       " (SELECT distinct ca_id FROM tbl_career_master where (basic_info4 = '" + interest1 + "' or basic_info5 = '" + interest1 + "')" +
                       " OR ca_id in (SELECT distinct ca_id FROM tbl_career_master where basic_info4 = '" + interest2 + "' or basic_info5 = '" + interest2 + "') )" +
                       " AND ca_id not in " +
                       " (SELECT distinct ca_id FROM tbl_career_master where (basic_info4 = '" + interest1 + "' or basic_info5 = '" + interest1 + "')" +
                       " AND ca_id in (SELECT distinct ca_id FROM tbl_career_master where basic_info4 = '" + interest2 + "' or basic_info5 = '" + interest2 + "' )	)" +
                       " AND Career_category IS NOT NULL)" +
                       " UNION " +
                       "(SELECT distinct A.ca_id As CareerID, B.Career_category As CareerCategory,B.Occupational_category As OccupationalCategory,B.basic_info1 As Career FROM tbl_career_personality_master AS A " +
                       " INNER JOIN tbl_career_master As B    ON A.ca_id = B.ca_id where (personality1='" + Personality1 + "' OR personality2='" + Personality1 + "' OR personality3='" + Personality1 + "') " +
                       " AND A.ca_id in(SELECT [ca_id] FROM tbl_career_personality_master where  (personality1 = '" + Personality2 + "' or personality2 = '" + Personality2 + "' or personality3 = '" + Personality2 + "')) " +
                       " AND A.ca_id in " +
                       " (SELECT [ca_id] FROM tbl_career_personality_master where  (personality1 = '" + Personality3 + "' or personality2 = '" + Personality3 + "' or personality3 = '" + Personality3 + "') )" +
                       " AND B.Career_category IS NOT NULL)";

                    cmd = new SqlCommand(CombinedPartialQuery, con);
                    //con.Open();
                    List<CareerToolsCombined> lctcp = new List<CareerToolsCombined>();
                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            CareerToolsCombined ct = new CareerToolsCombined();
                            ct.caId = Convert.ToInt32(dr["CareerID"]);
                            ct.CareerCategory = dr["CareerCategory"].ToString();
                            ct.OccupationalCategory = dr["OccupationalCategory"].ToString();
                            ct.Career = dr["Career"].ToString();
                            ctm.lctcp.Add(ct);
                        }

                        //return lct;
                    }
                    else
                    {
                        //return null;
                    }
                    return ctm;

                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return null;
            }

        }

        public IEnumerable<LineOfEductToCareer> GetlineOfEductCareer(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString1))
                {
                 string strcmd = "SELECT A.co_id, A.course1 FROM tbl_course_master A, tbl_career_course_bridge B ";
                    strcmd += " Where A.co_id = B.co_id AND B.ca_id = "+id;
                    SqlCommand cmd = new SqlCommand(strcmd, con);
                    con.Open();
                    List<LineOfEductToCareer> li = new List<LineOfEductToCareer>();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            LineOfEductToCareer LC = new LineOfEductToCareer();
                            LC.co_id = Convert.ToInt32(sdr["co_id"]);
                            LC.course1 = sdr["course1"].ToString();
                            li.Add(LC);
                        }
                        return li;
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

        public List<CareerToolsRAPD> GetCareerByRAPD(string Rscore, string Ascore, string Pscore, string Dscore)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    //Select career by ability from selected values by user
                    string RAPDQuery = "SELECT A.ca_id as cId,rScore,aScore,pScore,dScore,Career_category As CareerCategory,Occupational_category As OccupationalCategory,basic_info1 As Career from tbl_career_master as A " +
                   "inner join (SELECT ca_id, rScore, aScore, pScore, dScore FROM tblCareerRAPD where (rScore = '" + Rscore + "' and aScore = '" + Ascore + "' and pScore = '" + Pscore + "' and dScore = '" + Dscore + "')) " +
                   "as B on A.ca_id = B.ca_id where A.ca_id>1 ";

                    //if (careerCategory != null)
                    //{
                    //    RAPDQuery += " and A.Career_category='" + careerCategory + "'";
                    //}
                    //if (OccupationalCategory != null)
                    //{
                    //    RAPDQuery += " and A.Occupational_category='" + OccupationalCategory + "'";
                    //}

                    SqlCommand cmd = new SqlCommand(RAPDQuery, con);
                    con.Open();

                    List<CareerToolsRAPD> lcrapd = new List<CareerToolsRAPD>();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            CareerToolsRAPD ctrapd = new CareerToolsRAPD();
                            ctrapd.CareerCategory = dr["CareerCategory"].ToString();
                            ctrapd.OccupationalCategory = dr["OccupationalCategory"].ToString();
                            ctrapd.Career = dr["Career"].ToString();
                            ctrapd.caId = Convert.ToInt32(dr["cId"]);
                            lcrapd.Add(ctrapd);
                        }
                        return lcrapd;
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


        public List<CareerTools> GetOccupationalCategory()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("spCDFOccupationalcategory", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    List<CareerTools> lct = new List<CareerTools>();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            CareerTools ct = new CareerTools();
                            ct.OccupationalCategory = dr["Occupationalcategory"].ToString();
                            lct.Add(ct);
                        }
                        return lct;
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

        public List<CareerTools> GetCareerCategory(string OccupationalCategory)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("spCDFCareercategory", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@OccupationalCategory", OccupationalCategory);
                    con.Open();
                    List<CareerTools> lct = new List<CareerTools>();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            CareerTools ct = new CareerTools();
                            ct.CareerCategory = dr["Careercategory"].ToString();
                            lct.Add(ct);
                        }
                        return lct;
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

        public CareerDetails GetCareerDetails(int id)
        {
            try
            {
                CareerDetails cd = new CareerDetails();
                using (SqlConnection con = new SqlConnection(connectionString1))
                {

                    SqlCommand cmd = new SqlCommand("spCDFCareerDetails1", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    con.Open();

                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        dr.Read();

                        string IndustriesYouWork1 = "", IndustriesYouWork2 = "", IndustriesYouWork3 = "";

                        if (dr["basic_info8"].ToString() != "NULL" && dr["basic_info8"].ToString() != "")
                        {
                            IndustriesYouWork1 = dr["basic_info8"].ToString();
                        }
                        if (dr["basic_info9"].ToString() != "NULL" && dr["basic_info9"].ToString() != "")
                        {
                            IndustriesYouWork2 = dr["basic_info9"].ToString();
                        }
                        if (dr["basic_info10"].ToString() != "NULL" && dr["basic_info10"].ToString() != "")
                        {
                            IndustriesYouWork3 = dr["basic_info10"].ToString();
                        }
                        cd.SectorYouWork = dr["basic_info2"].ToString();
                        cd.CareerAllAbout = dr["basic_info3"].ToString();
                        cd.IndustriesYouWork = dr["basic_info7"].ToString() + ", " + IndustriesYouWork1 + ", " + IndustriesYouWork2 + ", " + IndustriesYouWork3;
                        cd.ProfessionalInterests = dr["basic_info4"].ToString() + ", " + dr["basic_info5"].ToString();
                        cd.CareerCategory = dr["basic_info6"].ToString();
                        cd.FutureRelevance = dr["Career_scope"].ToString();
                        dr.Close();
                    }

                        cmd = new SqlCommand("spCDFCareerDetails2", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", id);
                        //con.Open();

                        dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {

                        dr.Read();
                        string JobFocus1 = "", JobFocus2 = "", JobFocus3 = "", JobFocus4 = "", JobFocus5 = "", JobFocus6 = "", JobFocus7 = "", JobFocus8 = "";

                        if (dr["job_focus1"].ToString() != "NULL" && dr["job_focus1"].ToString() != "")
                        {
                            JobFocus1 = dr["job_focus1"].ToString();
                        }
                        if (dr["job_focus2"].ToString() != "NULL" && dr["job_focus2"].ToString() != "")
                        {
                            JobFocus2 = dr["job_focus2"].ToString();
                        }
                        if (dr["job_focus3"].ToString() != "NULL" && dr["job_focus3"].ToString() != "")
                        {
                            JobFocus3 = dr["job_focus3"].ToString();
                        }
                        if (dr["job_focus4"].ToString() != "NULL" && dr["job_focus4"].ToString() != "")
                        {
                            JobFocus4 = dr["job_focus4"].ToString();
                        }
                        if (dr["job_focus5"].ToString() != "NULL" && dr["job_focus5"].ToString() != "")
                        {
                            JobFocus5 = dr["job_focus5"].ToString();
                        }
                        if (dr["job_focus6"].ToString() != "NULL" && dr["job_focus6"].ToString() != "")
                        {
                            JobFocus6 = dr["job_focus6"].ToString();
                        }
                        if (dr["job_focus7"].ToString() != "NULL" && dr["job_focus7"].ToString() != "")
                        {
                            JobFocus7 = dr["job_focus8"].ToString();
                        }
                        if (dr["job_focus8"].ToString() != "NULL" && dr["job_focus8"].ToString() != "")
                        {
                            JobFocus8 = dr["job_focus8"].ToString();
                        }
                        cd.JobYouDo = dr["job_focus1"].ToString() + ", " + JobFocus2 + ", " + JobFocus3 + ", " + JobFocus4 + ", " + JobFocus5 + ", " + JobFocus6 + ", " + JobFocus7 + ", " + JobFocus8;
                        cd.CareerAdvancementOpportunities = dr["career_advancement1"].ToString();
                        cd.WorkingOfficeEnvironment = dr["work_environment1"].ToString();
                        cd.MobilityTravel = dr["work_environment2"].ToString();
                        cd.WorkingWithEquipment = dr["work_environment3"].ToString();
                        cd.WorkingWithComputers = dr["work_environment4"].ToString();
                        cd.HighRiskEnvironment = dr["work_environment5"].ToString();
                        cd.LongHours = dr["work_environment6"].ToString();
                        cd.WorkingAtNight = dr["work_environment7"].ToString();
                        cd.WorkingWithHightech = dr["work_environment8"].ToString();
                        cd.WorkingCloselyWithPeople = dr["work_environment9"].ToString();
                        cd.SittingAtOnePlace = dr["work_environment10"].ToString();

                        cd.ResultOriented = dr["work_context1"].ToString();
                        cd.ShortTermTimelineProjects = dr["work_context2"].ToString();
                        cd.SettingGoalsForYourselfAndTeam = dr["work_context3"].ToString();
                        cd.HighDecisionMakingNeeds = dr["work_context4"].ToString();
                        cd.HighProblemSolvingNeeds = dr["work_context5"].ToString();
                        cd.PeopleToPeopleInteraction1 = dr["work_context6"].ToString();
                        cd.WorkingInTeams = dr["work_context7"].ToString();
                        cd.LeadingTeam = dr["work_context8"].ToString();
                        cd.IndividualWorkingEnvironment = dr["work_context9"].ToString();
                        cd.RepetitiveJobsAndRoutine = dr["work_context10"].ToString();
                        cd.WorkingHigherQualityAccuracyRequirements = dr["work_context11"].ToString();
                        cd.DesigningProcesses = dr["work_context12"].ToString();
                        cd.WorkingWithProcesses = dr["work_context13"].ToString();

                        cd.WorkingWithEmails = dr["work_comm_demands1"].ToString();
                        cd.PeopleToPeopleInteraction2 = dr["work_comm_demands2"].ToString();
                        cd.VerbalCommunicationWithPeopleMasses = dr["work_comm_demands3"].ToString();
                        cd.WrittenCommunicationExpression = dr["work_comm_demands4"].ToString();
                        cd.NonVerbalCommunication = dr["work_comm_demands5"].ToString();

                        cd.NumericalAbility = dr["mental_abilities1"].ToString();
                        cd.VerbalAbility = dr["mental_abilities2"].ToString();
                        cd.WrittenComprehension = dr["mental_abilities3"].ToString();
                        cd.WrittenExpression = dr["mental_abilities4"].ToString();
                        cd.VerbalComprehension = dr["mental_abilities5"].ToString();
                        cd.VerbalExpression = dr["mental_abilities6"].ToString();
                        cd.LogicalThinking = dr["mental_abilities7"].ToString();
                        cd.AnalyticalThinking = dr["mental_abilities8"].ToString();
                        cd.ProblemSensitivityAndSolving = dr["mental_abilities9"].ToString();
                        cd.SpaceRelations = dr["mental_abilities10"].ToString();
                        cd.AbstractReasoning = dr["mental_abilities11"].ToString();
                        cd.Visualization = dr["mental_abilities12"].ToString();
                        cd.CreativeImaginationOriginality = dr["mental_abilities13"].ToString();
                        cd.MemoryRecall = dr["mental_abilities14"].ToString();

                        cd.FingerHandSwiftness = dr["physical_abilities1"].ToString();
                        cd.Hearing = dr["physical_abilities2"].ToString();
                        cd.Vision = dr["physical_abilities3"].ToString();
                        cd.NightVision = dr["physical_abilities4"].ToString();
                        cd.StaminaFitness = dr["physical_abilities5"].ToString();
                        cd.HandSteadiness = dr["physical_abilities6"].ToString();
                        cd.GrossBodyCoordination = dr["physical_abilities7"].ToString();
                        cd.WorkOfficeEnvtIndoors = dr["work_environment1"].ToString();

                        return cd;
                    }
                    else
                    {
                        return null;
                    }


                    //cmd = new SqlCommand("spCDFCareerDetails3", con);
                    //cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.AddWithValue("@id", id);

                    //dr = cmd.ExecuteReader();
                    //if (dr.HasRows)
                    //{
                    //    cd.ListOfCourses = "";
                    //}

                    //cmd = new SqlCommand("spCDFCareerDetails3", con);
                    //cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.AddWithValue("@id", id);

                    //dr = cmd.ExecuteReader();
                    //if (dr.HasRows)
                    //{



                    //    return cd;
                    //    }
                    //    else
                    //    {
                    //        return null;
                    //    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("" + ex);
                return null;
            }
        }

        //new methods by Bhagyashree
        public List<DDLAbility> GetAbility_DDL()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SP_Dropdowns_Ability", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Type", "Ability1");
                    cmd.Parameters.AddWithValue("@Ability1", "");
                    cmd.Parameters.AddWithValue("@Ability2", "");
                    con.Open();
                    List<DDLAbility> li = new List<DDLAbility>();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            DDLAbility abi = new DDLAbility();
                            abi.ability = dr["ability1"].ToString();
                            li.Add(abi);
                        }
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

        public List<DDLAbility> GetAbility_DDL2(string Ability1)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SP_Dropdowns_Ability", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Type", "Ability2");
                    cmd.Parameters.AddWithValue("@Ability1", Ability1);
                    cmd.Parameters.AddWithValue("@Ability2", "");
                    con.Open();
                    List<DDLAbility> li = new List<DDLAbility>();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            DDLAbility abi = new DDLAbility();
                            abi.ability = dr["ability1"].ToString();
                            li.Add(abi);
                        }
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

        public List<DDLAbility> GetAbility_DDL3(string Ability1, string Ability2)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SP_Dropdowns_Ability", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Type", "Ability3");
                    cmd.Parameters.AddWithValue("@Ability1", Ability1);
                    cmd.Parameters.AddWithValue("@Ability2", Ability2);
                    con.Open();
                    List<DDLAbility> li = new List<DDLAbility>();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            DDLAbility abi = new DDLAbility();
                            abi.ability = dr["ability1"].ToString();
                            li.Add(abi);
                        }
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

        public List<DDLInterest> GetInterest_DDL()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString1))
                {
                    SqlCommand cmd = new SqlCommand("SP_Dropdowns_Interest", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Type", "Interest1");
                    cmd.Parameters.AddWithValue("@Interest1", "");
                    con.Open();
                    List<DDLInterest> li = new List<DDLInterest>();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            DDLInterest inte = new DDLInterest();
                            inte.factorNo = Convert.ToInt32(dr["factor_no"]);
                            inte.factor = dr["factor"].ToString();
                            li.Add(inte);
                        }
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

        public List<DDLInterest> GetInterest_DDL2(int factorno)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString1))
                {
                    SqlCommand cmd = new SqlCommand("SP_Dropdowns_Interest", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Type", "Interest2");
                    cmd.Parameters.AddWithValue("@Interest1", factorno);
                    con.Open();
                    List<DDLInterest> li = new List<DDLInterest>();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            DDLInterest inte = new DDLInterest();
                            inte.factorNo = Convert.ToInt32(dr["factor_no"]);
                            inte.factor = dr["factor"].ToString();
                            li.Add(inte);
                        }
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

        //Code by bhagyashree
        public List<CareerToolsAbility> GetTotalCompa_Ability(string ability1, string ability2, string ability3)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    List<CareerToolsAbility> li = new List<CareerToolsAbility>();

                    SqlCommand cmd = new SqlCommand("SP_AbilityInterestRAPDfilter", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Type", "TotalCompa_Ability");
                    cmd.Parameters.AddWithValue("@ability1", ability1);
                    cmd.Parameters.AddWithValue("@ability2", ability2);
                    cmd.Parameters.AddWithValue("@ability3", ability3);
                    cmd.Parameters.AddWithValue("@interest1", "");
                    cmd.Parameters.AddWithValue("@interest2", "");
                    cmd.Parameters.AddWithValue("@R", "");
                    cmd.Parameters.AddWithValue("@A", "");
                    cmd.Parameters.AddWithValue("@P", "");
                    cmd.Parameters.AddWithValue("@D", "");
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            CareerToolsAbility obj = new CareerToolsAbility();
                            obj.caId = Convert.ToInt32(dr["CareerID"]);
                            obj.CareerCategory = dr["CareerCategory"].ToString();
                            obj.OccupationalCategory = dr["OccupationalCategory"].ToString();
                            obj.Career = dr["Career"].ToString();
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

        public List<CareerToolsInterest> GetTotalCompa_Interest(string Interest1, string Interest2)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    List<CareerToolsInterest> li = new List<CareerToolsInterest>();

                    SqlCommand cmd = new SqlCommand("SP_AbilityInterestRAPDfilter", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Type", "TotalCompa_Interest");
                    cmd.Parameters.AddWithValue("@ability1", "");
                    cmd.Parameters.AddWithValue("@ability2", "");
                    cmd.Parameters.AddWithValue("@ability3", "");
                    cmd.Parameters.AddWithValue("@interest1", Interest1);
                    cmd.Parameters.AddWithValue("@interest2", Interest2);
                    cmd.Parameters.AddWithValue("@R", "");
                    cmd.Parameters.AddWithValue("@A", "");
                    cmd.Parameters.AddWithValue("@P", "");
                    cmd.Parameters.AddWithValue("@D", "");
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            CareerToolsInterest obj = new CareerToolsInterest();
                            obj.caId = Convert.ToInt32(dr["CareerID"]);
                            obj.CareerCategory = dr["CareerCategory"].ToString();
                            obj.OccupationalCategory = dr["OccupationalCategory"].ToString();
                            obj.Career = dr["Career"].ToString();
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

        public List<CareerToolsPersonality> GetTotalCompa_RAPD(string R, string A, string P, string D)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    List<CareerToolsPersonality> li = new List<CareerToolsPersonality>();

                    SqlCommand cmd = new SqlCommand("SP_AbilityInterestRAPDfilter", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Type", "TotalCompa_RAPD");
                    cmd.Parameters.AddWithValue("@ability1", "");
                    cmd.Parameters.AddWithValue("@ability2", "");
                    cmd.Parameters.AddWithValue("@ability3", "");
                    cmd.Parameters.AddWithValue("@interest1", "");
                    cmd.Parameters.AddWithValue("@interest2", "");
                    cmd.Parameters.AddWithValue("@R", R);
                    cmd.Parameters.AddWithValue("@A", A);
                    cmd.Parameters.AddWithValue("@P", P);
                    cmd.Parameters.AddWithValue("@D", D);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            CareerToolsPersonality obj = new CareerToolsPersonality();
                            obj.caId = Convert.ToInt32(dr["CareerID"]);
                            obj.CareerCategory = dr["CareerCategory"].ToString();
                            obj.OccupationalCategory = dr["OccupationalCategory"].ToString();
                            obj.Career = dr["Career"].ToString();
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

        public List<CareerToolsCombined> GetTotalCompa_Combined(string ability1, string ability2, string ability3, string Interest1, string Interest2, string R, string A, string P, string D)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    List<CareerToolsCombined> li = new List<CareerToolsCombined>();

                    SqlCommand cmd = new SqlCommand("SP_AbilityInterestRAPDfilter", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Type", "Combined");
                    cmd.Parameters.AddWithValue("@ability1", ability1);
                    cmd.Parameters.AddWithValue("@ability2", ability2);
                    cmd.Parameters.AddWithValue("@ability3", ability3);
                    cmd.Parameters.AddWithValue("@interest1", Interest1);
                    cmd.Parameters.AddWithValue("@interest2", Interest2);
                    cmd.Parameters.AddWithValue("@R", R);
                    cmd.Parameters.AddWithValue("@A", A);
                    cmd.Parameters.AddWithValue("@P", P);
                    cmd.Parameters.AddWithValue("@D", D);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            CareerToolsCombined obj = new CareerToolsCombined();
                            obj.caId = Convert.ToInt32(dr["CareerID"]);
                            obj.CareerCategory = dr["CareerCategory"].ToString();
                            obj.OccupationalCategory = dr["OccupationalCategory"].ToString();
                            obj.Career = dr["Career"].ToString();
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


        public List<CareerToolsAbility> GetPartiallyAbility(string ability1, string ability2, string ability3)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    List<CareerToolsAbility> li = new List<CareerToolsAbility>();

                    SqlCommand cmd = new SqlCommand("SP_AbilityInterestRAPDfilter", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Type", "PartiallyAbility");
                    cmd.Parameters.AddWithValue("@ability1", ability1);
                    cmd.Parameters.AddWithValue("@ability2", ability2);
                    cmd.Parameters.AddWithValue("@ability3", ability3);
                    cmd.Parameters.AddWithValue("@interest1", "");
                    cmd.Parameters.AddWithValue("@interest2", "");
                    cmd.Parameters.AddWithValue("@R", "");
                    cmd.Parameters.AddWithValue("@A", "");
                    cmd.Parameters.AddWithValue("@P", "");
                    cmd.Parameters.AddWithValue("@D", "");
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            CareerToolsAbility obj = new CareerToolsAbility();
                            obj.caId = Convert.ToInt32(dr["CareerID"]);
                            obj.CareerCategory = dr["CareerCategory"].ToString();
                            obj.OccupationalCategory = dr["OccupationalCategory"].ToString();
                            obj.Career = dr["Career"].ToString();
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

        public List<CareerToolsInterest> GetPartiallyInterest(string Interest1, string Interest2)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    List<CareerToolsInterest> li = new List<CareerToolsInterest>();

                    SqlCommand cmd = new SqlCommand("SP_AbilityInterestRAPDfilter", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Type", "PartiallyInterest");
                    cmd.Parameters.AddWithValue("@ability1", "");
                    cmd.Parameters.AddWithValue("@ability2", "");
                    cmd.Parameters.AddWithValue("@ability3", "");
                    cmd.Parameters.AddWithValue("@interest1", Interest1);
                    cmd.Parameters.AddWithValue("@interest2", Interest2);
                    cmd.Parameters.AddWithValue("@R", "");
                    cmd.Parameters.AddWithValue("@A", "");
                    cmd.Parameters.AddWithValue("@P", "");
                    cmd.Parameters.AddWithValue("@D", "");
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            CareerToolsInterest obj = new CareerToolsInterest();
                            obj.caId = Convert.ToInt32(dr["CareerID"]);
                            obj.CareerCategory = dr["CareerCategory"].ToString();
                            obj.OccupationalCategory = dr["OccupationalCategory"].ToString();
                            obj.Career = dr["Career"].ToString();
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

        public List<CareerToolsPersonality> GetPartiallyRAPD(string R, string A, string P, string D)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    List<CareerToolsPersonality> li = new List<CareerToolsPersonality>();

                    SqlCommand cmd = new SqlCommand("SP_AbilityInterestRAPDfilter", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Type", "PartiallyRAPD");
                    cmd.Parameters.AddWithValue("@ability1", "");
                    cmd.Parameters.AddWithValue("@ability2", "");
                    cmd.Parameters.AddWithValue("@ability3", "");
                    cmd.Parameters.AddWithValue("@interest1", "");
                    cmd.Parameters.AddWithValue("@interest2", "");
                    cmd.Parameters.AddWithValue("@R", R);
                    cmd.Parameters.AddWithValue("@A", A);
                    cmd.Parameters.AddWithValue("@P", P);
                    cmd.Parameters.AddWithValue("@D", D);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            CareerToolsPersonality obj = new CareerToolsPersonality();
                            obj.caId = Convert.ToInt32(dr["CareerID"]);
                            obj.CareerCategory = dr["CareerCategory"].ToString();
                            obj.OccupationalCategory = dr["OccupationalCategory"].ToString();
                            obj.Career = dr["Career"].ToString();
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

        public List<CareerToolsCombined> GetPartiallyCombo_Abi_Int_RAPD(string ability1, string ability2, string ability3, string Interest1, string Interest2, string R, string A, string P, string D)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    List<CareerToolsCombined> li = new List<CareerToolsCombined>();

                    SqlCommand cmd = new SqlCommand("SP_AbilityInterestRAPDfilter", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Type", "PartiallyCombo_Abi_Int_RAPD");
                    cmd.Parameters.AddWithValue("@ability1", ability1);
                    cmd.Parameters.AddWithValue("@ability2", ability2);
                    cmd.Parameters.AddWithValue("@ability3", ability3);
                    cmd.Parameters.AddWithValue("@interest1", Interest1);
                    cmd.Parameters.AddWithValue("@interest2", Interest2);
                    cmd.Parameters.AddWithValue("@R", R);
                    cmd.Parameters.AddWithValue("@A", A);
                    cmd.Parameters.AddWithValue("@P", P);
                    cmd.Parameters.AddWithValue("@D", D);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            CareerToolsCombined obj = new CareerToolsCombined();
                            obj.caId = Convert.ToInt32(dr["CareerID"]);
                            obj.CareerCategory = dr["CareerCategory"].ToString();
                            obj.OccupationalCategory = dr["OccupationalCategory"].ToString();
                            obj.Career = dr["Career"].ToString();
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


        public CareerToolsModel GetTotalCompa_AbilityAndChart(string ability1, string ability2, string ability3)
        {
            try
            {
                CareerToolsModel CTM = new CareerToolsModel();
                // using (SqlConnection con = new SqlConnection(connectionString)) 
                using (SqlConnection con = new SqlConnection(connectionString1))
                {
                    // List<CareerToolsAbility> abili = new List<CareerToolsAbility>();

                    SqlCommand cmd = new SqlCommand("SP_AbilityInterestRAPDfilter", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Type", "TotalCompa_Ability");
                    cmd.Parameters.AddWithValue("@ability1", ability1);
                    cmd.Parameters.AddWithValue("@ability2", ability2);
                    cmd.Parameters.AddWithValue("@ability3", ability3);
                    cmd.Parameters.AddWithValue("@interest1", "");
                    cmd.Parameters.AddWithValue("@interest2", "");
                    cmd.Parameters.AddWithValue("@R", "");
                    cmd.Parameters.AddWithValue("@A", "");
                    cmd.Parameters.AddWithValue("@P", "");
                    cmd.Parameters.AddWithValue("@D", "");
                    con.Open();
                    //SqlDataReader dr = cmd.ExecuteReader();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            CareerToolsAbility obj = new CareerToolsAbility();
                            obj.caId = Convert.ToInt32(ds.Tables[0].Rows[i]["CareerID"]);
                            obj.CareerCategory = ds.Tables[0].Rows[i]["CareerCategory"].ToString();
                            obj.OccupationalCategory = ds.Tables[0].Rows[i]["OccupationalCategory"].ToString();
                            obj.Career = ds.Tables[0].Rows[i]["Career"].ToString();
                            obj.FutureRelavance = ds.Tables[0].Rows[i]["FutureRelavance"].ToString();
                            CTM.AbilityTable.Add(obj);
                        }

                    }
                    else { return null; }
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                        {
                            AbilityChart obj1 = new AbilityChart();
                            obj1.CareerCategory = ds.Tables[1].Rows[i]["CareerCategory"].ToString();
                            obj1.CategoryCount = Convert.ToInt32(ds.Tables[1].Rows[i]["CategoryCount"]);
                            obj1.Compatibility = Convert.ToDouble(ds.Tables[1].Rows[i]["Compatibility"]);

                            CTM.AbilityChart.Add(obj1);
                        }
                    }
                    else
                    {
                        return null;
                    }
                    con.Close();
                    return CTM;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return null;
            }
        }

        public CareerToolsModel GetTotalCompa_InterestAndChart(string Interest1, string Interest2)
        {
            try
            {
                //using (SqlConnection con = new SqlConnection(connectionString))
                using (SqlConnection con = new SqlConnection(connectionString1))
                {
                    CareerToolsModel CTM = new Models.CareerToolsModel();

                    SqlCommand cmd = new SqlCommand("SP_AbilityInterestRAPDfilter", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Type", "TotalCompa_Interest");
                    cmd.Parameters.AddWithValue("@ability1", "");
                    cmd.Parameters.AddWithValue("@ability2", "");
                    cmd.Parameters.AddWithValue("@ability3", "");
                    cmd.Parameters.AddWithValue("@interest1", Interest1);
                    cmd.Parameters.AddWithValue("@interest2", Interest2);
                    cmd.Parameters.AddWithValue("@R", "");
                    cmd.Parameters.AddWithValue("@A", "");
                    cmd.Parameters.AddWithValue("@P", "");
                    cmd.Parameters.AddWithValue("@D", "");
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            CareerToolsInterest obj = new CareerToolsInterest();
                            obj.caId = Convert.ToInt32(ds.Tables[0].Rows[i]["CareerID"]);
                            obj.CareerCategory = ds.Tables[0].Rows[i]["CareerCategory"].ToString();
                            obj.OccupationalCategory = ds.Tables[0].Rows[i]["OccupationalCategory"].ToString();
                            obj.Career = ds.Tables[0].Rows[i]["Career"].ToString();
                            obj.FutureRelavance = ds.Tables[0].Rows[i]["FutureRelavance"].ToString();
                            CTM.InterestTable.Add(obj);
                        }

                    }
                    else { return null; }
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                        {
                            InterestChart obj1 = new InterestChart();
                            obj1.CareerCategory = ds.Tables[1].Rows[i]["CareerCategory"].ToString();
                            obj1.CategoryCount = Convert.ToInt32(ds.Tables[1].Rows[i]["CategoryCount"]);
                            obj1.Compatibility = Convert.ToDouble(ds.Tables[1].Rows[i]["Compatibility"]);

                            CTM.InterestChart.Add(obj1);
                        }
                    }
                    else
                    {
                        return null;
                    }
                    con.Close();
                    return CTM;

                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return null;
            }
        }

        public CareerToolsModel GetTotalCompa_RAPDAndChart(string R, string A, string P, string D)
        {
            try
            {
                // using (SqlConnection con = new SqlConnection(connectionString))
                using (SqlConnection con = new SqlConnection(connectionString1))
                {
                    CareerToolsModel CTM = new CareerToolsModel();

                    SqlCommand cmd = new SqlCommand("SP_AbilityInterestRAPDfilter", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Type", "TotalCompa_RAPD");
                    cmd.Parameters.AddWithValue("@ability1", "");
                    cmd.Parameters.AddWithValue("@ability2", "");
                    cmd.Parameters.AddWithValue("@ability3", "");
                    cmd.Parameters.AddWithValue("@interest1", "");
                    cmd.Parameters.AddWithValue("@interest2", "");
                    cmd.Parameters.AddWithValue("@R", R);
                    cmd.Parameters.AddWithValue("@A", A);
                    cmd.Parameters.AddWithValue("@P", P);
                    cmd.Parameters.AddWithValue("@D", D);
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            CareerToolsRAPD obj = new CareerToolsRAPD();
                            obj.caId = Convert.ToInt32(ds.Tables[0].Rows[i]["CareerID"]);
                            obj.CareerCategory = ds.Tables[0].Rows[i]["CareerCategory"].ToString();
                            obj.OccupationalCategory = ds.Tables[0].Rows[i]["OccupationalCategory"].ToString();
                            obj.Career = ds.Tables[0].Rows[i]["Career"].ToString();
                            obj.FutureRelavance = ds.Tables[0].Rows[i]["FutureRelavance"].ToString();
                            CTM.RAPDTable.Add(obj);
                        }

                    }
                    else { return null; }
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                        {
                            RAPDChart obj1 = new RAPDChart();
                            obj1.CareerCategory = ds.Tables[1].Rows[i]["CareerCategory"].ToString();
                            obj1.CategoryCount = Convert.ToInt32(ds.Tables[1].Rows[i]["CategoryCount"]);
                            obj1.Compatibility = Convert.ToDouble(ds.Tables[1].Rows[i]["Compatibility"]);

                            CTM.RAPDChart.Add(obj1);
                        }
                    }
                    else
                    {
                        return null;
                    }
                    con.Close();
                    return CTM;

                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return null;
            }
        }

        public CareerToolsModel GetTotalCompa_CombinedAndChart(string ability1, string ability2, string ability3, string Interest1, string Interest2, string R, string A, string P, string D)
        {
            try
            {
                // using (SqlConnection con = new SqlConnection(connectionString))
                using (SqlConnection con = new SqlConnection(connectionString1))
                {
                    CareerToolsModel CTM = new CareerToolsModel();

                    SqlCommand cmd = new SqlCommand("SP_AbilityInterestRAPDfilter", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Type", "Combined");
                    cmd.Parameters.AddWithValue("@ability1", ability1);
                    cmd.Parameters.AddWithValue("@ability2", ability2);
                    cmd.Parameters.AddWithValue("@ability3", ability3);
                    cmd.Parameters.AddWithValue("@interest1", Interest1);
                    cmd.Parameters.AddWithValue("@interest2", Interest2);
                    cmd.Parameters.AddWithValue("@R", R);
                    cmd.Parameters.AddWithValue("@A", A);
                    cmd.Parameters.AddWithValue("@P", P);
                    cmd.Parameters.AddWithValue("@D", D);
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            CareerToolsCombined obj = new CareerToolsCombined();


                            obj.caId = Convert.ToInt32(ds.Tables[0].Rows[i]["CareerID"]);
                            obj.CareerCategory = ds.Tables[0].Rows[i]["CareerCategory"].ToString();
                            obj.OccupationalCategory = ds.Tables[0].Rows[i]["OccupationalCategory"].ToString();
                            obj.Career = ds.Tables[0].Rows[i]["Career"].ToString();
                            obj.FutureRelavance = ds.Tables[0].Rows[i]["FutureRelavance"].ToString();
                            CTM.CombinedTable.Add(obj);
                        }

                    }
                    else { return null; }
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                        {
                            CombinedChart obj1 = new CombinedChart();
                            obj1.CareerCategory = ds.Tables[1].Rows[i]["CareerCategory"].ToString();
                            obj1.CategoryCount = Convert.ToInt32(ds.Tables[1].Rows[i]["CategoryCount"]);
                            obj1.Compatibility = Convert.ToDouble(ds.Tables[1].Rows[i]["Compatibility"]);

                            CTM.CombinedChart.Add(obj1);
                        }
                    }
                    else
                    {
                        return null;
                    }
                    con.Close();
                    return CTM;

                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return null;
            }
        }

        public CareerToolsModel GetTotalCompa_AbilityInterestAndChart(string ability1, string ability2, string ability3, string Interest1, string Interest2)
        {
            try
            {
                CareerToolsModel CTM = new CareerToolsModel();
                // using (SqlConnection con = new SqlConnection(connectionString)) 
                using (SqlConnection con = new SqlConnection(connectionString1))
                {
                    SqlCommand cmd = new SqlCommand("SP_AbilityInterestRAPDfilter", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Type", "CombinedAbilityInterest");
                    cmd.Parameters.AddWithValue("@ability1", ability1);
                    cmd.Parameters.AddWithValue("@ability2", ability2);
                    cmd.Parameters.AddWithValue("@ability3", ability3);
                    cmd.Parameters.AddWithValue("@interest1", Interest1);
                    cmd.Parameters.AddWithValue("@interest2", Interest2);
                    cmd.Parameters.AddWithValue("@R", "");
                    cmd.Parameters.AddWithValue("@A", "");
                    cmd.Parameters.AddWithValue("@P", "");
                    cmd.Parameters.AddWithValue("@D", "");
                    con.Open();
                    //SqlDataReader dr = cmd.ExecuteReader();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            CareerToolsAbility_Interest obj = new CareerToolsAbility_Interest();
                            obj.caId = Convert.ToInt32(ds.Tables[0].Rows[i]["CareerID"]);
                            obj.CareerCategory = ds.Tables[0].Rows[i]["CareerCategory"].ToString();
                            obj.OccupationalCategory = ds.Tables[0].Rows[i]["OccupationalCategory"].ToString();
                            obj.Career = ds.Tables[0].Rows[i]["Career"].ToString();
                            obj.FutureRelavance = ds.Tables[0].Rows[i]["FutureRelavance"].ToString();
                            CTM.Ability_InterestTable.Add(obj);
                        }

                    }
                    else { return null; }
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                        {
                            Ability_InterestChart obj1 = new Ability_InterestChart();
                            obj1.CareerCategory = ds.Tables[1].Rows[i]["CareerCategory"].ToString();
                            obj1.CategoryCount = Convert.ToInt32(ds.Tables[1].Rows[i]["CategoryCount"]);
                            obj1.Compatibility = Convert.ToDouble(ds.Tables[1].Rows[i]["Compatibility"]);

                            CTM.Ability_InterestChart.Add(obj1);
                        }
                    }
                    else
                    {
                        return null;
                    }
                    con.Close();
                    return CTM;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return null;
            }
        }
        public CareerToolsModel GetTotalCompa_AbilityRAPDAndChart(string ability1, string ability2, string ability3, string R, string A, string P, string D)
        {
            try
            {
                CareerToolsModel CTM = new CareerToolsModel();
                // using (SqlConnection con = new SqlConnection(connectionString)) 
                using (SqlConnection con = new SqlConnection(connectionString1))
                {
                    SqlCommand cmd = new SqlCommand("SP_AbilityInterestRAPDfilter", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Type", "CombinedAbilityRAPD");
                    cmd.Parameters.AddWithValue("@ability1", ability1);
                    cmd.Parameters.AddWithValue("@ability2", ability2);
                    cmd.Parameters.AddWithValue("@ability3", ability3);
                    cmd.Parameters.AddWithValue("@interest1", "");
                    cmd.Parameters.AddWithValue("@interest2", "");
                    cmd.Parameters.AddWithValue("@R", R);
                    cmd.Parameters.AddWithValue("@A", A);
                    cmd.Parameters.AddWithValue("@P", P);
                    cmd.Parameters.AddWithValue("@D", D);
                    con.Open();
                    //SqlDataReader dr = cmd.ExecuteReader();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            CareerToolsAbility_RAPD obj = new CareerToolsAbility_RAPD();
                            obj.caId = Convert.ToInt32(ds.Tables[0].Rows[i]["CareerID"]);
                            obj.CareerCategory = ds.Tables[0].Rows[i]["CareerCategory"].ToString();
                            obj.OccupationalCategory = ds.Tables[0].Rows[i]["OccupationalCategory"].ToString();
                            obj.Career = ds.Tables[0].Rows[i]["Career"].ToString();
                            obj.FutureRelavance = ds.Tables[0].Rows[i]["FutureRelavance"].ToString();
                            CTM.Ability_RAPDTable.Add(obj);
                        }

                    }
                    else { return null; }
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                        {
                            Ability_RAPDChart obj1 = new Ability_RAPDChart();
                            obj1.CareerCategory = ds.Tables[1].Rows[i]["CareerCategory"].ToString();
                            obj1.CategoryCount = Convert.ToInt32(ds.Tables[1].Rows[i]["CategoryCount"]);
                            obj1.Compatibility = Convert.ToDouble(ds.Tables[1].Rows[i]["Compatibility"]);

                            CTM.Ability_RAPDChart.Add(obj1);
                        }
                    }
                    else
                    {
                        return null;
                    }
                    con.Close();
                    return CTM;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return null;
            }
        }

        public CareerToolsModel GetTotalCompa_InterestRAPDAndChart(string Interest1, string Interest2, string R, string A, string P, string D)
        {
            try
            {
                CareerToolsModel CTM = new CareerToolsModel();
                // using (SqlConnection con = new SqlConnection(connectionString)) 
                using (SqlConnection con = new SqlConnection(connectionString1))
                {
                    SqlCommand cmd = new SqlCommand("SP_AbilityInterestRAPDfilter", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Type", "CombinedInterestRAPD");
                    cmd.Parameters.AddWithValue("@ability1", "");
                    cmd.Parameters.AddWithValue("@ability2", "");
                    cmd.Parameters.AddWithValue("@ability3", "");
                    cmd.Parameters.AddWithValue("@interest1", Interest1);
                    cmd.Parameters.AddWithValue("@interest2", Interest2);
                    cmd.Parameters.AddWithValue("@R", R);
                    cmd.Parameters.AddWithValue("@A", A);
                    cmd.Parameters.AddWithValue("@P", P);
                    cmd.Parameters.AddWithValue("@D", D);
                    con.Open();
                    //SqlDataReader dr = cmd.ExecuteReader();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            CareerToolsInterest_RAPD obj = new CareerToolsInterest_RAPD();
                            obj.caId = Convert.ToInt32(ds.Tables[0].Rows[i]["CareerID"]);
                            obj.CareerCategory = ds.Tables[0].Rows[i]["CareerCategory"].ToString();
                            obj.OccupationalCategory = ds.Tables[0].Rows[i]["OccupationalCategory"].ToString();
                            obj.Career = ds.Tables[0].Rows[i]["Career"].ToString();
                            obj.FutureRelavance = ds.Tables[0].Rows[i]["FutureRelavance"].ToString();
                            CTM.Interest_RAPDTable.Add(obj);
                        }

                    }
                    else { return null; }
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                        {
                            Interest_RAPDChart obj1 = new Interest_RAPDChart();
                            obj1.CareerCategory = ds.Tables[1].Rows[i]["CareerCategory"].ToString();
                            obj1.CategoryCount = Convert.ToInt32(ds.Tables[1].Rows[i]["CategoryCount"]);
                            obj1.Compatibility = Convert.ToDouble(ds.Tables[1].Rows[i]["Compatibility"]);

                            CTM.Interest_RAPDChart.Add(obj1);
                        }
                    }
                    else
                    {
                        return null;
                    }
                    con.Close();
                    return CTM;
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

