using LS.Models;
using LSWebApp.Infrastructure;
using System.Collections.Generic;
using System.Web;

namespace LSWebApp.Models
{
    public class SurchargeDataModel
    {
        #region Properties
        public int? FYAYId { get; set; }
        public string FinancialYear { get; set; }
        public string AssessmentYear { get; set; }
        public List<FYAY> FYAYList { get; set; }
        public SurchargeData SurchargeDataObject { get; set; }
        public List<SurchargeData> SurchargeDataObjectList { get; set; }
        #endregion

        #region Constructors
        public SurchargeDataModel()
        {
            SurchargeDataObject = new SurchargeData();
            SurchargeDataObjectList = new List<SurchargeData>();
        }
        #endregion
    }
}