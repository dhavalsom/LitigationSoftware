using LS.Models;
using System.Collections.Generic;

namespace LS.DAL.Interface
{
    public interface IITReturnDetailsDataAccess
    {
        ITReturnDetailsResponse InsertorUpdateITReturnDetails(ITReturnDetails itReturnDetails);
        ComplianceDocumentsResponse InsertUpdateComplianceDocuments(ComplianceDocuments complianceDocuments, string operation);
        ComplianceDocumentsResponse GetComplianceDocumentsList(int companyId, int fyayId, int? complianceId, int? complianceDocumentId);
    }
}
