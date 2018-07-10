using LS.Models;
using System;
using System.Collections.Generic;

namespace LS.BL.Interface
{
    public interface IITReturnDetailsBL : IDisposable
    {
        ITReturnComplexAPIModelResponse InsertorUpdateITReturnDetails(ITReturnComplexAPIModel itreturnDetails);
        ComplianceDocumentsResponse InsertUpdateComplianceDocuments(ComplianceDocuments complianceDocuments, string operation);
        ComplianceDocumentsResponse GetComplianceDocumentsList(int companyId, int fyayId, int? complianceId, int? complianceDocumentId);
        ITReturnDetailsListResponse GetExistingITReturnDetailsList(int companyId, int fyayId, int? itsectionid, int? itreturnid);
        List<ITReturnDetailsExtension> GetExistingITReturnDetailsExtension(int? itreturnid);
    }
}
