﻿using System;
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
		ITReturnProvisionReportResponse GetITReturnProvisions(int CompanyId, int NoOfYears);
		QuarterlyAdvanceTaxReportResponse GetQuarterlyAdvanceTaxes(int CompanyId, int NoOfYears);
		TaxLiabilityReportResponse GetTaxLiabilities(int CompanyId, int NoOfYears);
	}
}
