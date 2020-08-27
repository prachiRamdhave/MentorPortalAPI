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
    public class CountryStateCityRepository
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //Get all Cities
        public List<Cities> GetAllCities()
        {
            try
            {
                List<Cities> cities = new List<Cities>();
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT A.id as cityid,A.name as cityName FROM tblCitiesMaster as A inner join tblStatesMaster as B on A.stateId=B.id inner join tblCountriesMaster as C on B.countryId=C.id where C.name='India' order by A.name", con);
                    con.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            Cities city = new Cities();
                            city.cityId = Convert.ToInt32(sdr["cityid"]);
                            city.cityName = sdr["cityName"].ToString();
                            cities.Add(city);
                        }
                        sdr.Close();
                        return cities;
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

        public List<Country> GetAllCountry()
        {
            try {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    List<Country> li = new List<Country>();
                    string strQuery = "select id,name from tblCountriesMaster";

                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    con.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            Country C = new Country();
                            C.countId = Convert.ToInt32(sdr["id"]);
                            C.countName = sdr["name"].ToString();
                            li.Add(C);
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

        //Get all Cities by state id
        public List<Cities> GetAllCitiesByStateId(int stateid)
        {
            try
            {
                List<Cities> cities = new List<Cities>();
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT id as cityid,name as cityName FROM tblCitiesMaster where stateId=" + stateid, con);
                    con.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            Cities city = new Cities();
                            city.cityId = Convert.ToInt32(sdr["cityid"]);
                            city.cityName = sdr["cityName"].ToString();
                            cities.Add(city);
                        }
                        sdr.Close();
                        return cities;
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

        //Get all Cities by city Name
        public List<Cities> GetAllCitiesByCityName(string cityName)
        {
            try
            {
                List<Cities> cities = new List<Cities>();
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT id as cityid,name as cityName FROM tblCitiesMaster where name like '%" + cityName + "%'", con);
                    con.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            Cities city = new Cities();
                            city.cityId = Convert.ToInt32(sdr["cityid"]);
                            city.cityName = sdr["cityName"].ToString();
                            cities.Add(city);
                        }
                        sdr.Close();
                        return cities;
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

        //Get all States
        public List<States> GetAllStates()
        {
            try
            {
                List<States> states = new List<States>();
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT top 100 id as stateId,name as stateName FROM tblStatesMaster", con);
                    con.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            States state = new States();
                            state.stateId = Convert.ToInt32(sdr["stateId"]);
                            state.stateName = sdr["stateName"].ToString();
                            states.Add(state);
                        }
                        sdr.Close();
                        return states;
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

        //Get all States by country id
        public List<States> GetAllStatesByCountryid(int countryid)
        {
            try
            {
                List<States> states = new List<States>();
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT id as stateId,name as stateName FROM tblStatesMaster where countryId=" + countryid, con);
                    con.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            States state = new States();
                            state.stateId = Convert.ToInt32(sdr["stateId"]);
                            state.stateName = sdr["stateName"].ToString();
                            states.Add(state);
                        }
                        sdr.Close();
                        return states;
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

        public CountryCode GetCntryCode(int countryId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {

                    string strQuery = "select CntryCode from  tblCountriesMaster where id=" + countryId;

                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    con.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    CountryCode C = new CountryCode();
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {

                            C.CntryCode = sdr["CntryCode"].ToString();
                        }
                        return C;
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

    }
}