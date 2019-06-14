using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LS.Models;

namespace LS.DAL.Interface
{
	public interface ICompanyDashboardDataAccess : IDataAccessBase
	{
		ITReturnProvisionReportResponse GetITReturnProvisions(int CompanyId, int NoOfYears);		
		ChartDataResponse GetChartData(ChartDataModel chartDataModel);
	}

}
