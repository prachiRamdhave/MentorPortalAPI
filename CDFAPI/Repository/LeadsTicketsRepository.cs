using CDFAPI.Models;
using log4net;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Web;

namespace CDFAPI.Repository
{
    public class LeadsTicketsRepository
    {
        string connectionString = ConfigurationManager.ConnectionStrings["CRM_ConnectionString"].ConnectionString;
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public List<Leads> GetCDFLeads(string referredByEmail)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    string leadPreviewQuery = "select l.id,l.first_name,l.last_name,l.phone_mobile,ea.email_address,l.date_entered,cust.city_c,l.lead_source, "
                                            + "l.status, cust.lead_category_c,l.lead_source_description,CONCAT(u.first_name, ' ', u.last_name) as Assigned_User,u.phone_work as Number FROM suitecrm.leads as l "
                                            + "Left Outer Join suitecrm.leads_cstm as cust on l.id = cust.id_c Left Outer Join suitecrm.email_addr_bean_rel as eabl "
                                            + "ON l.id = eabl.bean_id  AND eabl.deleted=0 Left Outer Join suitecrm.email_addresses ea " 
                                            + "ON (eabl.email_address_id = ea.id ) and ea.deleted=0 Left Outer Join suitecrm.users as u " 
                                            + "on l.assigned_user_id = u.id AND u.deleted=0 where l.refered_by='" + referredByEmail + "' and l.deleted=0";

                    MySqlCommand cmd = new MySqlCommand(leadPreviewQuery, con);
                    
                    con.Open();
                    MySqlDataReader dr = cmd.ExecuteReader();
                    List<Leads> li = new List<Leads>();                    

                    if(dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            Leads ld = new Leads();
                            ld.id = dr["id"].ToString();
                            ld.fname = dr["first_name"].ToString();
                            ld.lname = dr["last_name"].ToString();
                            ld.contactNo = dr["phone_mobile"].ToString();
                            ld.email = dr["email_address"].ToString();
                           // ld.createdDate = Convert.ToDateTime(dr["date_entered"]);
                            //ld.city = dr["city_c"].ToString();
                            ld.leadStatus = dr["status"].ToString();                           
                            //ld.description = dr["lead_source_description"].ToString();
                           // ld.assignedExecutive = dr["Assigned_User"].ToString();
                            //ld.ExecutiveNo = dr["Number"].ToString();

                            li.Add(ld);
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
        
        public List<Tickets> GetCDFTickets(string fname, string lname, string dheyaEmail)
        {
            string userName = fname + " " + lname;
            try
            {
                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    string ticketPreviewQuery = "select id, state,priority,name,status,cust.issue_c as Issue,date_entered as Date,description "
                                              + "FROM suitecrm.cases left join suitecrm.cases_cstm cust on cust.id_c = suitecrm.cases.id "
                                              + "where (cust.ticket_created_by_c = '" + userName + "' or cust.ticket_created_by_c = '" + dheyaEmail +"') and suitecrm.cases.deleted = 0 ";

                    MySqlCommand cmd = new MySqlCommand(ticketPreviewQuery, con);
                    con.Open();

                    MySqlDataReader dr = cmd.ExecuteReader();
                    List<Tickets> li = new List<Tickets>();
                    if(dr.HasRows)
                    {
                        while(dr.Read())
                        {
                            Tickets tk = new Tickets();
                            tk.id = dr["id"].ToString();
                            tk.state = dr["state"].ToString();
                            tk.priority = dr["priority"].ToString();
                            tk.name = dr["name"].ToString();
                            tk.status = dr["status"].ToString();
                            //tk.issue = dr["Issue"].ToString();
                           // tk.createdDate = Convert.ToDateTime(dr["Date"]);
                           // tk.description = dr["description"].ToString();

                            li.Add(tk);
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

        public Leads GetCDFMoreLeads(string referredByEmail, string id)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    string leadMorePreviewQuery = "select l.id,l.first_name,l.last_name,l.phone_mobile,ea.email_address,l.date_entered,cust.city_c,l.lead_source, "
                                                + "l.status, cust.lead_category_c,l.lead_source_description,CONCAT(u.first_name, ' ', u.last_name) as Assigned_User,u.phone_work as Number FROM suitecrm.leads as l "
                                                + "Left Outer Join suitecrm.leads_cstm as cust on l.id = cust.id_c Left Outer Join suitecrm.email_addr_bean_rel as eabl "
                                                + "ON l.id = eabl.bean_id  AND eabl.deleted=0 Left Outer Join suitecrm.email_addresses ea "
                                                + "ON (eabl.email_address_id = ea.id ) and ea.deleted=0 Left Outer Join suitecrm.users as u "
                                                + "on l.assigned_user_id = u.id AND u.deleted=0 where l.refered_by='" + referredByEmail + "' and "
                                                + "l.id = '" + id + "' and l.deleted=0";

                    MySqlCommand cmd = new MySqlCommand(leadMorePreviewQuery, con);

                    con.Open();
                    MySqlDataReader dr = cmd.ExecuteReader();                  

                    if (dr.HasRows)
                    {
                        dr.Read();
                        
                            Leads ld = new Leads();
                            ld.id = dr["id"].ToString();
                            ld.fname = dr["first_name"].ToString();
                            ld.lname = dr["last_name"].ToString();
                            ld.contactNo = dr["phone_mobile"].ToString();
                            ld.email = dr["email_address"].ToString();
                            ld.createdDate = Convert.ToDateTime(dr["date_entered"]);
                            ld.city = dr["city_c"].ToString();
                            ld.leadStatus = dr["status"].ToString();
                            ld.description = dr["lead_source_description"].ToString();
                            ld.assignedExecutive = dr["Assigned_User"].ToString();
                            ld.ExecutiveNo = dr["Number"].ToString();
                        
                        return ld;
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

        public Tickets GetCDFMoreTickets(string fname, string lname, string dheyaEmail, string id)
        {
            string userName = fname + " " + lname;
            try
            {
                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    string ticketMorePreviewQuery = "select ca.id, ca.state,ca.priority,ca.name,ca.status,cust.issue_c as Issue,ca.date_entered as Date, "
                                              + "ca.description,cu.name as solution,cu.date_modified as modifiedDate FROM suitecrm.cases as ca "
                                              + "left join suitecrm.cases_cstm cust on ca.id = cust.id_c "
                                              + "LEFT OUTER JOIN suitecrm.aop_case_updates as cu on ca.id = cu.case_id "
                                              + "where (cust.ticket_created_by_c = '" + userName + "' or cust.ticket_created_by_c = '" + dheyaEmail + "') and "
                                              + "ca.id = '" + id + "' and ca.deleted = 0";

                    MySqlCommand cmd = new MySqlCommand(ticketMorePreviewQuery, con);
                    con.Open();

                    MySqlDataReader dr = cmd.ExecuteReader();                  
                    if (dr.HasRows)
                    {
                            dr.Read();
                       
                            Tickets tk = new Tickets();
                            tk.id = dr["id"].ToString();
                            tk.state = dr["state"].ToString();
                            tk.priority = dr["priority"].ToString();
                            tk.name = dr["name"].ToString();
                            tk.status = dr["status"].ToString();
                            tk.issue = dr["Issue"].ToString();
                            tk.createdDate = Convert.ToDateTime(dr["Date"]);
                            tk.description = dr["description"].ToString();
                            tk.solution = dr["solution"].ToString();
                            if (dr["modifiedDate"] != DBNull.Value)
                            {
                                tk.modifiedDate = Convert.ToDateTime(dr["modifiedDate"]);
                            }
                            else
                            {
                                tk.modifiedDate = null;
                            }
                        return tk;
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

        public string CreateLead(Leads Leads)
        {
            try
            {                                                                                                                                                                                                                                                                                                                                                                                                                     
                    string canddetails = "first_name=" + Leads.fname + "&last_name=" + Leads.lname + "&email1=" + Leads.email + "&phone_mobile=" + Leads.contactNo + "&enquired_for_c=" + Leads.enquiredFor + "&school_college_name_c=" + Leads.schoolCollegeName + "&pincode_c=" + Leads.pincode + "&description=" + Leads.description + ""
                                       + "&city_c=" + Leads.city + "&refered_by=" + Leads.referedBy + "&lead_category_c=" + Leads.leadCategory + "&lead_source=" + Leads.leadSource + "&Submit=Submit&campaign_id=" + ConfigurationManager.AppSettings["campaignId"].ToString() + "&assigned_user_id=1&moduleDir=Leads&client_id_address=117.223.141.186&dup_checked=1";
                    var task = new Thread(() => pushtoCRM(canddetails));
                    task.Start();
                    return "Success";              
            }
            catch (Exception ex)
            {
                Log.Error("" + ex);
                return "Something went wrong! Error:" + ex.Message;
            }
        }

        public string CreateTicket(Tickets Tickets)
        {
            try
            {
                string ticketDetails = "state=" + Tickets.state + "&priority=" + Tickets.priority + "&name=" + Tickets.name + "&issue_c=" + Tickets.issue + "&description=" + Tickets.description + "&type=" + Tickets.type + "&status=" + Tickets.status + "&account_id=" + ConfigurationManager.AppSettings["TicketaccountId"].ToString() + ""
                                   + "&ticket_created_by_c=" + Tickets.ticketCreatedBy + "&Submit=Submit&campaign_id=" + ConfigurationManager.AppSettings["TicketCampaignId"].ToString() + "&assigned_user_id=1&moduleDir=Cases&client_id_address=117.223.141.186&dup_checked=1";
                var task = new Thread(() => pushtoCRM(ticketDetails));
                task.Start();
                return "Success";                    
            }
            catch (Exception ex)
            {
                Log.Error("" + ex);
                return "Something went wrong! Error:" + ex.Message;
            }

        }

        //User Data push into CRM
        private void pushtoCRM(string strRequest)
        {
            try
            {
                HttpWebResponse objHttpWebResponse = null;
                UTF8Encoding encoding;
                string strResponse = "";
                string strURL = ConfigurationManager.AppSettings["CRMDataPushlink"].ToString();
                HttpWebRequest objHttpWebRequest;
                objHttpWebRequest = (HttpWebRequest)WebRequest.Create(strURL);
                objHttpWebRequest.ContentType = "application/x-www-form-urlencoded";
                objHttpWebRequest.PreAuthenticate = true;
                objHttpWebRequest.Method = "POST";

                //Prepare the request stream
                if (strRequest != null && strRequest != string.Empty)
                {
                    encoding = new UTF8Encoding();
                    Stream objStream = objHttpWebRequest.GetRequestStream();
                    Byte[] Buffer = encoding.GetBytes(strRequest);
                    // Post the request
                    objStream.Write(Buffer, 0, Buffer.Length);
                    objStream.Close();
                }
                objHttpWebResponse = (HttpWebResponse)objHttpWebRequest.GetResponse();
                encoding = new UTF8Encoding();
                StreamReader objStreamReader =
                    new StreamReader(objHttpWebResponse.GetResponseStream(), encoding);
                strResponse = objStreamReader.ReadToEnd();
                objHttpWebResponse.Close();
                objHttpWebRequest = null;
            }
            catch (Exception ex)
            {
                Log.Error("" + ex);
            }

        }
    }
}