using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LS.Models;

namespace LS.BL.Interface
{
	public interface ICompanyDashboard
	{
		CompetitorTaxRateReportResponse GetCompetitorTaxRates(int CompanyId);
	}
}
