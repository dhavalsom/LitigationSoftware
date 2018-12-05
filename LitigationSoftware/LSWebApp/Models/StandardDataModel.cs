using LS.Models;
using LSWebApp.Infrastructure;
using System.Collections.Generic;
using System.Web;

namespace LSWebApp.Models
{
    public class StandardDataModel
    {
        #region Proeprties
        public int? FYAYId { get; set; }
        public string FinancialYear { get; set; }
        public string AssessmentYear { get; set; }
        public List<FYAY> FYAYList { get; set; }
        public StandardData StandardDataObject { get; set; }
        public List<StandardData> StandardDataObjectList { get; set; }
        #endregion

        #region Constructors
        public StandardDataModel()
        {
            StandardDataObject = new StandardData();
            StandardDataObjectList = new List<StandardData>();
        }
        #endregion
    }
}