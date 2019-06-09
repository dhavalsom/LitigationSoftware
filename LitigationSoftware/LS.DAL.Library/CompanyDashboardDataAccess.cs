﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LS.DAL.Interface;
using LS.Models;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;

namespace LS.DAL.Library
{
	public class CompanyDashboardDataAccess : DataAccessBase, ICompanyDashboardDataAccess
	{
		#region ICompanyDashboardDataAccess Methods

		public CompetitorTaxRateReportResponse GetCompetitorTaxRates(int CompanyId)
		{
			CompetitorTaxRateReportResponse _response = null;
			try
			{
				Log.Info("GetCompetitorTaxRates.Start");
				Log.Info("parameter values" + JsonConvert.SerializeObject(new { companyId = CompanyId }));
				Command.CommandText = "SP_GET_COMPETITOR_TAX_RATE_REPORT";
				Command.CommandType = CommandType.StoredProcedure;
				Command.Parameters.Clear();

				Command.Parameters.AddWithValue("@COMPANY_ID", CompanyId);

				Connection.Open();
				SqlDataReader reader = Command.ExecuteReader();
				_response = new CompetitorTaxRateReportResponse();
				if (reader.HasRows)
				{
					_response.IsSuccess = true;
					while (reader.Read())
					{
						_response.CompetitorTaxRates.Add(new CompetitorTaxRateReport()
						{
							CompetitorId = int.Parse(reader["CompetitorId"].ToString()),
							CompetitorName = reader["CompetitorName"] != DBNull.Value ? reader["CompetitorName"].ToString() : string.Empty,
							FinancialYear = reader["FinancialYear"] != DBNull.Value ? reader["FinancialYear"].ToString() : string.Empty,
							TaxRate = decimal.Parse(reader["TaxRate"].ToString())
						});
					}
				}
				else
				{
					_response.IsSuccess = false;
					_response.Message = "No data found";
				}
			}
			catch (Exception ex)
			{
				Log.Error("GetCompetitorTaxRates.Error:" + JsonConvert.SerializeObject(ex));
				LogError(ex);
				throw;
			}
			finally
			{
				Connection.Close();
				Log.Info("GetCompetitorTaxRates.End");
			}
			return _response;
		}

		#endregion
	}
}