using LS.Models;
using System.Collections.Generic;

namespace LSWebApp.Models
{
    public class ABCReportDataModel
    {
        #region Properties
        public List<ABCReport> ABCReportData { get; set; }
        #endregion

        #region Constructors
        public ABCReportDataModel()
        {
            ABCReportData = new List<ABCReport>();
        }
        #endregion

        #region Methods
        #endregion
    }
}