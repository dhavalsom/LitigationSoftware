using System.Collections.Generic;

namespace LS.Models
{
    public class ITReturnDocuments : BaseEntity
    {
        public int ITReturnDetailsId { get; set; }
        public int? ITHeadId { get; set; }
        public int DocumentCategoryId { get; set; }
        public int? SubDocumentCategoryId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }

        #region Serialization
        public bool ShouldSerializeITHeadId()
        {
            return ITHeadId.HasValue;
        }

        public bool ShouldSerializeSubDocumentCategoryId()
        {
            return SubDocumentCategoryId.HasValue;
        }

        #endregion
    }

    public class ITReturnDocumentsResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<ITReturnDocumentsDisplay> ITReturnDocumentsList { get; set; }
    }

    public class ITReturnDocumentsDisplay : ITReturnDocuments
    {
        public string ExcelSrNo { get; set; }
        public string ITHeadDescription { get; set; }
        public string PropertyName { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public int FYAYId { get; set; }
        public string FinancialYear { get; set; }
        public string AssessmentYear { get; set; }
        public string DocumentCategoryName { get; set; }
        public string SubDocumentCategoryName { get; set; }
    }
}
