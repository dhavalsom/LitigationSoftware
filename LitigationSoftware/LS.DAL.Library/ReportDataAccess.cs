using LS.DAL.Interface;
using LS.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace LS.DAL.Library
{
    public class ReportDataAccess : DataAccessBase, IReportDataAccess
    {
        #region Methods

        public ABCReportResponse GetABCReportData(int companyId, bool? isAllowance)
        {
            try
            {
                Log.Info("Started call to GetABCReportData");
                Log.Info("parameter values" + JsonConvert.SerializeObject(new
                {
                    companyId = companyId,
                    isAllowance = isAllowance
                }));
                Command.CommandText = "SP_GET_ABC_REPORT";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.AddWithValue("@COMPANY_ID", companyId);
                if (isAllowance.HasValue)
                {
                    Command.Parameters.AddWithValue("@IS_ALLOWANCE", isAllowance);
                }
                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();
                ABCReportResponse result = new ABCReportResponse();
                if (reader.HasRows)
                {
                    result.ABCReportData = new List<ABCReport>();
                    while (reader.Read())
                    {
                        result.ABCReportData.Add(new ABCReport
                        {
                            ITSubHeadId = Convert.ToInt32(reader["ITSubHeadId"].ToString()),
                            ITSubHead = reader["ITSubHead"] != DBNull.Value ? reader["ITSubHead"].ToString() : null,
                            ITHeadId = Convert.ToInt32(reader["ITHeadId"].ToString()),
                            ITHead = reader["ITHead"] != DBNull.Value ? reader["ITHead"].ToString() : null,
                            SubHeadType = reader["SubHeadType"] != DBNull.Value ? reader["SubHeadType"].ToString() : null,
                            IsAllowance = Convert.ToBoolean(reader["IsAllowance"].ToString()),
                            Total = Convert.ToDecimal(reader["Total"].ToString()),
                            CompanyId = Convert.ToInt32(reader["CompanyId"].ToString()),
                            CompanyName = reader["CompanyName"] != DBNull.Value ? reader["CompanyName"].ToString() : null,
                        });
                    }
                }
                Log.Info("End call to GetABCReportData. Result:"
                    + JsonConvert.SerializeObject(result));
                return result;
            }
            catch (Exception ex)
            {
                Log.Error("Error in GetABCReportData. Error:"
                    + JsonConvert.SerializeObject(ex));
                LogError(ex);
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
