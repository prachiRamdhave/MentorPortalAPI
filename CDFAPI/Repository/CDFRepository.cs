using CDFAPI.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using static CDFAPI.Models.Resources;
using static CDFAPI.Models.CDFProfile;
using static CDFAPI.Models.CommonDetails;
using System.Drawing;

namespace CDFAPI.Repository
{
    public class CDFRepository
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public  List<NewsFeedNew> GetMentorNewsFeedNew()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    //Get all user details 
                    string strcmd = "select NewsId,StartDate,EndDate,NewsItem,Description,CreatedDate from tblMentorNewsFeed";

                    SqlCommand cmd = new SqlCommand(strcmd, con);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    List<NewsFeedNew> li = new List<NewsFeedNew>();
                    
                    if (dr.HasRows)
                    {
                        dr.Read();
                        {
                            NewsFeedNew NF = new NewsFeedNew();
                            NF.NewsId = Convert.ToInt32(dr["NewsId"]);
                            NF.StartDate = Convert.ToDateTime(dr["StartDate"]);
                            NF.EndDate = Convert.ToDateTime(dr["EndDate"]);
                            NF.NewsItem = dr["NewsItem"].ToString();
                            NF.Description = dr["Description"].ToString();
                            NF.CreatedDate = Convert.ToDateTime(dr["CreatedDate"]);
                            li.Add(NF);
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
                Log.Error("" + ex);
                return null;

            }
        }

        public bool UpdateSessionAcceptReject(bool CDFResponse, int CDFId, int studentId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string strCommand = "update tbl_Session set CDF_Acceptance=@CDF_Acceptance where CDF_Id=@CDF_Id and Student_Id=@Student_Id";
                    SqlCommand cmd = new SqlCommand(strCommand, con);
                    cmd.Parameters.AddWithValue("@CDF_Acceptance", CDFResponse);
                    cmd.Parameters.AddWithValue("@CDF_Id", CDFId);
                    cmd.Parameters.AddWithValue("@Student_Id", studentId);
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

        public BankDtl GetBankDetails(int userID)
        {
            try
            { 
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                //Get all user details 
                string strcmd = "SELECT accountHolderName,accountNumber,bankName,branchName,ifscNo from tblUserBankDetails WHERE uId ="+ userID ;

                SqlCommand cmd = new SqlCommand(strcmd, con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                    BankDtl BD = new BankDtl();
                    if (dr.HasRows)
                    {
                        dr.Read();

                        {
                            BD.accountHolderName = dr["accountHolderName"].ToString();
                            BD.accountNumber = dr["accountNumber"].ToString();
                            BD.bankName = dr["bankName"].ToString();
                            BD.branchName = dr["branchName"].ToString();
                            BD.ifscNo = dr["ifscNo"].ToString();

                        }
                        return BD;
                    }
                    else {
                        return null;
                    }
            }
        }
            catch (Exception ex)
            {
                Log.Error("" + ex);
                return null;

            }
}

        // Get all CDF details
        public CDFProfile GetAllCDFDetails(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("spCDFAllDetails", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    con.Open();

                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        sdr.Read();
                        CDFProfile cdf = new CDFProfile();
                        cdf.uId = Convert.ToInt32(sdr["uId"]);
                        cdf.userTypeId = Convert.ToInt32(sdr["userTypeid"]);
                        cdf.fname = sdr["fname"].ToString();
                        cdf.lname = sdr["lname"].ToString();
                        cdf.gender = sdr["gender"].ToString();
                        cdf.dob = Convert.ToDateTime(sdr["dob"]);
                        cdf.contactNo = sdr["contactNo"].ToString();
                        cdf.email = sdr["email"].ToString();
                        cdf.password = sdr["password"].ToString();
                        cdf.city = sdr["name"].ToString();
                        cdf.pincode = sdr["pincode"].ToString();
                        cdf.address = sdr["address"].ToString();
                        cdf.cdfApproved = sdr["cdfApproved"].ToString();
                        cdf.cdfLevel = Convert.ToInt32(sdr["cdfLevel"]);
                        cdf.cdfrating = Convert.ToInt32(sdr["cdfrating"]);
                        cdf.regDateTime = Convert.ToDateTime(sdr["regDateTime"]);
                        cdf.standard = sdr["standard"].ToString();
                        cdf.schoolName = sdr["schoolName"].ToString();
                        cdf.division = sdr["division"].ToString();
                        cdf.occupation = sdr["occupation"].ToString();
                        cdf.status = sdr["status"].ToString();
                        cdf.userStatus = sdr["userStatus"].ToString();
                        cdf.userSource = sdr["userSource"].ToString();
                        cdf.profileDisplayApproval = Convert.ToBoolean(sdr["profileDisplayApproval"]);
                        cdf.cdfId = sdr["cdfId"].ToString();
                        cdf.profileUpdateApproval = Convert.ToBoolean(sdr["profileUpdateApproval"]);
                        if (cdf.dateModified != null)
                        {
                            cdf.dateModified = Convert.ToDateTime(sdr["dateModified"]);
                        }
                        cdf.modifiedBy = sdr["modifiedBy"].ToString();
                        cdf.qualification = sdr["qualification"].ToString();
                        cdf.whyThisOpp = sdr["whyThisOpp"].ToString();
                        cdf.working = sdr["working"].ToString();
                        cdf.designation = sdr["designation"].ToString();
                        cdf.oraganisation = sdr["oraganisation"].ToString();
                        cdf.maritalstatus = sdr["maritalstatus"].ToString();
                        cdf.linkedin = sdr["linkedin"].ToString();
                        cdf.facebook = sdr["facebook"].ToString();
                        cdf.twitter = sdr["twitter"].ToString();
                        cdf.aboutSelf = sdr["aboutSelf"].ToString();
                        cdf.resume = sdr["resume"].ToString();
                        cdf.formalImg = sdr["formalImg"].ToString();
                        cdf.casualImg = sdr["casualImg"].ToString();
                        cdf.spouseName = sdr["spouseName"].ToString();
                        cdf.spouseAge = sdr["spouseAge"].ToString();
                        cdf.childrenAge = sdr["childrenAge"].ToString();
                        cdf.feesPay = sdr["feesPay"].ToString();
                        cdf.ndaStatus = sdr["ndaStatus"].ToString();
                        cdf.preStatus = sdr["preStatus"].ToString();
                        cdf.postStatus = sdr["postStatus"].ToString();
                        cdf.profilePicDisplay = sdr["profilePicDisplay"].ToString();
                        cdf.authCode = sdr["authCode"].ToString();
                        //if (cdf.idcard != null)
                        //{
                        cdf.idcard = Convert.ToBoolean(sdr["idcard"]);
                        //}
                        if (cdf.certificate != null)
                        {
                            cdf.certificate = Convert.ToBoolean(sdr["certificate"]);
                        }
                        if (cdf.visitingCard != null)
                        {
                            cdf.visitingCard = Convert.ToBoolean(sdr["visitingCard"]);
                        }
                        if (cdf.ndaCopy != null)
                        {
                            cdf.ndaCopy = Convert.ToBoolean(sdr["ndaCopy"]);
                        }
                        if (cdf.batchId != null)
                        {
                            cdf.batchId = Convert.ToInt32(sdr["batchId"]);
                        }
                        cdf.comments = sdr["comments"].ToString();
                        cdf.refundStatus = sdr["refundStatus"].ToString();
                        if (cdf.refundAmount != null)
                        {
                            cdf.refundAmount = Convert.ToInt32(sdr["refundAmount"]);
                        }
                        if (cdf.childTestStatus != null)
                        {
                            cdf.childTestStatus = Convert.ToBoolean(sdr["childTestStatus"]);
                        }
                        if (cdf.childSessionStatus != null)
                        {
                            cdf.childSessionStatus = Convert.ToBoolean(sdr["childSessionStatus"]);
                        }
                        if (cdf.spouseTestStatus != null)
                        {
                            cdf.spouseTestStatus = Convert.ToBoolean(sdr["spouseTestStatus"]);
                        }
                        if (cdf.shadowSession != null)
                        {
                            cdf.shadowSession = Convert.ToInt32(sdr["shadowSession"]);
                        }
                        cdf.accountDetails = sdr["accountDetails"].ToString();

                        cdf.yearsOfExperience = Convert.ToInt32(sdr["yearsOfExperience"]);

                        cdf.fieldOfWork = sdr["fieldOfWork"].ToString();
                        cdf.modeOfWork = sdr["modeOfWork"].ToString();
                        cdf.industrySector = sdr["industrySector"].ToString();
                        cdf.remark = sdr["remark"].ToString();
                        cdf.classification = sdr["classification"].ToString();

                        cdf.accountHolderName = sdr["accountHolderName"].ToString();
                        cdf.accountNumber = sdr["accountNumber"].ToString();
                        cdf.bankName = sdr["bankName"].ToString();
                        cdf.branchName = sdr["branchName"].ToString();
                        cdf.ifscNo = sdr["ifscNo"].ToString();
                        cdf.state = sdr["state"].ToString();
                        cdf.address2 = sdr["address2"].ToString();
                        cdf.countryCode = sdr["countryCode"].ToString();
                        cdf.CountryID = Convert.ToInt32(sdr["CountryId"]);
                        cdf.stateName = sdr["StateName"].ToString();
                        cdf.cityName = sdr["CityName"].ToString();
                        return cdf;
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

        public bool PutProImg(int uid, profilePic pc)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {

                    string strQuery = "update tblUserDetails set formalImg = '" + pc.profilePicName + "' where uId = " + uid;

                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        return true;
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

        public string getUserPic(int uId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string ProfilePicURL = "";
                    string strQuery = "select isnull(formalImg,'NA') as formalImg from tblUserDetails where uId=" + uId;
                    SqlCommand cmd = new SqlCommand(strQuery,con);
                    con.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        sdr.Read();
                        ProfilePicURL = sdr["formalImg"].ToString();
                        return ProfilePicURL;
                    }
                    return "Not Available";
                }

            }
            catch (Exception ex)
            {
                Log.Error("" + ex);
                return null;
            }
        }

        public bool UpdateUserProfilePic(int uId,string ImageName)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {


                    // string imgfile = emailId + "_formal_" + uId;//
                    //  string strQuery = "update tblUserDetails set formalImg = '" + imgfile.ToString() + "' where uId = "+ uId;

                     string strQuery = "update tblUserDetails set formalImg = " + ImageName + " where uId = "+ uId;

                    SqlCommand cmd = new SqlCommand(strQuery,con);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        return true;
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

        //public string UpdateImg(string email, int uId, string ImageName)
        //{
        //    try
        //    {
        //        var httpRequest = HttpContext.Current.Request;
        //        foreach (string file in httpRequest.Files)
        //        {
        //            var postedFile1 = httpRequest.Files[file];
        //        }
        //            string fileName = email + "_formal_" + uId+ ".png";
        //        // string Img_filePath = "~/Uploads/" + fileName;
        //        //New Code

        //       // string queryString = @"http://www.dheya.com/cdf-dashboard/doc/demo/";
        //        // var filePath = HttpContext.Current.Server.MapPath(queryString + fileName);
        //        string qu = "~/ImgDemo/"+ ImageName;
        //        //var filePath = HttpContext.Current.Server.MapPath(qu + fileName); 
        //        //var postedFile = httpRequest.Files[fileName];

        //        byte[] imageArray = System.IO.File.ReadAllBytes(HttpContext.Current.Server.MapPath(qu));
        //        string base64ImageRepresentation = Convert.ToBase64String(imageArray);

        //        return base64ImageRepresentation;
        //        //string filename = ImageName;
        //        //FileStream f = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
        //        //int size = (int)f.Length; 
        //        //byte[] MyData = new byte[f.Length + 1];
        //        //f.Read(MyData, 0, size);
        //        //f.Close();
        //        //return Convert.ToBase64String(MyData);



        //        // file_image.PostedFile.SaveAs(Server.MapPath(ConfigurationManager.AppSettings["imageFormalPath"].ToString() + imgfile));

        //        //System.IO.Stream fs = postedFile.InputStream;
        //        //System.IO.BinaryReader br = new System.IO.BinaryReader(fs);
        //        //Byte[] bytes = br.ReadBytes((Int32)fs.Length);
        //        //string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);

        //        //if (base64String != "")
        //        //{
        //        //    return true;
        //        //}
        //        //else
        //        //    return false;
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error("" + ex);
        //        return null;
        //    }

        //}

        public IEnumerable<IndustrySect> IndSector()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                List<IndustrySect> li = new List<IndustrySect>();
                string SqlComm = "Select distinct basic_info2 from tbl_career_master";
                SqlCommand cmd = new SqlCommand(SqlComm, con);
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        IndustrySect ind = new IndustrySect();
                       // ind.IndSecValue = Convert.ToInt32(sdr["ca_id"]);
                        ind.IndustrySector = sdr["basic_info2"].ToString();
                        li.Add(ind);
                    }
                    return li;
                }
                else
                    return null;
            }
        }

        public IEnumerable<yearOfExp> GetYerOfExp()
        {
            try
            {
                List<yearOfExp> li = new List<yearOfExp>();
                for (int i = 0; i <= 50; i++)
                {
                    yearOfExp ye = new yearOfExp();
                    ye.value = i;
                    ye.YearOfExperience = i;
                    li.Add(ye);
                }
                return li;
            }
            catch (Exception ex)
            {
                Log.Error("" + ex);
                return null;
            }
        }

        public List<FieldOfWork> GetFieldOfWorkData()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlComnd = "Select distinct ISNULL(Career_category,'No') as Career_category from tbl_career_master";
                SqlCommand cmd = new SqlCommand(sqlComnd,con);
                con.Open();
                List<FieldOfWork> li = new List<FieldOfWork>();
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        FieldOfWork fw = new FieldOfWork();
                       // fw.value = Convert.ToInt32(sdr["id"]);
                        fw.fieldOfWorkList = sdr["Career_category"].ToString();
                        li.Add(fw);
                    }
                    sdr.Close();
                    return li;
                }
                else
                    return null;

            }
        }

        internal CDFProfile.CDFLevelDetails GetCDFLevelDetails(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("spCDFLevelDetails", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    con.Open();

                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        sdr.Read();
                        CDFLevelDetails cdf = new CDFLevelDetails();
                        cdf.uId = Convert.ToInt32(sdr["uId"]);
                        cdf.CDFLevel = Convert.ToInt32(sdr["cdfLevel"]);
                        
                        return cdf;
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

        public IEnumerable<resourcesPath> GetResourcesPathFromId(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("GetResourcesPathByID", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    con.Open();
                    List<resourcesPath> lr = new List<resourcesPath>();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            resourcesPath rs = new resourcesPath();
                            rs.id = Convert.ToInt32(dr["id"]);
                            rs.previewPath = ConfigurationManager.AppSettings["docfolderpath"].ToString() + dr["preview_path"].ToString();   
                            lr.Add(rs);
                        }
                        return lr;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("" + ex);
                return null;
            }
        }

        // Get all CDF Education List
        public List<CDFEducation> GetCDFEducation(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string cdfEduQuery = "SELECT * FROM tblEducation where uId = " + id + "";
                    SqlCommand cmd = new SqlCommand(cdfEduQuery, con);
                    con.Open();

                    List<CDFEducation> le = new List<CDFEducation>();

                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            CDFEducation edu = new CDFEducation();
                            edu.eduId = Convert.ToInt32(dr["id"]);
                            edu.college = dr["cdf_college"].ToString();
                            edu.degree = dr["cdf_degree"].ToString();
                            edu.description = dr["cdf_description"].ToString();
                            edu.grade = dr["cdf_grade"].ToString();

                            le.Add(edu);
                        }
                        return le;
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

        // Get selected education details
        public CDFEducation GetCDFEducationMoreDetails(int id, int eduId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("spCDFEducationMoreDetails", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@uId", id);
                    cmd.Parameters.AddWithValue("@eduId", eduId);
                    con.Open();

                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        dr.Read();

                        CDFEducation edu = new CDFEducation();
                        edu.eduId = Convert.ToInt32(dr["id"]);
                        edu.college = dr["cdf_college"].ToString();
                        edu.degree = dr["cdf_degree"].ToString();
                        edu.description = dr["cdf_description"].ToString();
                        edu.grade = dr["cdf_grade"].ToString();

                        return edu;
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

        // Get all CDF Experience List
        public List<CDFExperience> GetCDFExperience(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string cdfExpQuery = "SELECT * FROM tblExperience where uId = " + id + "";
                    SqlCommand cmd = new SqlCommand(cdfExpQuery, con);
                    con.Open();

                    List<CDFExperience> lx = new List<CDFExperience>();

                    SqlDataReader dr2 = cmd.ExecuteReader();
                    if (dr2.HasRows)
                    {
                        while (dr2.Read())
                        {
                            CDFExperience exp = new CDFExperience();
                            exp.expId = Convert.ToInt32(dr2["id"]);
                            exp.company = dr2["cdf_company"].ToString();
                            exp.position = dr2["cdf_position"].ToString();
                            //exp.job_start_date = Convert.ToDateTime(dr2["cdf_job_start_date"]);
                            //exp.job_end_date = Convert.ToDateTime(dr2["cdf_job_end_date"]);

                            object sqlDateTime = dr2["cdf_job_start_date"];
                            exp.job_start_date = (sqlDateTime == System.DBNull.Value)
                                ? (DateTime?)null
                                : Convert.ToDateTime(sqlDateTime);

                            object sqlDateTime1 = dr2["cdf_job_end_date"];
                            exp.job_end_date = (sqlDateTime1 == System.DBNull.Value)
                                ? (DateTime?)null
                                : Convert.ToDateTime(sqlDateTime1);


                            exp.location = dr2["cdf_location"].ToString();
                            exp.position_discription = dr2["cdf_position_discription"].ToString();

                            lx.Add(exp);
                        }
                        return lx;
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

        // Get selected experience details
        public CDFExperience GetCDFExperienceMoreDetails(int id, int expId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("spCDFExperienceMoreDetails", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@uId", id);
                    cmd.Parameters.AddWithValue("@expId", expId);
                    con.Open();

                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        dr.Read();

                        CDFExperience exp = new CDFExperience();
                        exp.uId = Convert.ToInt32(dr["uId"]);
                        exp.expId = Convert.ToInt32(dr["id"]);
                        exp.company = dr["cdf_company"].ToString();
                        exp.position = dr["cdf_position"].ToString();
                        //exp.job_start_date = Convert.ToDateTime(dr["cdf_job_start_date"]);
                        //exp.job_end_date = Convert.ToDateTime(dr["cdf_job_end_date"]);

                        object sqlDateTime = dr["cdf_job_start_date"];
                        exp.job_start_date = (sqlDateTime == System.DBNull.Value)
                            ? (DateTime?)null
                            : Convert.ToDateTime(sqlDateTime);

                        object sqlDateTime1 = dr["cdf_job_end_date"];
                        exp.job_end_date = (sqlDateTime1 == System.DBNull.Value)
                            ? (DateTime?)null
                            : Convert.ToDateTime(sqlDateTime1);


                        exp.location = dr["cdf_location"].ToString();
                        exp.position_discription = dr["cdf_position_discription"].ToString();

                        return exp;
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

        // CDF Details update
        public bool PutCDFDetailsUpdate(int id, CDFProfile cdfProfile)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {

                    SqlCommand cmd = new SqlCommand("spCDFUpdateDetails", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@uId", id);
                    cmd.Parameters.AddWithValue("@fname", cdfProfile.fname);
                    cmd.Parameters.AddWithValue("@lname", cdfProfile.lname);
                    cmd.Parameters.AddWithValue("@contactNo", cdfProfile.contactNo);
                    cmd.Parameters.AddWithValue("@cityid", cdfProfile.cityid);
                    cmd.Parameters.AddWithValue("@gender", cdfProfile.gender);
                    cmd.Parameters.AddWithValue("@dob", cdfProfile.dob);
                    // cmd.Parameters.AddWithValue("@profileUpdateApproval", cdfProfile.profileUpdateApproval);
                    //cmd.Parameters.AddWithValue("@profileDisplayApproval", cdfProfile.profileDisplayApproval);
                    //cmd.Parameters.AddWithValue("@dateModified", cdfProfile.dateModified);
                    cmd.Parameters.AddWithValue("@pincode", cdfProfile.pincode);
                    cmd.Parameters.AddWithValue("@qualification", cdfProfile.qualification);
                    cmd.Parameters.AddWithValue("@designation", cdfProfile.designation);
                    cmd.Parameters.AddWithValue("@yearsOfExperience", cdfProfile.yearsOfExperience);
                    
                    //New Fields
                    cmd.Parameters.AddWithValue("@aboutSelf", cdfProfile.aboutSelf);
                    cmd.Parameters.AddWithValue("@fieldOfWork", cdfProfile.fieldOfWork);
                    cmd.Parameters.AddWithValue("@modeOfWork", cdfProfile.modeOfWork);
                    cmd.Parameters.AddWithValue("@industrySector", cdfProfile.industrySector);
                    cmd.Parameters.AddWithValue("@CountryID", cdfProfile.CountryID);
                    cmd.Parameters.AddWithValue("@CountryCode", cdfProfile.countryCode);
                    cmd.Parameters.AddWithValue("@Address2", cdfProfile.address2);
                    // new fields

                    cmd.Parameters.AddWithValue("@StateId", cdfProfile.stateID);
                    cmd.Parameters.AddWithValue("@StateName", cdfProfile.stateName);
                    cmd.Parameters.AddWithValue("@CityName", cdfProfile.cityName);


                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        return true;
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

        // CDF Bank Details update
        public bool PutCDFBankDetailUpdate(int id, BankDtl cdfBank)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("spCDFBankDetailUpdate", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@uId", id);
                    cmd.Parameters.AddWithValue("@accountHolderName", cdfBank.accountHolderName);
                    cmd.Parameters.AddWithValue("@accountNumber", cdfBank.accountNumber);
                    cmd.Parameters.AddWithValue("@bankName", cdfBank.bankName);
                    cmd.Parameters.AddWithValue("@branchName", cdfBank.branchName);
                    cmd.Parameters.AddWithValue("@ifscNo", cdfBank.ifscNo);

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
                Log.Error("" + ex);
                return false;
            }
        }

        // CDF Education Add
        public string PostCDFEducationAdd(CDFEducation CDFEducation)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("spCDFAddEducation", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    CDFEducation cdfEdu = new CDFEducation();
                    cmd.Parameters.AddWithValue("@uId", CDFEducation.uId);
                    cmd.Parameters.AddWithValue("@college", CDFEducation.college);
                    cmd.Parameters.AddWithValue("@degree", CDFEducation.degree);
                    cmd.Parameters.AddWithValue("@description", CDFEducation.description);
                    cmd.Parameters.AddWithValue("@grade", CDFEducation.grade);

                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        return "Record has been successfully added";
                    }
                    else
                    {
                        return "No record added";
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("" + ex);
                return "Something went wrong! Error:" + ex.Message;
            }

        }

        // CDF Education Update
        public string PutCDFEducationUpdate(int id, int eduId, CDFEducation cdfeducation)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("spCDFUpdateEducation", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@uId", id);
                    cmd.Parameters.AddWithValue("@eduId", eduId);
                    cmd.Parameters.AddWithValue("@college", cdfeducation.college);
                    cmd.Parameters.AddWithValue("@degree", cdfeducation.degree);
                    cmd.Parameters.AddWithValue("@description", cdfeducation.description);
                    cmd.Parameters.AddWithValue("@grade", cdfeducation.grade);

                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        return "Record has been successfully updated";
                    }
                    else
                    {
                        return "No record updated";
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("" + ex);
                return "Something went wrong! Error:" + ex.Message;
            }
        }

        // CDF Education Delete
        public string DeleteCDFEducation(int id, int eduId)   //, CDFEducation CDFEducationDel
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("spCDFDeleteEducation", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@uId", id);
                    cmd.Parameters.AddWithValue("@eduId", eduId);

                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        return "Record has been successfully deleted";
                    }
                    else
                    {
                        return "No record Deleted";
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("" + ex);
                return "Something went wrong! Error:" + ex.Message;
            }
        }

        // CDF Experience Add
        public string PostCDFExperienceAdd(CDFExperience CDFExperience)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    //object d1 = CDFExperience.job_start_date;
                  
                    //if (d1 == null)
                    //{
                    //    CDFExperience.job_start_date = null;
                    //}

                    //object d2 = CDFExperience.job_end_date;
                    //if (d2 == null)
                    //{
                    //    //System.DBNull.Value
                    //    CDFExperience.job_end_date = null;
                    //}


                    //object sqlDateTime = dr2["cdf_job_start_date"];
                    //exp.job_start_date = (sqlDateTime == System.DBNull.Value)
                    //    ? (DateTime?)null
                    //    : Convert.ToDateTime(sqlDateTime);


                    //object sqlDateTime1 = CDFExperience.job_start_date;
                    //CDFExperience.job_start_date = (sqlDateTime1 == " ")
                    //      ? (DateTime?)null
                    //      : Convert.ToDateTime(sqlDateTime1);

                    //object sqlDateTime2 = CDFExperience.job_end_date;
                    //CDFExperience.job_end_date = (sqlDateTime2 == " ")
                    //      ? (DateTime?)null
                    //      : Convert.ToDateTime(sqlDateTime2);

                    SqlCommand cmd = new SqlCommand("spCDFAddExperience", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@uId", CDFExperience.uId);
                    cmd.Parameters.AddWithValue("@company", CDFExperience.company);
                    cmd.Parameters.AddWithValue("@position", CDFExperience.position);
                    cmd.Parameters.AddWithValue("@job_start_date", CDFExperience.job_start_date);
                    cmd.Parameters.AddWithValue("@job_end_date", CDFExperience.job_end_date);
                    cmd.Parameters.AddWithValue("@location", CDFExperience.location);
                    cmd.Parameters.AddWithValue("@position_discription", CDFExperience.position_discription);

                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        return "Record has been successfully added";
                    }
                    else
                    {
                        return "No record added";
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("" + ex);
                return "Something went wrong! Error:" + ex.Message;
            }
        }

        // CDF Experience Update
        public string PutCDFExperienceUpdate(int id,int expId, CDFExperience CDFExperience)  //int id,
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("spCDFUpdateExperience", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@uId", id);
                    cmd.Parameters.AddWithValue("@expId", expId);
                    cmd.Parameters.AddWithValue("@company", CDFExperience.company);
                    cmd.Parameters.AddWithValue("@position", CDFExperience.position);
                    cmd.Parameters.AddWithValue("@job_start_date", CDFExperience.job_start_date);
                    cmd.Parameters.AddWithValue("@job_end_date", CDFExperience.job_end_date);
                    cmd.Parameters.AddWithValue("@location", CDFExperience.location);
                    cmd.Parameters.AddWithValue("@position_discription", CDFExperience.position_discription);

                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        return "Record has been successfully updated";
                    }
                    else
                    {
                        return "No record updated";
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("" + ex);
                return "Something went wrong! Error:" + ex.Message;
            }
        }

        // CDF Experience Delete
        public string DeleteCDFExperience(int expId)  //int id, 
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("spCDFDeleteExperience", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                   // cmd.Parameters.AddWithValue("@uId", id);
                    cmd.Parameters.AddWithValue("@expId", expId);

                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        return "Record has been successfully deleted";
                    }
                    else
                    {
                        return "No record deleted";
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("" + ex);
                return "Something went wrong! Error:" + ex.Message;
            }
        }

        //public string PutProfileImageupdate(int uid, string email, string imagePath, ProfilePicUpdate ProfilePicUpdate)
        //{
        //    try
        //    {
        //        using (SqlConnection con = new SqlConnection(connectionString))
        //        {
        //            //if (imagePath.PostedFile.ContentLength < 2097152) // 2 MB 1024*KB of file size
        //            //{
        //            //formal_Image Upload code with rename in user emailid+_formal_+id
        //            string img = imagePath;
        //                img = img.Substring(img.LastIndexOf('.'));
        //                string imgfile = email + "_formal_" + uid + img;
        //            imagePath.
        //                PostedFile.SaveAs(Server.MapPath(ConfigurationManager.AppSettings["imageFormalPath"].ToString() + imgfile));

        //                // update data in table tblUserDetails for CDF-PostRegistration
        //                string str = "update tblUserDetails set formalImg = '" + imgfile.ToString() + "' where uId = '" + uid + "'";
        //                int i = dbContext.ExecNonQuery(str);
        //                div_msg.Visible = true;
        //                div_msg.Attributes["class"] = "alert alert-success";
        //                div_msg.InnerText = "Successfully uploaded your image";
        //            //}
        //            //else
        //            //{
        //            //    div_msg.Visible = true;
        //            //    div_msg.Attributes["class"] = "alert alert-danger";
        //            //    div_msg.InnerText = "Selected image file size is more than 1 MB. So, please upload it again with less than 1 MB size.";
        //            //}
        //            SqlCommand cmd = new SqlCommand("spCDFUpdateExperience", con);
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@uId", uid);
        //            cmd.Parameters.AddWithValue("@email", email);
        //            cmd.Parameters.AddWithValue("@company", CDFExperience.company);


        //            con.Open();
        //            int i = cmd.ExecuteNonQuery();
        //            if (i > 0)
        //            {
        //                return "Successfully uploaded your image";
        //            }
        //            else
        //            {
        //                return "No record updated";
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error("" + ex);
        //        return "Something went wrong! Error:" + ex.Message;
        //    }
        //}

        //public async Task<HttpResponseMessage> PostProfileImageupdate(int id, string email, string imagePath)
        //{
        //    Dictionary<string, object> dict = new Dictionary<string, object>();
        //    try
        //    {

        //        var httpRequest = HttpContext.Current.Request;

        //        foreach (string file in httpRequest.Files)
        //        {
        //            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);

        //            var postedFile = httpRequest.Files[file];
        //            if (postedFile != null && postedFile.ContentLength > 0)
        //            {

        //                int MaxContentLength = 1024 * 1024 * 1; //Size = 1 MB

        //                IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
        //                var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
        //                var extension = ext.ToLower();
        //                if (!AllowedFileExtensions.Contains(extension))
        //                {

        //                    var message = string.Format("Please Upload image of type .jpg,.gif,.png.");

        //                    dict.Add("error", message);
        //                    return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
        //                }
        //                else if (postedFile.ContentLength > MaxContentLength)
        //                {

        //                    var message = string.Format("Please Upload a file upto 1 mb.");

        //                    dict.Add("error", message);
        //                    return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
        //                }
        //                else
        //                {

        //                    YourModelProperty.imageurl = userInfo.email_id + extension;
        //                    //  where you want to attach your imageurl

        //                    //if needed write the code to update the table

        //                    var filePath = HttpContext.Current.Server.MapPath("~/Userimage/" + userInfo.email_id + extension);
        //                    //Userimage myfolder name where i want to save my image
        //                    postedFile.SaveAs(filePath);

        //                }
        //            }

        //            var message1 = string.Format("Image Updated Successfully.");
        //            return Request.CreateErrorResponse(HttpStatusCode.Created, message1); ;
        //        }
        //        var res = string.Format("Please Upload a image.");
        //        dict.Add("error", res);
        //        return Request.CreateResponse(HttpStatusCode.NotFound, dict);
        //    }
        //    catch (Exception ex)
        //    {
        //        var res = string.Format("some Message");
        //        dict.Add("error", res);
        //        return Request.CreateResponse(HttpStatusCode.NotFound, dict);
        //    }
        //}



        public List<Resources> GetAllResources()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("spCDFResources", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    List<Resources> lr = new List<Resources>();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            Resources rs = new Resources();
                            rs.id = Convert.ToInt32(dr["id"]);
                            rs.pId = Convert.ToInt32(dr["p_id"]);
                            rs.docName = dr["doc_name"].ToString();
                            //rs.filePath = ConfigurationManager.AppSettings["docfolderpath"].ToString() + dr["path"].ToString();
                            rs.tooltip = dr["tooltip"].ToString();
                            rs.previewPath = dr["preview_path"].ToString();
                            lr.Add(rs);
                        }
                        return lr;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("" + ex);
                return null;
            }
        }

        public List<Resources> GetResourcesFromId(int pid)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("spCDFResourcesById", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@pid", pid);
                    con.Open();
                    List<Resources> lr = new List<Resources>();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            Resources rs = new Resources();
                            rs.id = Convert.ToInt32(dr["id"]);
                            rs.pId = Convert.ToInt32(dr["p_id"]);
                            rs.docName = dr["doc_name"].ToString();
                            rs.filePath = ConfigurationManager.AppSettings["docfolderpath"].ToString() + dr["path"].ToString();
                            rs.tooltip = dr["tooltip"].ToString();
                            rs.previewPath = ConfigurationManager.AppSettings["docfolderpath"].ToString() + dr["preview_path"].ToString();
                            lr.Add(rs);
                        }
                        return lr;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("" + ex);
                return null;
            }
        }

        public List<NewsFeed> GetNewsFeed()
        {
            try
            {
                using (SqlConnection connection1 = new SqlConnection(connectionString))
                {
                    string query_newsfeed = "Select top 3 id,title,description,DateDiff(day, dateCreated, (SWITCHOFFSET(SYSDATETIMEOFFSET(), '+05:30'))) as days From tblNewsFeed order by dateCreated desc";
                    SqlCommand command = new SqlCommand(query_newsfeed, connection1);
                    connection1.Open();
                    
                    List<NewsFeed> lin = new List<NewsFeed>();
                    SqlDataReader sdr = command.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            NewsFeed nf = new NewsFeed();
                            nf.id = Convert.ToInt32(sdr["id"]);
                            nf.title = sdr["title"].ToString();
                            nf.description = sdr["description"].ToString();
                            nf.days = sdr["days"].ToString();

                            lin.Add(nf);
                        }
                        return lin;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("" + ex);
                return null;
            }
        }

        public List<CDFNetwork> GetCDFNetwork()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("spCDFNetwork", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    con.Open();

                    List<CDFNetwork> li = new List<CDFNetwork>();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            CDFNetwork cn = new CDFNetwork();                            
                            cn.city = sdr["city"].ToString();
                            cn.latitude = sdr["latitude"].ToString();
                            cn.longitude = sdr["longitude"].ToString();

                            li.Add(cn);
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
                Log.Error("" + ex);
                return null;
            }
        }

        public bool UpdateProfilePic(int uId, string fileName)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string strQuery = "update tblUserDetails set formalImg = " + fileName + " where uId = " + uId;

                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        return true;
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
        public string GetImg(int uid)
        {
            try
            {
                string formalImg = "";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string strQuery = " select formalImg from tblUserDetails where uid =" + uid;



                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    //   con.Open();
                    //SqlDataReader dr = cmd.ExecuteReader();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            formalImg = ds.Tables[0].Rows[i]["formalImg"].ToString();
                        }
                    }
                    return formalImg;
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
