using LS.Models;
using System.Collections.Generic;

namespace LS.DAL.Interface
{
    public interface IITReturnDetailsDataAccess
    {
        ITReturnComplexAPIModelResponse InsertorUpdateITReturnDetails(ITReturnComplexAPIModel itReturnDetails);
        ComplianceDocumentsResponse InsertUpdateComplianceDocuments(ComplianceDocuments complianceDocuments, string operation);
        ComplianceDocumentsResponse GetComplianceDocumentsList(int companyId, int fyayId, int? complianceId, int? complianceDocumentId);
        ITReturnDetailsListResponse GetExistingITReturnDetailsList(int companyId, int fyayId, int? itsectionid, int? itreturnid);
        List<ITReturnDetailsExtension> GetExistingITReturnDetailsExtension(int? itreturnid);
        ITReturnDocumentsResponse InsertUpdateITReturnDocuments(ITReturnDocuments itReturnDocuments
        , string operation);
        ITReturnDocumentsResponse GetITReturnDocumentsList(int? companyId,
            int? fyayId, int? itReturnDetailsId, int? itHeadId, int? itReturnDocumentId);
        ITReturnDetailsListResponse GetLitigationAndSimulation(int companyId);
        LAndSCommentsResponse GetLAndSCommentList(int? companyId, int? itSubHeadId);
        LAndSCommentsResponse InsertUpdateLAndSComments(LAndSComments landsComments
           , string operation);
    }
}
