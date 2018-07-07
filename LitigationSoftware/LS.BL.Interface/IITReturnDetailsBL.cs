using LS.Models;
using System;

namespace LS.BL.Interface
{
    public interface IITReturnDetailsBL : IDisposable
    {
        ITReturnComplexAPIModelResponse InsertorUpdateITReturnDetails(ITReturnComplexAPIModel itreturnDetails);
        ComplianceDocumentsResponse InsertUpdateComplianceDocuments(ComplianceDocuments complianceDocuments, string operation);
        ComplianceDocumentsResponse GetComplianceDocumentsList(int companyId, int fyayId, int? complianceId, int? complianceDocumentId);
    }
}
