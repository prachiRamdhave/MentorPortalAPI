using CDFAPI.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CDFAPI.Repository
{
    public class ProductRepository
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public List<Product> GetProducts()
        {
            try
            {
                List<Product> lst = new List<Product>();
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "select * from [dbo].[tblProductMaster] where status='ACTIVE'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            Product p = new Product();
                            p.pid = Convert.ToInt32(dr["pid"]);
                            p.ProdName = dr["ProdName"].ToString();
                            lst.Add(p);
                        }
                        return lst;
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