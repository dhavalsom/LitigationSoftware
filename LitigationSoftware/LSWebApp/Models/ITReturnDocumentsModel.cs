using LS.Models;
using LSWebApp.Infrastructure;
using System.Collections.Generic;
using System.Web;

namespace LSWebApp.Models
{
    public class ITReturnDocumentsModel : ViewModelBase
    {
        #region Proeprties
        public int? CompanyId { get; set; }
        public string CompanyName { get; set; }
        public int? FYAYId { get; set; }
        public string FinancialYear { get; set; }
        public int? ITHeadId { get; set; }
        public int? DocumentCategoryId { get; set; }
        public int? SubDocumentCategoryId { get; set; }
        public List<FYAY> FYAYList { get; set; }
        public List<Company> CompanyList { get; set; }
        public List<ITHeadMaster> ITHeadList { get; set; }
        public HttpPostedFileBase ReportFile { get; set; }
        public ITReturnDocuments ObjComplianceDocuments { get; set; }
        public ITReturnDocumentListModel ObjITReturnDocumentListModel { get; set; }
        public List<DocumentCategoryMaster> DocumentCategoryList { get; set; }
        public List<SubDocumentCategoryMaster> SubDocumentCategoryList { get; set; }
        #endregion

        #region Constructors
        public ITReturnDocumentsModel() : base(Pages.ITReturnDetailsPage)
        {
            SubDocumentCategoryList = new List<SubDocumentCategoryMaster>();
        }
        #endregion
    }
}