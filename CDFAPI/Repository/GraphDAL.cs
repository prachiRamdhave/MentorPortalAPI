using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CDFAPI.Repository
{
    public class GraphDAL
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        string strcon = ConfigurationManager.ConnectionStrings["career_portalConnectionString"].ConnectionString;
        int batid = 3;
        private int bluem;
        private int redm;
        private int blackm;
        private int greenm;
        private int bluel;
        private int redl;
        private int blackl;
        private int greenl;
        private int hole;
        private int diffb, diffr, diffbl, diffg;
        private int dm, sm, cm, im, dl, il, sl, cl, dd, id, sd, cd;
        private string code_m, code_l, dw;


        public int DM
        {
            set { dm = value; }
            get { return dm; }

        }
        public int IM
        {
            set { im = value; }
            get { return im; }
        }
        public int SM
        {
            set { sm = value; }
            get { return sm; }
        }
        public int CM
        {
            set { cm = value; }
            get { return cm; }
        }
        public int DL
        {
            set { dl = value; }
            get { return dl; }

        }
        public int IL
        {
            set { il = value; }
            get { return il; }
        }
        public int SL
        {
            set { sl = value; }
            get { return sl; }
        }
        public int CL
        {
            set { cl = value; }
            get { return cd; }
        }
        public int DD
        {
            set { dd = value; }
            get { return dd; }

        }
        public int ID
        {
            set { id = value; }
            get { return id; }
        }
        public int SD
        {
            set { sd = value; }
            get { return sd; }
        }
        public int CD
        {
            set { cd = value; }
            get { return cd; }
        }
        public int BLUEM
        {
            set { bluem = value; }
            get { return bluem; }
        }
        public int REDM
        {
            set { redm = value; }
            get { return redm; }
        }
        public int BLACKM
        {
            set { blackm = value; }
            get { return blackm; }
        }
        public int GREENM
        {
            set { greenm = value; }
            get { return greenm; }
        }
        public int BLUEL
        {
            set { bluel = value; }
            get { return bluel; }
        }
        public int REDL
        {
            set { redl = value; }
            get { return redl; }
        }
        public int BLACKL
        {
            set { blackl = value; }
            get { return blackl; }
        }
        public int GREENL
        {
            set { greenl = value; }
            get { return greenl; }
        }
        public int HOLE
        {
            set { hole = value; }
            get { return hole; }
        }
        public int DIFFB
        {
            set { diffb = value; }
            get { return diffb; }
        }
        public int DIFFR
        {
            set { diffr = value; }
            get { return diffr; }
        }
        public int DIFFBL
        {
            set { diffbl = value; }
            get { return diffbl; }
        }
        public int DIFFG
        {
            set { diffg = value; }
            get { return diffg; }
        }
        public string DW
        {
            set { dw = value; }
            get { return dw; }
        }


        public Boolean get_values(int cId, int batid)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("select * from tblPersonalityCandAnswers where batid=" + batid + " and c_id=" + cId, con);
                    con.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            code_m = sdr["most_code"].ToString();
                            code_l = sdr["least_code"].ToString();

                            switch (code_m)
                            {
                                case "Bl":
                                    BLACKM = BLACKM + 1;
                                    break;
                                case "B":
                                    BLUEM = BLUEM + 1;
                                    break;
                                case "G":
                                    GREENM = GREENM + 1;
                                    break;
                                case "R":
                                    REDM = REDM + 1;
                                    break;
                                case "H":
                                    HOLE = HOLE + 1;
                                    break;
                            }
                            switch (code_l)
                            {
                                case "Bl":
                                    BLACKL = BLACKL + 1;
                                    break;
                                case "B":
                                    BLUEL = BLUEL + 1;
                                    break;
                                case "G":
                                    GREENL = GREENL + 1;
                                    break;
                                case "R":
                                    REDL = REDL + 1;
                                    break;
                                case "H":
                                    HOLE = HOLE + 1;
                                    break;
                            }
                        }
                        DIFFB = BLUEM - BLUEL;
                        DIFFR = REDM - REDL;
                        DIFFBL = BLACKM - BLACKL;
                        DIFFG = GREENM - GREENL;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                return false;
            }
        }

        public int set_values()
        {
            //For Graph1 D
            switch (BLUEM)
            {
                case 0:
                    DM = -19;
                    break;
                case 1:
                    DM = -11;
                    break;
                case 2:
                    DM = -9;
                    break;
                case 3:
                    DM = -6;
                    break;
                case 4:
                    DM = -5;
                    break;
                case 5:
                    DM = -4;
                    break;
                case 6:
                    DM = -2;
                    break;
                case 7:
                    DM = 0;
                    break;
                case 8:
                    DM = 2;
                    break;
                case 9:
                    DM = 4;
                    break;
                case 10:
                    DM = 7;
                    break;
                case 11:
                    DM = 7;
                    break;
                case 12:
                    DM = 8;
                    break;
                case 13:
                    DM = 8;
                    break;
                case 14:
                    DM = 9;
                    break;
                case 15:
                    DM = 10;
                    break;
                case 16:
                    DM = 11;
                    break;
                case 17:
                    DM = 13;
                    break;
                case 18:
                    DM = 15;
                    break;
                case 19:
                    DM = 17;
                    break;
                case 20:
                    DM = 19;
                    break;
            }

            //For Graph1 I
            switch (REDM)
            {
                case 0:
                    IM = -11;
                    break;
                case 1:
                    IM = -8;
                    break;
                case 2:
                    IM = -7;
                    break;
                case 3:
                    IM = -4;
                    break;
                case 4:
                    IM = -1;
                    break;
                case 5:
                    IM = 2;
                    break;
                case 6:
                    IM = 3;
                    break;
                case 7:
                    IM = 7;
                    break;
                case 8:
                    IM = 8;
                    break;
                case 9:
                    IM = 9;
                    break;
                case 10:
                    IM = 11;
                    break;
                case 11:
                    IM = 12;
                    break;
                case 12:
                    IM = 13;
                    break;
                case 13:
                    IM = 14;
                    break;
                case 14:
                    IM = 15;
                    break;
                case 15:
                    IM = 16;
                    break;
                case 16:
                    IM = 17;
                    break;
                case 17:
                    IM = 19;
                    break;
            }

            //For Graph1 S
            switch (BLACKM)
            {
                case 0:
                    SM = -8;
                    break;
                case 1:
                    SM = -7;
                    break;
                case 2:
                    SM = -6;
                    break;
                case 3:
                    SM = -3;
                    break;
                case 4:
                    SM = -2;
                    break;
                case 5:
                    SM = 0;
                    break;
                case 6:
                    SM = 2;
                    break;
                case 7:
                    SM = 3;
                    break;
                case 8:
                    SM = 5;
                    break;
                case 9:
                    SM = 7;
                    break;
                case 10:
                    SM = 8;
                    break;
                case 11:
                    SM = 9;
                    break;
                case 12:
                    SM = 11;
                    break;
                case 13:
                    SM = 12;
                    break;
                case 14:
                    SM = 13;
                    break;
                case 15:
                    SM = 14;
                    break;
                case 16:
                    SM = 15;
                    break;
                case 17:
                    SM = 16;
                    break;
                case 18:
                    SM = 17;
                    break;
                case 19:
                    SM = 19;
                    break;
            }

            switch (GREENM)
            {
                case 0:
                    CM = -12;
                    break;
                case 1:
                    CM = -8;
                    break;
                case 2:
                    CM = -7;
                    break;
                case 3:
                    CM = -3;
                    break;
                case 4:
                    CM = 0;
                    break;
                case 5:
                    CM = 4;
                    break;
                case 11:
                    CM = 14;
                    break;
                case 9:
                    CM = 11;
                    break;
                case 8:
                    CM = 9;
                    break;
                case 7:
                    CM = 8;
                    break;
                case 6:
                    CM = 7;
                    break;
                case 14:
                    CM = 17;
                    break;
                case 15:
                    CM = 19;
                    break;
                case 13:
                    CM = 15;
                    break;
                case 10:
                    CM = 13;
                    break;
                case 12:
                    CM = 15;
                    break;
            }
            //***********************************************************************************************
            // For Graph2 D
            switch (BLUEL)
            {
                case 0:
                    DL = 19;
                    break;
                case 1:
                    DL = 9;
                    break;
                case 2:
                    DL = 5;
                    break;
                case 3:
                    DL = 2;
                    break;
                case 4:
                    DL = 0;
                    break;
                case 5:
                    DL = -1;
                    break;
                case 6:
                    DL = -2;
                    break;
                case 7:
                    DL = -3;
                    break;
                case 8:
                    DL = -4;
                    break;
                case 9:
                    DL = -5;
                    break;
                case 10:
                    DL = -6;
                    break;
                case 11:
                    DL = -7;
                    break;
                case 12:
                    DL = -8;
                    break;
                case 13:
                    DL = -9;
                    break;
                case 14:
                    DL = -10;
                    break;
                case 15:
                    DL = -11;
                    break;
                case 16:
                    DL = -12;
                    break;
                case 17:
                    DL = -13;
                    break;
                case 18:
                    DL = -14;
                    break;
                case 19:
                    DL = -16;
                    break;
                case 20:
                    DL = -17;
                    break;
                case 21:
                    DL = -19;
                    break;
            }
            //For Graph2 I
            switch (REDL)
            {
                case 0:
                    IL = 19;
                    break;
                case 9:
                    IL = -9;
                    break;
                case 1:
                    IL = 9;
                    break;
                case 5:
                    IL = -3;
                    break;
                case 2:
                    IL = 5;
                    break;
                case 3:
                    IL = 1;
                    break;
                case 4:
                    IL = -1;
                    break;
                case 6:
                    IL = -5;
                    break;
                case 7:
                    IL = -7;
                    break;
                case 8:
                    IL = -8;
                    break;
                case 10:
                    IL = -10;
                    break;
                case 11:
                    IL = -11;
                    break;
                case 12:
                    IL = -12;
                    break;
                case 13:
                    IL = -13;
                    break;
                case 14:
                    IL = -14;
                    break;
                case 15:
                    IL = -15;
                    break;
                case 16:
                    IL = -16;
                    break;
                case 17:
                    IL = -17;
                    break;
                case 18:
                    IL = -18;
                    break;
                case 19:
                    IL = -19;
                    break;
            }
            //For Graph2 S
            switch (BLACKL)
            {
                case 0:
                    SL = 19;
                    break;
                case 1:
                    SL = 11;
                    break;
                case 9:
                    SL = -7;
                    break;
                case 2:
                    SL = 9;
                    break;
                case 8:
                    SL = -5;
                    break;
                case 3:
                    SL = 8;
                    break;
                case 4:
                    SL = 3;
                    break;
                case 5:
                    SL = 1;
                    break;
                case 6:
                    SL = -1;
                    break;
                case 7:
                    SL = -3;
                    break;
                case 10:
                    SL = -8;
                    break;
                case 11:
                    SL = -10;
                    break;
                case 12:
                    SL = -11;
                    break;
                case 13:
                    SL = -12;
                    break;
                case 14:
                    SL = -13;
                    break;
                case 15:
                    SL = -14;
                    break;
                case 16:
                    SL = -15;
                    break;
                case 17:
                    SL = -16;
                    break;
                case 18:
                    SL = -17;
                    break;
                case 19:
                    SL = -19;
                    break;
            }

            //For Graph2 C
            switch (GREENL)
            {
                case 0:
                    CL = 19;
                    break;
                case 11:
                    CL = -9;
                    break;
                case 1:
                    CL = 11;
                    break;
                case 9:
                    CL = -6;
                    break;
                case 2:
                    CL = 9;
                    break;
                case 7:
                    CL = -2;
                    break;
                case 3:
                    CL = 7;
                    break;
                case 4:
                    CL = 4;
                    break;
                case 5:
                    CL = 2;
                    break;
                case 6:
                    CL = 0;
                    break;
                case 8:
                    CL = -4;
                    break;
                case 10:
                    CL = -7;
                    break;
                case 12:
                    CL = -10;
                    break;
                case 13:
                    CL = -12;
                    break;
                case 14:
                    CL = -14;
                    break;
                case 15:
                    CL = -16;
                    break;
                case 16:
                    CL = -19;
                    break;
            }

            //************************************************************************************************
            // For Graph3 D
            switch (DIFFB)
            {
                case 10:
                    DD = 5;
                    break;
                case 13:
                    DD = 8;
                    break;
                case 17:
                    DD = 13;
                    break;
                case 19:
                    DD = 17;
                    break;
                case 20:
                    DD = 19;
                    break;
                case 15:
                    DD = 10;
                    break;
                case 18:
                    DD = 15;
                    break;
                case 11:
                    DD = 6;
                    break;
                case 16:
                    DD = 11;
                    break;
                case 9:
                    DD = 4;
                    break;
                case 14:
                    DD = 9;
                    break;
                case 7:
                    DD = 2;
                    break;
                case 12:
                    DD = 7;
                    break;
                case 3:
                    DD = 0;
                    break;
                case 8:
                    DD = 3;
                    break;
                case -5:
                    DD = -5;
                    break;
                case -4:
                    DD = -5;
                    break;
                case -3:
                    DD = -4;
                    break;
                case -1:
                    DD = -3;
                    break;
                case -2:
                    DD = -3;
                    break;
                case 1:
                    DD = -1;
                    break;
                case 2:
                    DD = -1;
                    break;
                case 5:
                    DD = 1;
                    break;
                case 0:
                    DD = -2;
                    break;
                case -6:
                    DD = -2;
                    break;
                case -7:
                    DD = -7;
                    break;
                case -8:
                    DD = -7;
                    break;
                case -9:
                    DD = -8;
                    break;
                case -10:
                    DD = -9;
                    break;
                case -11:
                    DD = -10;
                    break;
                case -12:
                    DD = -10;
                    break;
                case -13:
                    DD = -11;
                    break;
                case -14:
                    DD = -13;
                    break;
                case -15:
                    DD = -14;
                    break;
                case -16:
                    DD = -15;
                    break;
                case -17:
                    DD = -16;
                    break;
                case -18:
                    DD = -17;
                    break;
                case -19:
                    DD = -18;
                    break;
                case -20:
                    DD = -18;
                    break;
                case -21:
                    DD = -19;
                    break;
            }

            //For Graph3 I
            switch (DIFFR)
            {
                case 17:
                    ID = 19;
                    break;
                case 16:
                    ID = 18;
                    break;
                case 15:
                    ID = 17;
                    break;
                case 14:
                    ID = 16;
                    break;
                case 13:
                    ID = 15;
                    break;
                case 12:
                    ID = 14;
                    break;
                case 11:
                    ID = 13;
                    break;
                case 10:
                    ID = 12;
                    break;
                case 9:
                    ID = 11;
                    break;
                case 8:
                    ID = 10;
                    break;
                case 7:
                    ID = 9;
                    break;
                case 6:
                    ID = 8;
                    break;
                case 5:
                    ID = 7;
                    break;
                case 4:
                    ID = 6;
                    break;
                case 3:
                    ID = 4;
                    break;
                case 2:
                    ID = 2;
                    break;
                case 1:
                    ID = 1;
                    break;
                case -19:
                    ID = -19;
                    break;
                case -18:
                    ID = -18;
                    break;
                case -17:
                    ID = -18;
                    break;
                case -16:
                    ID = -17;
                    break;
                case -15:
                    ID = -16;
                    break;
                case -14:
                    ID = -15;
                    break;
                case -13:
                    ID = -14;
                    break;
                case -12:
                    ID = -13;
                    break;
                case -11:
                    ID = -12;
                    break;
                case -10:
                    ID = -11;
                    break;
                case -9:
                    ID = -10;
                    break;
                case -8:
                    ID = -9;
                    break;
                case -7:
                    ID = -8;
                    break;
                case -6:
                    ID = -7;
                    break;
                case -5:
                    ID = -6;
                    break;
                case -4:
                    ID = -5;
                    break;
                case -3:
                    ID = -4;
                    break;
                case -2:
                    ID = -3;
                    break;
                case -1:
                    ID = -2;
                    break;
                case 0:
                    ID = -1;
                    break;

            }

            //For Graph3 S
            switch (DIFFBL)
            {
                case 19:
                    SD = 19;
                    break;
                case 18:
                    SD = 18;
                    break;
                case 17:
                    SD = 17;
                    break;
                case 16:
                    SD = 16;
                    break;
                case 15:
                    SD = 15;
                    break;
                case 14:
                    SD = 14;
                    break;
                case 13:
                    SD = 13;
                    break;
                case 12:
                    SD = 12;
                    break;
                case 11:
                    SD = 11;
                    break;
                case 10:
                    SD = 10;
                    break;
                case 9:
                    SD = 9;
                    break;
                case 8:
                    SD = 8;
                    break;
                case 7:
                    SD = 7;
                    break;
                case 6:
                    SD = 6;
                    break;
                case 5:
                    SD = 5;
                    break;
                case 4:
                    SD = 4;
                    break;
                case 3:
                    SD = 3;
                    break;
                case 2:
                    SD = 2;
                    break;
                case 1:
                    SD = 1;
                    break;
                case 0:
                    SD = 0;
                    break;
                case -1:
                    SD = -1;
                    break;
                case -2:
                    SD = -2;
                    break;
                case -3:
                    SD = -3;
                    break;
                case -4:
                    SD = -4;
                    break;
                case -5:
                    SD = -5;
                    break;
                case -6:
                    SD = -6;
                    break;
                case -7:
                    SD = -7;
                    break;
                case -8:
                    SD = -8;
                    break;
                case -9:
                    SD = -9;
                    break;
                case -10:
                    SD = -10;
                    break;
                case -11:
                    SD = -11;
                    break;
                case -12:
                    SD = -12;
                    break;
                case -13:
                    SD = -13;
                    break;
                case -14:
                    SD = -14;
                    break;
                case -15:
                    SD = -15;
                    break;
                case -16:
                    SD = -16;
                    break;
                case -17:
                    SD = -17;
                    break;
                case -18:
                    SD = -18;
                    break;
                case -19:
                    SD = -19;
                    break;
            }

            //For Graph3 C
            switch (DIFFG)
            {
                case 15:
                    CD = 19;
                    break;
                case 14:
                    CD = 18;
                    break;
                case 13:
                    CD = 17;
                    break;
                case 12:
                    CD = 16;
                    break;
                case 11:
                    CD = 15;
                    break;
                case 10:
                    CD = 14;
                    break;
                case 9:
                    CD = 13;
                    break;
                case 8:
                    CD = 12;
                    break;
                case 7:
                    CD = 11;
                    break;
                case 6:
                    CD = 10;
                    break;
                case 5:
                    CD = 9;
                    break;
                case 4:
                    CD = 8;
                    break;
                case 3:
                    CD = 7;
                    break;
                case 2:
                    CD = 5;
                    break;
                case 1:
                    CD = 3;
                    break;
                case 0:
                    CD = 2;
                    break;
                case -1:
                    CD = 1;
                    break;
                case -2:
                    CD = 0;
                    break;
                case -3:
                    CD = -2;
                    break;
                case -4:
                    CD = -3;
                    break;
                case -5:
                    CD = -4;
                    break;
                case -6:
                    CD = -5;
                    break;
                case -7:
                    CD = -6;
                    break;
                case -8:
                    CD = -7;
                    break;
                case -9:
                    CD = -8;
                    break;
                case -10:
                    CD = -9;
                    break;
                case -11:
                    CD = -10;
                    break;
                case -12:
                    CD = -11;
                    break;
                case -13:
                    CD = -13;
                    break;
                case -14:
                    CD = -15;
                    break;
                case -15:
                    CD = -17;
                    break;
                case -16:
                    CD = -19;
                    break;
            }
            //***********************************************************************************************
            return DM;
            return IM;
            return SM;
            return CM;
            return DL;
            return IL;
            return SL;
            return CL;
            return DD;
            return ID;
            return SD;
            return CD;
            return HOLE;

        }

        public int get_values_maintest(int cId)
        {

                    SqlConnection con = new SqlConnection(strcon);                    
                    string strcmd = "Select * FROM Cand_PD_Details where c_id= '" + cId + "'";
                    SqlCommand cmd = new SqlCommand(strcmd,con);
                    con.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                   
                        while (sdr.Read())
                        {
                            code_m = sdr.GetString(4);
                            code_l = sdr.GetString(5);
                            switch (code_m)
                            {
                                case "Bl":
                                    BLACKM = BLACKM + 1;
                                    break;
                                case "B":
                                    BLUEM = BLUEM + 1;
                                    break;
                                case "G":
                                    GREENM = GREENM + 1;
                                    break;
                                case "R":
                                    REDM = REDM + 1;
                                    break;
                                case "H":
                                    HOLE = HOLE + 1;
                                    break;
                            }

                            switch (code_l)
                            {
                                case "Bl":
                                    BLACKL = BLACKL + 1;
                                    break;
                                case "B":
                                    BLUEL = BLUEL + 1;
                                    break;
                                case "G":
                                    GREENL = GREENL + 1;
                                    break;
                                case "R":
                                    REDL = REDL + 1;
                                    break;
                                case "H":
                                    HOLE = HOLE + 1;
                                    break;
                            }

                        }
                        DIFFB = BLUEM - BLUEL;
                        DIFFR = REDM - REDL;
                        DIFFBL = BLACKM - BLACKL;
                        DIFFG = GREENM - GREENL;

                        return BLUEM;
                        return BLUEL;
                        return REDM;
                        return REDL;
                        return BLACKM;
                        return BLACKL;
                        return GREENM;
                        return GREENL;
                        return HOLE;
          
        }

        public int set_values_maintest()
        {
            //For Graph1 D
            switch (BLUEM)
            {
                case 0:
                    DM = -19;
                    break;
                case 1:
                    DM = -11;
                    break;
                case 2:
                    DM = -9;
                    break;
                case 3:
                    DM = -6;
                    break;
                case 4:
                    DM = -5;
                    break;
                case 5:
                    DM = -4;
                    break;
                case 6:
                    DM = -2;
                    break;
                case 7:
                    DM = 0;
                    break;
                case 8:
                    DM = 2;
                    break;
                case 9:
                    DM = 4;
                    break;
                case 10:
                    DM = 7;
                    break;
                case 11:
                    DM = 7;
                    break;
                case 12:
                    DM = 8;
                    break;
                case 13:
                    DM = 8;
                    break;
                case 14:
                    DM = 9;
                    break;
                case 15:
                    DM = 10;
                    break;
                case 16:
                    DM = 11;
                    break;
                case 17:
                    DM = 13;
                    break;
                case 18:
                    DM = 15;
                    break;
                case 19:
                    DM = 17;
                    break;
                case 20:
                    DM = 19;
                    break;
            }

            //For Graph1 I
            switch (REDM)
            {
                case 0:
                    IM = -11;
                    break;
                case 1:
                    IM = -8;
                    break;
                case 2:
                    IM = -7;
                    break;
                case 3:
                    IM = -4;
                    break;
                case 4:
                    IM = -1;
                    break;
                case 5:
                    IM = 2;
                    break;
                case 6:
                    IM = 3;
                    break;
                case 7:
                    IM = 7;
                    break;
                case 8:
                    IM = 8;
                    break;
                case 9:
                    IM = 9;
                    break;
                case 10:
                    IM = 11;
                    break;
                case 11:
                    IM = 12;
                    break;
                case 12:
                    IM = 13;
                    break;
                case 13:
                    IM = 14;
                    break;
                case 14:
                    IM = 15;
                    break;
                case 15:
                    IM = 16;
                    break;
                case 16:
                    IM = 17;
                    break;
                case 17:
                    IM = 19;
                    break;
            }

            //For Graph1 S
            switch (BLACKM)
            {
                case 0:
                    SM = -8;
                    break;
                case 1:
                    SM = -7;
                    break;
                case 2:
                    SM = -6;
                    break;
                case 3:
                    SM = -3;
                    break;
                case 4:
                    SM = -2;
                    break;
                case 5:
                    SM = 0;
                    break;
                case 6:
                    SM = 2;
                    break;
                case 7:
                    SM = 3;
                    break;
                case 8:
                    SM = 5;
                    break;
                case 9:
                    SM = 7;
                    break;
                case 10:
                    SM = 8;
                    break;
                case 11:
                    SM = 9;
                    break;
                case 12:
                    SM = 11;
                    break;
                case 13:
                    SM = 12;
                    break;
                case 14:
                    SM = 13;
                    break;
                case 15:
                    SM = 14;
                    break;
                case 16:
                    SM = 15;
                    break;
                case 17:
                    SM = 16;
                    break;
                case 18:
                    SM = 17;
                    break;
                case 19:
                    SM = 19;
                    break;
            }

            switch (GREENM)
            {
                case 0:
                    CM = -12;
                    break;
                case 1:
                    CM = -8;
                    break;
                case 2:
                    CM = -7;
                    break;
                case 3:
                    CM = -3;
                    break;
                case 4:
                    CM = 0;
                    break;
                case 5:
                    CM = 4;
                    break;
                case 11:
                    CM = 14;
                    break;
                case 9:
                    CM = 11;
                    break;
                case 8:
                    CM = 9;
                    break;
                case 7:
                    CM = 8;
                    break;
                case 6:
                    CM = 7;
                    break;
                case 14:
                    CM = 17;
                    break;
                case 15:
                    CM = 19;
                    break;
                case 13:
                    CM = 15;
                    break;
                case 10:
                    CM = 13;
                    break;
                case 12:
                    CM = 15;
                    break;
            }
            //***********************************************************************************************
            // For Graph2 D
            switch (BLUEL)
            {
                case 0:
                    DL = 19;
                    break;
                case 1:
                    DL = 9;
                    break;
                case 2:
                    DL = 5;
                    break;
                case 3:
                    DL = 2;
                    break;
                case 4:
                    DL = 0;
                    break;
                case 5:
                    DL = -1;
                    break;
                case 6:
                    DL = -2;
                    break;
                case 7:
                    DL = -3;
                    break;
                case 8:
                    DL = -4;
                    break;
                case 9:
                    DL = -5;
                    break;
                case 10:
                    DL = -6;
                    break;
                case 11:
                    DL = -7;
                    break;
                case 12:
                    DL = -8;
                    break;
                case 13:
                    DL = -9;
                    break;
                case 14:
                    DL = -10;
                    break;
                case 15:
                    DL = -11;
                    break;
                case 16:
                    DL = -12;
                    break;
                case 17:
                    DL = -13;
                    break;
                case 18:
                    DL = -14;
                    break;
                case 19:
                    DL = -16;
                    break;
                case 20:
                    DL = -17;
                    break;
                case 21:
                    DL = -19;
                    break;
            }
            //For Graph2 I
            switch (REDL)
            {
                case 0:
                    IL = 19;
                    break;
                case 9:
                    IL = -9;
                    break;
                case 1:
                    IL = 9;
                    break;
                case 5:
                    IL = -3;
                    break;
                case 2:
                    IL = 5;
                    break;
                case 3:
                    IL = 1;
                    break;
                case 4:
                    IL = -1;
                    break;
                case 6:
                    IL = -5;
                    break;
                case 7:
                    IL = -7;
                    break;
                case 8:
                    IL = -8;
                    break;
                case 10:
                    IL = -10;
                    break;
                case 11:
                    IL = -11;
                    break;
                case 12:
                    IL = -12;
                    break;
                case 13:
                    IL = -13;
                    break;
                case 14:
                    IL = -14;
                    break;
                case 15:
                    IL = -15;
                    break;
                case 16:
                    IL = -16;
                    break;
                case 17:
                    IL = -17;
                    break;
                case 18:
                    IL = -18;
                    break;
                case 19:
                    IL = -19;
                    break;
            }
            //For Graph2 S
            switch (BLACKL)
            {
                case 0:
                    SL = 19;
                    break;
                case 1:
                    SL = 11;
                    break;
                case 9:
                    SL = -7;
                    break;
                case 2:
                    SL = 9;
                    break;
                case 8:
                    SL = -5;
                    break;
                case 3:
                    SL = 8;
                    break;
                case 4:
                    SL = 3;
                    break;
                case 5:
                    SL = 1;
                    break;
                case 6:
                    SL = -1;
                    break;
                case 7:
                    SL = -3;
                    break;
                case 10:
                    SL = -8;
                    break;
                case 11:
                    SL = -10;
                    break;
                case 12:
                    SL = -11;
                    break;
                case 13:
                    SL = -12;
                    break;
                case 14:
                    SL = -13;
                    break;
                case 15:
                    SL = -14;
                    break;
                case 16:
                    SL = -15;
                    break;
                case 17:
                    SL = -16;
                    break;
                case 18:
                    SL = -17;
                    break;
                case 19:
                    SL = -19;
                    break;
            }

            //For Graph2 C
            switch (GREENL)
            {
                case 0:
                    CL = 19;
                    break;
                case 11:
                    CL = -9;
                    break;
                case 1:
                    CL = 11;
                    break;
                case 9:
                    CL = -6;
                    break;
                case 2:
                    CL = 9;
                    break;
                case 7:
                    CL = -2;
                    break;
                case 3:
                    CL = 7;
                    break;
                case 4:
                    CL = 4;
                    break;
                case 5:
                    CL = 2;
                    break;
                case 6:
                    CL = 0;
                    break;
                case 8:
                    CL = -4;
                    break;
                case 10:
                    CL = -7;
                    break;
                case 12:
                    CL = -10;
                    break;
                case 13:
                    CL = -12;
                    break;
                case 14:
                    CL = -14;
                    break;
                case 15:
                    CL = -16;
                    break;
                case 16:
                    CL = -19;
                    break;
            }

            //************************************************************************************************
            // For Graph3 D
            switch (DIFFB)
            {
                case 10:
                    DD = 5;
                    break;
                case 13:
                    DD = 8;
                    break;
                case 17:
                    DD = 13;
                    break;
                case 19:
                    DD = 17;
                    break;
                case 20:
                    DD = 19;
                    break;
                case 15:
                    DD = 10;
                    break;
                case 18:
                    DD = 15;
                    break;
                case 11:
                    DD = 6;
                    break;
                case 16:
                    DD = 11;
                    break;
                case 9:
                    DD = 4;
                    break;
                case 14:
                    DD = 9;
                    break;
                case 7:
                    DD = 2;
                    break;
                case 12:
                    DD = 7;
                    break;
                case 3:
                    DD = 0;
                    break;
                case 8:
                    DD = 3;
                    break;
                case -5:
                    DD = -5;
                    break;
                case -4:
                    DD = -5;
                    break;
                case -3:
                    DD = -4;
                    break;
                case -1:
                    DD = -3;
                    break;
                case -2:
                    DD = -3;
                    break;
                case 1:
                    DD = -1;
                    break;
                case 2:
                    DD = -1;
                    break;
                case 5:
                    DD = 1;
                    break;
                case 0:
                    DD = -2;
                    break;
                case -6:
                    DD = -2;
                    break;
                case -7:
                    DD = -7;
                    break;
                case -8:
                    DD = -7;
                    break;
                case -9:
                    DD = -8;
                    break;
                case -10:
                    DD = -9;
                    break;
                case -11:
                    DD = -10;
                    break;
                case -12:
                    DD = -10;
                    break;
                case -13:
                    DD = -11;
                    break;
                case -14:
                    DD = -13;
                    break;
                case -15:
                    DD = -14;
                    break;
                case -16:
                    DD = -15;
                    break;
                case -17:
                    DD = -16;
                    break;
                case -18:
                    DD = -17;
                    break;
                case -19:
                    DD = -18;
                    break;
                case -20:
                    DD = -18;
                    break;
                case -21:
                    DD = -19;
                    break;
            }

            //For Graph3 I
            switch (DIFFR)
            {
                case 17:
                    ID = 19;
                    break;
                case 16:
                    ID = 18;
                    break;
                case 15:
                    ID = 17;
                    break;
                case 14:
                    ID = 16;
                    break;
                case 13:
                    ID = 15;
                    break;
                case 12:
                    ID = 14;
                    break;
                case 11:
                    ID = 13;
                    break;
                case 10:
                    ID = 12;
                    break;
                case 9:
                    ID = 11;
                    break;
                case 8:
                    ID = 10;
                    break;
                case 7:
                    ID = 9;
                    break;
                case 6:
                    ID = 8;
                    break;
                case 5:
                    ID = 7;
                    break;
                case 4:
                    ID = 6;
                    break;
                case 3:
                    ID = 4;
                    break;
                case 2:
                    ID = 2;
                    break;
                case 1:
                    ID = 1;
                    break;
                case -19:
                    ID = -19;
                    break;
                case -18:
                    ID = -18;
                    break;
                case -17:
                    ID = -18;
                    break;
                case -16:
                    ID = -17;
                    break;
                case -15:
                    ID = -16;
                    break;
                case -14:
                    ID = -15;
                    break;
                case -13:
                    ID = -14;
                    break;
                case -12:
                    ID = -13;
                    break;
                case -11:
                    ID = -12;
                    break;
                case -10:
                    ID = -11;
                    break;
                case -9:
                    ID = -10;
                    break;
                case -8:
                    ID = -9;
                    break;
                case -7:
                    ID = -8;
                    break;
                case -6:
                    ID = -7;
                    break;
                case -5:
                    ID = -6;
                    break;
                case -4:
                    ID = -5;
                    break;
                case -3:
                    ID = -4;
                    break;
                case -2:
                    ID = -3;
                    break;
                case -1:
                    ID = -2;
                    break;
                case 0:
                    ID = -1;
                    break;

            }

            //For Graph3 S
            switch (DIFFBL)
            {
                case 19:
                    SD = 19;
                    break;
                case 18:
                    SD = 18;
                    break;
                case 17:
                    SD = 17;
                    break;
                case 16:
                    SD = 16;
                    break;
                case 15:
                    SD = 15;
                    break;
                case 14:
                    SD = 14;
                    break;
                case 13:
                    SD = 13;
                    break;
                case 12:
                    SD = 12;
                    break;
                case 11:
                    SD = 11;
                    break;
                case 10:
                    SD = 10;
                    break;
                case 9:
                    SD = 9;
                    break;
                case 8:
                    SD = 8;
                    break;
                case 7:
                    SD = 7;
                    break;
                case 6:
                    SD = 6;
                    break;
                case 5:
                    SD = 5;
                    break;
                case 4:
                    SD = 4;
                    break;
                case 3:
                    SD = 3;
                    break;
                case 2:
                    SD = 2;
                    break;
                case 1:
                    SD = 1;
                    break;
                case 0:
                    SD = 0;
                    break;
                case -1:
                    SD = -1;
                    break;
                case -2:
                    SD = -2;
                    break;
                case -3:
                    SD = -3;
                    break;
                case -4:
                    SD = -4;
                    break;
                case -5:
                    SD = -5;
                    break;
                case -6:
                    SD = -6;
                    break;
                case -7:
                    SD = -7;
                    break;
                case -8:
                    SD = -8;
                    break;
                case -9:
                    SD = -9;
                    break;
                case -10:
                    SD = -10;
                    break;
                case -11:
                    SD = -11;
                    break;
                case -12:
                    SD = -12;
                    break;
                case -13:
                    SD = -13;
                    break;
                case -14:
                    SD = -14;
                    break;
                case -15:
                    SD = -15;
                    break;
                case -16:
                    SD = -16;
                    break;
                case -17:
                    SD = -17;
                    break;
                case -18:
                    SD = -18;
                    break;
                case -19:
                    SD = -19;
                    break;
            }

            //For Graph3 C
            switch (DIFFG)
            {
                case 15:
                    CD = 19;
                    break;
                case 14:
                    CD = 18;
                    break;
                case 13:
                    CD = 17;
                    break;
                case 12:
                    CD = 16;
                    break;
                case 11:
                    CD = 15;
                    break;
                case 10:
                    CD = 14;
                    break;
                case 9:
                    CD = 13;
                    break;
                case 8:
                    CD = 12;
                    break;
                case 7:
                    CD = 11;
                    break;
                case 6:
                    CD = 10;
                    break;
                case 5:
                    CD = 9;
                    break;
                case 4:
                    CD = 8;
                    break;
                case 3:
                    CD = 7;
                    break;
                case 2:
                    CD = 5;
                    break;
                case 1:
                    CD = 3;
                    break;
                case 0:
                    CD = 2;
                    break;
                case -1:
                    CD = 1;
                    break;
                case -2:
                    CD = 0;
                    break;
                case -3:
                    CD = -2;
                    break;
                case -4:
                    CD = -3;
                    break;
                case -5:
                    CD = -4;
                    break;
                case -6:
                    CD = -5;
                    break;
                case -7:
                    CD = -6;
                    break;
                case -8:
                    CD = -7;
                    break;
                case -9:
                    CD = -8;
                    break;
                case -10:
                    CD = -9;
                    break;
                case -11:
                    CD = -10;
                    break;
                case -12:
                    CD = -11;
                    break;
                case -13:
                    CD = -13;
                    break;
                case -14:
                    CD = -15;
                    break;
                case -15:
                    CD = -17;
                    break;
                case -16:
                    CD = -19;
                    break;
            }
            //***********************************************************************************************
            return DM;
            return IM;
            return SM;
            return CM;
            return DL;
            return IL;
            return SL;
            return CL;
            return DD;
            return ID;
            return SD;
            return CD;
            return HOLE;

        }

    }
}