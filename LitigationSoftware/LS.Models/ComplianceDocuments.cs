using System.Collections.Generic;

namespace LS.Models
{
    public class ComplianceDocuments : BaseEntity
    {
        public int ComplianceId { get; set; }
        public int CompanyId { get; set; }
        public int FYAYId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
    }

    public class ComplianceDocumentsResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<ComplianceDocumentsDisplay> ComplianceDocumentsList { get; set; }
    }

    public class ComplianceDocumentsDisplay : ComplianceDocuments
    {
        public string ComplianceDescription { get; set; }
        public string CompanyName { get; set; }
        public string FinancialYear { get; set; }
        public string AssessmentYear { get; set; }
    }
}
