using LS.BL.Interface;
using LS.DAL.Interface;
using LS.Models;
using System;
using System.Collections.Generic;

namespace LS.BL.Library
{
	public class CompanyDashboardBL : ICompanyDashboard
	{
		#region Declarations

		ICompanyDashboardDataAccess _dashboardDA;

		#endregion

		#region Constructors

		public CompanyDashboardBL(ICompanyDashboardDataAccess dashboardDA)
		{
			this._dashboardDA = dashboardDA;
		}

		#endregion

		#region ICompanyDashboard Methods

		public CompetitorTaxRateReportResponse GetCompetitorTaxRates(int CompanyId)
		{
			try
			{
				return this._dashboardDA.GetCompetitorTaxRates(CompanyId);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				//Log
			}
		}

		public ITReturnProvisionReportResponse GetITReturnProvisions(int CompanyId, int NoOfYears)
		{
			try
			{
				return this._dashboardDA.GetITReturnProvisions(CompanyId, NoOfYears);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				//Log
			}
		}

		#endregion

		#region IDisposable

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{ }
		}
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		
		#endregion

	}
}
