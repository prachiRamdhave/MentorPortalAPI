using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CDFAPI.Models;
using log4net;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
namespace CDFAPI.Repository
{
    public class CDFGraphRepository
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        string strcon = ConfigurationManager.ConnectionStrings["career_portalConnectionString"].ConnectionString;
        GraphDAL GD = new GraphDAL();

        int batid = 3;
        int BlackM;
        int BlackL;
        int BlueM;
        int BlueL;
        int RedM;
        int RedL;
        int GreenM;
        int GreenL;
        int Hole;
        int DiffB;
        int DiffR;        
        int DiffBl;
        int DiffG;


        public CDFGraph GetcdfGraph(int cId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    if (cId > 0)
                    {
                        CDFGraph Gr = new CDFGraph();
                        string strcmd = "SELECT fname,lname,(DATEPART(yyyy,regDateTime)-DATEPART(yyyy,dob)) as age,email FROM tblUserMaster where uId=" + cId;
                        SqlCommand cmd = new SqlCommand(strcmd, con);
                        con.Open();
                        SqlDataReader sdr = cmd.ExecuteReader();
                        if (sdr.HasRows)
                        {
                            sdr.Read();
                          
                            string name = sdr["fname"].ToString() + " " + sdr["lname"].ToString();
                            string age = sdr["age"].ToString();
                            string userName = sdr["email"].ToString();


                            Gr.name = name;
                            Gr.userName = "Username - " + userName;
                            Gr.age = "Age - " + age;
                            sdr.Close();
                            Boolean flag = GD.get_values(cId, batid);
                            if (flag)
                            {
                                BlueM = GD.BLUEM;
                                BlueL = GD.BLUEL;
                                RedM = GD.REDM;
                                RedL = GD.REDL;
                                BlackM = GD.BLACKM;
                                BlackL = GD.BLACKL;
                                GreenM = GD.GREENM;
                                GreenL = GD.GREENL;
                                Hole = GD.HOLE;

                                DiffB = BlueM - BlueL;
                                DiffR = RedM - RedL;
                                DiffBl = BlackM - BlackL;
                                DiffG = GreenM - GreenL;

                                Gr.lblBM = Convert.ToString(BlueM);
                                Gr.lblBL = Convert.ToString(BlueL);
                                Gr.lblRM = Convert.ToString(RedM);
                                Gr.lblRL = Convert.ToString(RedL);
                                Gr.lblBlM = Convert.ToString(BlackM);
                                Gr.lblBlL = Convert.ToString(BlackL);
                                Gr.lblGM = Convert.ToString(GreenM);
                                Gr.lblGL = Convert.ToString(GreenL);
                                Gr.lblDiffB = Convert.ToString(DiffB);
                                Gr.lblDiffR = Convert.ToString(DiffR);
                                Gr.lblDiffBl = Convert.ToString(DiffBl);
                                Gr.lblDiffG = Convert.ToString(DiffG);
                                Gr.lblTotal = Convert.ToString(Hole);

                                // createChart1

                                GD.set_values();
                                BlueM = GD.DM;
                                RedM = GD.IM;
                                BlackM = GD.SM;
                                GreenM = GD.CM;

                                Gr.R1 = BlueM;
                                Gr.A1 = RedM;
                                Gr.P1 = BlackM;
                                Gr.D1 = GreenM;

                                //CreateChart2

                                GD.set_values();
                                BlueL = GD.DL;
                                RedL = GD.IL;
                                BlackL = GD.SL;
                                GreenL = GD.CL;

                                Gr.R2 = BlueL;
                                Gr.A2 = RedL;
                                Gr.P2 = BlackL;
                                Gr.D2 = GreenL;

                                //CreateChart3

                                GD.set_values();
                                DiffB = GD.DD;
                                DiffR = GD.ID;
                                DiffBl = GD.SD;
                                DiffG = GD.CD;

                                Gr.R3 = DiffB;
                                Gr.A3 = DiffR;
                                Gr.P3 = DiffBl;
                                Gr.D3 = DiffG;

                                strcmd = "SELECT * FROM tblCandRAPDScore WHERE c_id=" + cId;
                                SqlCommand cmdRAPD = new SqlCommand(strcmd, con);
                                SqlDataReader sdrRAPD = cmdRAPD.ExecuteReader();
                                if (sdrRAPD.HasRows)
                                {
                                    sdrRAPD.Close();
                                    SqlCommand updateRAPD = new SqlCommand("update tblCandRAPDScore set Rscore=@Rscore,Ascore=@Ascore,Pscore=@Pscore,Dscore=@Dscore where c_id=@c_id", con);
                                    updateRAPD.Parameters.AddWithValue("@c_id", cId);
                                    updateRAPD.Parameters.AddWithValue("@Rscore", DiffB);
                                    updateRAPD.Parameters.AddWithValue("@Ascore", DiffR);
                                    updateRAPD.Parameters.AddWithValue("@Pscore", DiffBl);
                                    updateRAPD.Parameters.AddWithValue("@Dscore", DiffG);
                                    int intEffectedRows = updateRAPD.ExecuteNonQuery();
                                }
                                else
                                {
                                    sdrRAPD.Close();

                                    SqlCommand updateRAPD = new SqlCommand("insert into tblCandRAPDScore (c_id,Rscore,Ascore,Pscore,Dscore) values(@c_id,@Rscore,@Ascore,@Pscore,@Dscore)", con);
                                    updateRAPD.Parameters.AddWithValue("@Rscore", DiffB);
                                    updateRAPD.Parameters.AddWithValue("@Ascore", DiffR);
                                    updateRAPD.Parameters.AddWithValue("@Pscore", DiffBl);
                                    updateRAPD.Parameters.AddWithValue("@Dscore", DiffG);
                                    updateRAPD.Parameters.AddWithValue("@c_id", cId);
                                    int intEffectedRows = updateRAPD.ExecuteNonQuery();
                                }

                            }

                        }
                        return Gr;

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

        public StudentGraph GetStudentGraph(int cId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {

                    string strcmd = "select c_first_name,c_last_name,c_middle_Name,c_age_years,c_username,c_email_id from tbl_candidate_master where c_id = " + cId;
                    SqlDataAdapter sda = new SqlDataAdapter(strcmd,con);
                    con.Open();
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    StudentGraph SG = new StudentGraph();
                    SG.name= ds.Tables[0].Rows[0][0].ToString() + " " + ds.Tables[0].Rows[0][2].ToString() + " " + ds.Tables[0].Rows[0][1].ToString();
                    SG.UserName = ds.Tables[0].Rows[0][4].ToString();
                    SG.CandEmailID = ds.Tables[0].Rows[0][5].ToString();
                    SG.CandAge = Convert.ToInt32(ds.Tables[0].Rows[0][3]);
                    int id = GD.get_values_maintest(cId);

                    BlueM = GD.BLUEM;
                    BlueL = GD.BLUEL;
                    RedM = GD.REDM;
                    BlackM = GD.BLACKM;
                    RedL = GD.REDL;
                    BlackL = GD.BLACKL;
                    GreenM = GD.GREENM;
                    GreenL = GD.GREENL;
                    Hole = GD.HOLE;

                    DiffB = BlueM - BlueL;
                    DiffR = RedM - RedL;
                    DiffBl = BlackM - BlackL;
                    DiffG = GreenM - GreenL;

                    SG.lblBM= Convert.ToString(BlueM);
                    SG.lblBL = Convert.ToString(BlueL);
                    SG.lblRM = Convert.ToString(RedM);
                    SG.lblRL = Convert.ToString(RedL);
                    SG.lblBlM = Convert.ToString(BlackM);
                    SG.lblBlL = Convert.ToString(BlackL);
                    SG.lblGM = Convert.ToString(GreenM);
                    SG.lblGL = Convert.ToString(GreenL);
                    SG.lblDiffB = Convert.ToString(DiffB);
                    SG.lblDiffR = Convert.ToString(DiffR);
                    SG.lblDiffBl = Convert.ToString(DiffBl);
                    SG.lblDiffG = Convert.ToString(DiffG);
                    SG.lblTotal = Convert.ToString(Hole);

                    //  CreateChart1();
                    /////////////////// het function in side///

                    GD.set_values_maintest();

                    BlueM = GD.DM;
                    RedM = GD.IM;
                    BlackM = GD.SM;
                    GreenM = GD.CM;

                    //RAPD GRAPH 1 information
                    SG.R1 = BlueM;
                    SG.A1 = RedM;
                    SG.P1 = BlackM;
                    SG.D1 = GreenM;

                    
                    GD.set_values_maintest();

                    BlueL = GD.DL;
                    RedL = GD.IL;
                    BlackL = GD.SL;
                    GreenL = GD.CL;

                    //RAPD GRAPH 2 information

                    SG.R2 = BlueL;
                    SG.A2 = RedL;
                    SG.P2 = BlackL;
                    SG.D2 = GreenL;

                    GD.set_values_maintest();
                    DiffB = GD.DD;
                    DiffR = GD.ID;
                    DiffBl = GD.SD;
                    DiffG = GD.CD;

                    //RAPD GRAPH 3 information
                    SG.R3 = DiffB;
                    SG.A3 = DiffR;
                    SG.P3 = DiffBl;
                    SG.D3 = DiffG;


                    String StrSql = "";
                    StrSql = "SELECT * FROM tbl_candidate_RAPD_Score WHERE c_id='" + cId + "'";
                    SqlCommand cmd1 = new SqlCommand(StrSql, con);
                    SqlDataReader sdr = cmd1.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        sdr.Close();
                        StrSql = "INSERT INTO tbl_candidate_RAPD_Score VALUES ( " + cId + "," + DiffB + ", " + DiffR + "," + DiffBl + "," + DiffG + ")";
                        SqlCommand cmd2 = new SqlCommand(StrSql, con);
                        int i = cmd2.ExecuteNonQuery();
                        //cccc++;
                    }
                    else
                    {
                        sdr.Close();
                        StrSql = "update tbl_candidate_RAPD_Score set Rscore=" + DiffB + ", Ascore=" + DiffR + ",Pscore=" + DiffBl + ",Dscore=" + DiffG + " where c_id=" + cId;
                        SqlCommand cmd3 = new SqlCommand(StrSql,con);
                        int i = cmd3.ExecuteNonQuery();
                    }
                    return SG;
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