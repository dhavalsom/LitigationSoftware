using LS.BL.Interface;
using LS.DAL.Interface;
using LS.Models;
using System;

namespace LS.BL.Library
{
    public class ReportDataBL : IReportDataBL
    {
        #region Declarations
        IReportDataAccess _reportDataDA;
        #endregion

        #region Constructors

        public ReportDataBL(IReportDataAccess reportDataDA)
        {
            this._reportDataDA = reportDataDA;
        }

        #endregion

        #region Methods
        public ABCReportResponse GetABCReportData(int companyId, bool? isAllowance)
        {
            try
            {
                return this._reportDataDA.GetABCReportData(companyId, isAllowance);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                //Log
            }
        }
        #endregion
    }
}
