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
		CompetitorTaxRateReportResponse GetCompetitorTaxRates(int CompanyId);
		ITReturnProvisionReportResponse GetITReturnProvisions(int CompanyId, int NoOfYears);
	}

}
