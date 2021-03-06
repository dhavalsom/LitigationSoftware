﻿using Ninject.Modules;
using LS.BL.Interface;
using LS.BL.Library;
using LS.DAL.Interface;
using LS.DAL.Library;

namespace LS.Modules
{
    public class TaxIntegrationModule : NinjectModule
    {
        public override void Load()
        {
            try
            {
                Bind<IITReturnDetailsBL>().To<ITReturnDetailsBL>();
                Bind<IITReturnDetailsDataAccess>().To<ITReturnDetailsDataAccess>();
                Bind<IReportDataBL>().To<ReportDataBL>();
                Bind<IReportDataAccess>().To<ReportDataAccess>();
            }
            catch
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
