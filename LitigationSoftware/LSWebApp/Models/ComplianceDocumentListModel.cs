using LS.Models;
using System.Collections.Generic;
using System.Web;

namespace LSWebApp.Models
{
    public class ComplianceDocumentListModel
    {
        #region Properties
        public List<ComplianceMaster> ComplianceList { get; set; }
        public List<ComplianceDocumentsDisplay> ComplianceDocumentList { get; set; }
        public int CompanyId { get; set; }
        public int FYAYId { get; set; }
        #endregion

        #region Constructors
        public ComplianceDocumentListModel()
        {
        }
        #endregion
    }
}