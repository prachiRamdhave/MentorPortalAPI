using CDFAPI.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace CDFAPI.Repository
{
    public class LeadGraphRepository
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        string constr = ConfigurationManager.ConnectionStrings["CRM_ConnectionString"].ConnectionString;
        public List<OwnLeadGraph> GetLeadOwnGraph(string dheyaEmail)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(constr))
                {
                    //Get data for referrals (own)
                    string monthLeadCount = "Select count(id) as Count, MONTHNAME(date_entered) AS Month From suitecrm.leads where deleted = 0 and refered_by ='" + dheyaEmail + "' AND Year(date_entered) = YEAR(CURDATE()) group by MONTHNAME(date_entered) order by MONTH(date_entered) asc";
                    con.Open();
                    MySqlCommand cmd2 = new MySqlCommand(monthLeadCount, con);
                    MySqlDataReader dr2 = cmd2.ExecuteReader();
                    //Check if table has rows for required query

                    if (dr2.HasRows)
                    {
                        List<OwnLeadGraph> li = new List<OwnLeadGraph>();

                        while (dr2.Read())
                        {
                            OwnLeadGraph ol = new OwnLeadGraph();
                            ol.monthNameOwn = dr2["Month"].ToString();
                            ol.monthOwnLeadCount = Convert.ToInt32(dr2["Count"]);
                            li.Add(ol);

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
    }
}