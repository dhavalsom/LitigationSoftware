using LS.Models;
using System.Collections.Generic;

namespace LSWebApp.Models
{
    public class ITReturnDocumentListModel
    {
        #region Properties
        public List<ITReturnDocumentsDisplay> ITReturnDocumentsList { get; set; }
        public int? CompanyId { get; set; }
        public int? FYAYId { get; set; }
        public int? ITReturnDetailsId { get; set; }
        public int? ITHeadId { get; set; }
        public int? ITReturnDocumentId { get; set; }
        public int? DocumentCategoryId { get; set; }
        public int? SubDocumentCategoryId { get; set; }
        public int? ITSectionId { get; set; }
        public int? ITSectionCategoryId { get; set; }
        #endregion

        #region Constructors
        public ITReturnDocumentListModel()
        {
        }
        #endregion
    }
}