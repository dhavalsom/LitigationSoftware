using System;
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

		public ITReturnProvisionReportResponse GetITReturnProvisions(int CompanyId, int NoOfYears)
		{
			ITReturnProvisionReportResponse _response = null;
			try
			{
				Log.Info("GetITReturnProvisions.Start");
				Log.Info("parameter values" + JsonConvert.SerializeObject(new { companyId = CompanyId, noOfYears = NoOfYears }));
				Command.CommandText = "SP_GET_ITR_TAX_PROVISION_REPORT";
				Command.CommandType = CommandType.StoredProcedure;
				Command.Parameters.Clear();

				Command.Parameters.AddWithValue("@COMPANY_ID", CompanyId);
				Command.Parameters.AddWithValue("@NO_OF_YEARS", NoOfYears);

				Connection.Open();
				SqlDataReader reader = Command.ExecuteReader();
				_response = new ITReturnProvisionReportResponse();
				if (reader.HasRows)
				{
					_response.IsSuccess = true;
					while (reader.Read())
					{
						_response.ITReturnProvisions.Add(new ITReturnProvisionReport()
						{
							FYAYId = int.Parse(reader["FYAYID"].ToString()),
							FinancialYear = reader["FinancialYear"] != DBNull.Value ? reader["FinancialYear"].ToString() : string.Empty,
							TaxProvisions = decimal.Parse(reader["TaxProvisions"].ToString()),
							TaxAssets = decimal.Parse(reader["TaxAssets"].ToString()),
							ContingentLiabilities = decimal.Parse(reader["ContingentLiabilities"].ToString())
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
				Log.Error("GetITReturnProvisions.Error:" + JsonConvert.SerializeObject(ex));
				LogError(ex);
				throw;
			}
			finally
			{
				Connection.Close();
				Log.Info("GetITReturnProvisions.End");
			}
			return _response;
		}

		public ChartDataResponse GetChartData(ChartDataModel chartDataModel)
		{
			string procName = this.getChartProcName(chartDataModel);
			return this.GetChartDataInternal(procName, chartDataModel.ChartParams);
		}

		#endregion

		#region Private methods 

		private IDictionary<int, string> chartProcNames = new Dictionary<int, string>()
		{
			{1, "SP_GET_COMPETITOR_TAX_RATE_REPORT" },
			{2, "SP_GET_ITR_TAX_PROVISION_REPORT" },
			{3, "SP_GET_ADVANCE_TAX_REPORT" },
			{4, "SP_GET_TAX_LIABILITY_REPORT" },
			{5, "SP_GET_TDS_CREDIT_REPORT" },
			{6, "SP_GET_AUDIT_REPORT" },
			{7, "SP_GET_TOP_LITIGIOUS_TAX_REPORT" }
		};

		private string getChartProcName(ChartDataModel chartDataModel)
		{
			string procName = string.Empty;
			if (chartProcNames.ContainsKey(chartDataModel.ChartId))
			{
				procName = chartProcNames[chartDataModel.ChartId];
			}
			return procName;
		}

		private ChartDataResponse GetChartDataInternal(string procName, IDictionary<string, object> procParams)
		{
			ChartDataResponse _response = null;
			try
			{
				Log.Info("GetChartData.Start");
				Log.Info("parameter values" + JsonConvert.SerializeObject(new { procParams = procParams }));
				Command.CommandText = procName;
				Command.CommandType = CommandType.StoredProcedure;
				Command.Parameters.Clear();

				if (procParams != null)
				{
					procParams.All(param =>
					{
						Command.Parameters.AddWithValue("@" + param.Key, param.Value);
						return true;
					});
				}
				
				Connection.Open();
				SqlDataReader reader = Command.ExecuteReader();
				_response = new ChartDataResponse();
				if (reader.HasRows)
				{
					_response.IsSuccess = true;
					while (reader.Read())
					{
						_response.Data.Add(new ChartData()
						{
							Id = int.Parse(reader["Id"].ToString()),
							CategoryName = reader["CategoryName"] != DBNull.Value ? reader["CategoryName"].ToString() : string.Empty,
							SeriesName = reader["SeriesName"] != DBNull.Value ? reader["SeriesName"].ToString() : string.Empty,
							Value = decimal.Parse(reader["Value"].ToString())
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
				Log.Error("GetChartData.Error:" + JsonConvert.SerializeObject(ex));
				LogError(ex);
				throw;
			}
			finally
			{
				Connection.Close();
				Log.Info("GetChartData.End");
			}
			return _response;
		}

		#endregion

	}
}
