using Ninject;
using LS.BL.Interface;
using LS.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace LSApi.Controllers
{
	[RoutePrefix("api/CompanyDashboardAPI")]
	public class CompanyDashboardController : ApiController
    {
		#region Declarations

		private readonly IKernel _Kernel;

		#endregion

		#region Constructor

		public CompanyDashboardController()
		{
			_Kernel = new StandardKernel();
			_Kernel.Load(new LS.Modules.CompanyDashboardModule());
		}

		#endregion

		#region Action Methods 

		[HttpGet]
		[Route("CompetitorTaxRates")]
		public CompetitorTaxRateReportResponse GetCompetitorTaxRates(int CompanyId)
		{
			var dashboardObj = _Kernel.Get<ICompanyDashboard>();
			var result = dashboardObj.GetCompetitorTaxRates(CompanyId);
			return result;
		}


		[HttpGet]
		[Route("ITReturnProvisions")]
		public ITReturnProvisionReportResponse GetITReturnProvisions(int CompanyId, int NoOfYears)
		{
			var dashboardObj = _Kernel.Get<ICompanyDashboard>();
			var result = dashboardObj.GetITReturnProvisions(CompanyId, NoOfYears);
			return result;
		}

		[HttpGet]
		[Route("QuarterlyAdvanceTaxes")]
		public QuarterlyAdvanceTaxReportResponse GetQuarterlyAdvanceTaxes(int CompanyId, int NoOfYears)
		{
			var dashboardObj = _Kernel.Get<ICompanyDashboard>();
			var result = dashboardObj.GetQuarterlyAdvanceTaxes(CompanyId, NoOfYears);
			return result;
		}

		#endregion


	}
}
