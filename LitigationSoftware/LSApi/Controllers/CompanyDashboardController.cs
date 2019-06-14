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
		[Route("ITReturnProvisions")]
		public ITReturnProvisionReportResponse GetITReturnProvisions(int CompanyId, int NoOfYears)
		{
			var dashboardObj = _Kernel.Get<ICompanyDashboard>();
			var result = dashboardObj.GetITReturnProvisions(CompanyId, NoOfYears);
			return result;
		}
			

		[HttpPost]
		[Route("ChartData")]
		public ChartDataResponse GetChartData(ChartDataModel chartDataModel)
		{
			var dashboardObj = _Kernel.Get<ICompanyDashboard>();
			var result = dashboardObj.GetChartData(chartDataModel);
			return result;
		}

		#endregion


	}
}
