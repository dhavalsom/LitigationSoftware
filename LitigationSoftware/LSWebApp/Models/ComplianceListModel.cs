using LS.Models;
using LSWebApp.Infrastructure;
using System.Collections.Generic;
using System.Web;

namespace LSWebApp.Models
{
    public class ComplianceListModel : ViewModelBase
    {
        #region Proeprties
        public int? CompanyId { get; set; }
        public string CompanyName { get; set; }
        public int? FYAYId { get; set; }
        public string FinancialYear { get; set; }
        public List<FYAY> FYAYList { get; set; }
        public List<Company> CompanyList { get; set; }
        public List<ComplianceMaster> ComplianceList { get; set; }
        public HttpPostedFileBase ReportFile { get; set; }
        public ComplianceDocuments ObjComplianceDocuments { get; set; }
        public ComplianceDocumentListModel ObjComplianceDocumentListModel { get; set; }
        public LitigationDDModel ComplianceListSource { get; set; }
        #endregion

        #region Constructors
        public ComplianceListModel() : base(Pages.SecondPage)
        {
        }
        #endregion
    }
}