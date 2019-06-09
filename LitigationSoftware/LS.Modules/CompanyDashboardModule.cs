using Ninject.Modules;
using LS.BL.Interface;
using LS.BL.Library;
using LS.DAL.Interface;
using LS.DAL.Library;

namespace LS.Modules
{
	public class CompanyDashboardModule : NinjectModule
	{
		public override void Load()
		{
			try
			{
				Bind<ICompanyDashboard>().To<CompanyDashboardBL>();
				Bind<ICompanyDashboardDataAccess>().To<CompanyDashboardDataAccess>();				
			}
			catch
			{
				throw new System.NotImplementedException();
			}
		}

	}
}
