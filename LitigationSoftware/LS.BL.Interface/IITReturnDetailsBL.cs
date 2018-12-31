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
        ITReturnDocumentsResponse InsertUpdateITReturnDocuments(ITReturnDocuments itReturnDocuments
            , string operation);
        ITReturnDocumentsResponse GetITReturnDocumentsList(int? companyId, 
            int? fyayId, int? itReturnDetailsId, int? itHeadId, int? itReturnDocumentId);
        ITReturnDetailsListResponse GetLitigationAndSimulation(int companyId);
        LAndSCommentsResponse GetLAndSCommentList(int? companyId, int? itSubHeadId);
        LAndSCommentsResponse InsertUpdateLAndSComments(LAndSComments landsComments
           , string operation);
        SPIncomeDetailsResponse GetSPIncomeDetailsList(int? itReturnDetailsId, int? itHeadId);
        SPIncomeDetailsResponse InsertUpdateSPIncomeDetails(SPIncomeDetails spIncomeDetails
           , string operation);
        BusinessLossDetailsResponse InsertUpdateBusinessLossDetails
            (BusinessLossDetails businessLossDetails, string operation);
        BusinessLossDetailsResponse GetBusinessLossDetailsList(int? companyId, int? fyayId
            , int? itSectionCategoryId, int? businessLossDetailsId);
        MATCreditDetailsResponse InsertUpdateMATCreditDetails
            (MATCreditDetails matCreditDetails, string operation);
        MATCreditDetailsResponse GetMATCreditDetailsList(int? companyId, int? fyayId
            , int? itSectionCategoryId, int? matCreditDetailsId);
    }
}
