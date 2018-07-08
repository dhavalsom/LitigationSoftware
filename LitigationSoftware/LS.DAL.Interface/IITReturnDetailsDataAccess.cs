using LS.Models;
using System.Collections.Generic;

namespace LS.DAL.Interface
{
    public interface IITReturnDetailsDataAccess
    {
        ITReturnComplexAPIModelResponse InsertorUpdateITReturnDetails(ITReturnComplexAPIModel itReturnDetails);
        ComplianceDocumentsResponse InsertUpdateComplianceDocuments(ComplianceDocuments complianceDocuments, string operation);
        ComplianceDocumentsResponse GetComplianceDocumentsList(int companyId, int fyayId, int? complianceId, int? complianceDocumentId);
        ITReturnDetailsListResponse GetExistingITReturnDetailsList(int companyId, int fyayId);
    }
}
