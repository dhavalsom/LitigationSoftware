using Newtonsoft.Json;
using LS.DAL.Interface;
using LS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace LS.DAL.Library
{
    public class MasterDataAccess : DataAccessBase, IMasterDataAccess
    {
        #region Methods
        public List<Company> GetCompanies()
        {
            try
            {
                Log.Info("Started call to GetCompanies");
                
                Command.CommandText = "SP_GET_COMPANY";
                Command.CommandType = CommandType.StoredProcedure;
                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();
                List<Company> result = new List<Company>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(new Company
                        {
                            CompanyName = reader["CompanyName"] != DBNull.Value ? reader["CompanyName"].ToString() : null,
                            PANNumber = reader["PANNumber"] != DBNull.Value ? reader["PANNumber"].ToString() : null,
                            Id = Convert.ToInt32(reader["ID"].ToString())
                        });
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }

            finally
            {
                Connection.Close();
            }
        }


        public bool CreateCompany(Company comp)
        {
            try
            {
                Log.Info("Started call to CreateCompany");
                Log.Info("parameter values" + JsonConvert.SerializeObject(comp));
                Command.CommandText = "SP_CREATE_COMPANY";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Clear();

                SqlDataAdapter da = new SqlDataAdapter(Command);
                da.SelectCommand.Parameters.Add(new SqlParameter("@COMPANY_DETAIL_XML", SqlDbType.NVarChar, 10000));
                da.SelectCommand.Parameters["@COMPANY_DETAIL_XML"].Value = GetXMLFromObject(comp);
                da.SelectCommand.Parameters.Add(new SqlParameter("@USER_ID", SqlDbType.BigInt, 100));
                da.SelectCommand.Parameters["@USER_ID"].Value = !string.IsNullOrEmpty(comp.LoggedInUserID) ? Convert.ToInt32(comp.LoggedInUserID) : 1;
                Connection.Open();

                int? result = (int?)Command.ExecuteScalar();
                Log.Info("End call to CreateCompany with result  = " + result);

                if (result == null || result == 1)
                    return true;
                else
                    return false;

            }
            catch (Exception ex)
            {
                throw;
            }

            finally
            {
                Connection.Close();
            }
        }
        #endregion
    }
}
