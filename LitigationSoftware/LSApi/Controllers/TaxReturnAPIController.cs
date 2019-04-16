using LS.BL.Interface;
using LS.Models;
using Ninject;
using System.Collections.Generic;
using System.Web.Http;

namespace LSApi.Controllers
{
    [RoutePrefix("api/TaxReturnAPI")]
    public class TaxReturnAPIController : ApiController
    {
        #region Declarations

        private readonly IKernel _Kernel;

        #endregion

        #region Constructor

        public TaxReturnAPIController()
        {
            _Kernel = new StandardKernel();
            _Kernel.Load(new LS.Modules.TaxIntegrationModule());
        }

        #endregion

        #region Actions
        
        [HttpPost]
        [Route("InsertorUpdateITReturnDetails")]
        public ITReturnComplexAPIModelResponse InsertorUpdateITReturnDetails(
            ITReturnComplexAPIModel itreturn, string operation)
        {
            var ItReturnObj = _Kernel.Get<IITReturnDetailsBL>();
            var ItReturnResult = ItReturnObj.InsertorUpdateITReturnDetails(itreturn, operation);
            return ItReturnResult;
        }

        [HttpGet]
        [Route("GetComplianceDocumentsList")]
        // GET: api/TaxReturnAPI/GetComplianceDocumentsList
        public ComplianceDocumentsResponse GetComplianceDocumentsList(int companyId, int fyayId, 
            int? complianceId, int? complianceDocumentId)
        {
            var ItReturnObj = _Kernel.Get<IITReturnDetailsBL>();
            return ItReturnObj.GetComplianceDocumentsList(companyId, fyayId, complianceId, complianceDocumentId);
        }

        // POST: api/InsertUpdateComplianceDocuments
        [HttpPost]
        [Route("InsertUpdateComplianceDocuments")]
        public ComplianceDocumentsResponse InsertUpdateComplianceDocuments([FromBody]ComplianceDocuments complianceDocuments)
        {
            var ItReturnObj = _Kernel.Get<IITReturnDetailsBL>();
            var complianceDocumentsResult = ItReturnObj.InsertUpdateComplianceDocuments(complianceDocuments, string.Empty);
            return complianceDocumentsResult;
        }

        // POST: api/DeleteComplianceDocuments
        [HttpPost]
        [Route("DeleteComplianceDocuments")]
        public ComplianceDocumentsResponse DeleteComplianceDocuments([FromBody]ComplianceDocuments complianceDocuments)
        {
            var ItReturnObj = _Kernel.Get<IITReturnDetailsBL>();
            var complianceDocumentsResult = ItReturnObj.InsertUpdateComplianceDocuments(complianceDocuments, "Delete");
            return complianceDocumentsResult;
        }

        [HttpGet]
        [Route("GetITReturnDocumentsList")]
        // GET: api/TaxReturnAPI/GetITReturnDocumentsList
        public ITReturnDocumentsResponse GetITReturnDocumentsList(int? companyId,
            int? fyayId, int? itReturnDetailsId, int? itHeadId, int? itReturnDocumentId,
            int? documentCategoryId, int? subDocumentCategoryId)
        {
            var ItReturnObj = _Kernel.Get<IITReturnDetailsBL>();
            return ItReturnObj.GetITReturnDocumentsList(companyId, fyayId
                , itReturnDetailsId, itHeadId, itReturnDocumentId
                , documentCategoryId, subDocumentCategoryId);
        }

        // POST: api/InsertUpdateITReturnDocuments
        [HttpPost]
        [Route("InsertUpdateITReturnDocuments")]
        public ITReturnDocumentsResponse InsertUpdateITReturnDocuments
            ([FromBody]ITReturnDocuments itReturnDocuments)
        {
            var ItReturnObj = _Kernel.Get<IITReturnDetailsBL>();
            itReturnDocuments.Active = true;
            var itReturnDocumentsResult = ItReturnObj.InsertUpdateITReturnDocuments
                (itReturnDocuments, string.Empty);
            return itReturnDocumentsResult;
        }

        // POST: api/DeleteITReturnDocuments
        [HttpPost]
        [Route("DeleteITReturnDocuments")]
        public ITReturnDocumentsResponse DeleteITReturnDocuments
            ([FromBody]ITReturnDocuments itReturnDocuments)
        {
            var ItReturnObj = _Kernel.Get<IITReturnDetailsBL>();
            var itReturnDocumentsResult = ItReturnObj.InsertUpdateITReturnDocuments(itReturnDocuments, "Delete");
            return itReturnDocumentsResult;
        }

        [HttpGet]
        [Route("GetExistingITReturnDetailsList")]
        // GET: api/TaxReturnAPI/GetExistingITReturnDetailsList
        public ITReturnDetailsListResponse GetExistingITReturnDetailsList(int companyId, int fyayId, int? itsectionid, int? itreturnid)
        {
            var ItReturnObj = _Kernel.Get<IITReturnDetailsBL>();
            return ItReturnObj.GetExistingITReturnDetailsList(companyId, fyayId, itsectionid, itreturnid);
        }

        [HttpGet]
        [Route("GetExistingITReturnDetailsExtension")]
        // GET: api/TaxReturnAPI/GetExistingITReturnDetailsExtension
        public List<ITReturnDetailsExtension> GetExistingITReturnDetailsExtension(int? itreturnid)
        {
            var ItReturnObj = _Kernel.Get<IITReturnDetailsBL>();
            return ItReturnObj.GetExistingITReturnDetailsExtension(itreturnid);
        }

        [HttpGet]
        [Route("GetLitigationAndSimulation")]
        // GET: api/TaxReturnAPI/GetLitigationAndSimulation
        public ITReturnDetailsListResponse GetLitigationAndSimulation(int companyId)
        {
            var ItReturnObj = _Kernel.Get<IITReturnDetailsBL>();
            return ItReturnObj.GetLitigationAndSimulation(companyId);
        }

        [HttpGet]
        [Route("GetLAndSCommentList")]
        // GET: api/TaxReturnAPI/GetLAndSCommentList
        public LAndSCommentsResponse GetLAndSCommentList(int? companyId, int? itSubHeadId)
        {
            var ItReturnObj = _Kernel.Get<IITReturnDetailsBL>();
            return ItReturnObj.GetLAndSCommentList(companyId, itSubHeadId);
        }

        // POST: api/InsertUpdateLAndSComments
        [HttpPost]
        [Route("InsertUpdateLAndSComments")]
        public LAndSCommentsResponse InsertUpdateLAndSComments
            ([FromBody]LAndSComments landsComments)
        {
            var ItReturnObj = _Kernel.Get<IITReturnDetailsBL>();
            var landsCommentsResult = ItReturnObj.InsertUpdateLAndSComments(landsComments, null);
            return landsCommentsResult;
        }

        [HttpGet]
        [Route("GetSPIncomeDetailsList")]
        // GET: api/TaxReturnAPI/GetSPIncomeDetailsList
        public SPIncomeDetailsResponse GetSPIncomeDetailsList(int? itReturnDetailsId, int? itHeadId)
        {
            var ItReturnObj = _Kernel.Get<IITReturnDetailsBL>();
            return ItReturnObj.GetSPIncomeDetailsList(itReturnDetailsId, itHeadId);
        }

        // POST: api/InsertUpdateSPIncomeDetails
        [HttpPost]
        [Route("InsertUpdateSPIncomeDetails")]
        public SPIncomeDetailsResponse InsertUpdateSPIncomeDetails
            ([FromBody]SPIncomeDetails spIncomeDetails)
        {
            var ItReturnObj = _Kernel.Get<IITReturnDetailsBL>();
            var spIncomeDetailsResult = ItReturnObj.InsertUpdateSPIncomeDetails(spIncomeDetails, null);
            return spIncomeDetailsResult;
        }


        [HttpGet]
        [Route("GetBusinessLossDetailsList")]
        // GET: api/TaxReturnAPI/GetBusinessLossDetailsList
        public BusinessLossDetailsResponse GetBusinessLossDetailsList(int? companyId, int? fyayId
            , int? itSectionCategoryId, int? businessLossDetailsId)
        {
            var ItReturnObj = _Kernel.Get<IITReturnDetailsBL>();
            return ItReturnObj.GetBusinessLossDetailsList(companyId, fyayId
            , itSectionCategoryId, businessLossDetailsId);
        }

        // POST: api/InsertUpdateBusinessLossDetails
        [HttpPost]
        [Route("InsertUpdateBusinessLossDetails")]
        public BusinessLossDetailsResponse InsertUpdateBusinessLossDetails
            ([FromBody]BusinessLossDetails businessLossDetails)
        {
            var ItReturnObj = _Kernel.Get<IITReturnDetailsBL>();
            var businessLossDetailsResult = ItReturnObj.InsertUpdateBusinessLossDetails(businessLossDetails, null);
            return businessLossDetailsResult;
        }

        [HttpGet]
        [Route("GetMATCreditDetailsList")]
        // GET: api/TaxReturnAPI/GetMATCreditDetailsList
        public MATCreditDetailsResponse GetMATCreditDetailsList(int? companyId, int? fyayId
            , int? itSectionCategoryId, int? matCreditDetailsId)
        {
            var ItReturnObj = _Kernel.Get<IITReturnDetailsBL>();
            return ItReturnObj.GetMATCreditDetailsList(companyId, fyayId
            , itSectionCategoryId, matCreditDetailsId);
        }

        // POST: api/InsertUpdateMATCreditDetails
        [HttpPost]
        [Route("InsertUpdateMATCreditDetails")]
        public MATCreditDetailsResponse InsertUpdateMATCreditDetails
            ([FromBody]MATCreditDetails matCreditDetails)
        {
            var ItReturnObj = _Kernel.Get<IITReturnDetailsBL>();
            var matCreditDetailsResult = ItReturnObj.InsertUpdateMATCreditDetails(matCreditDetails, null);
            return matCreditDetailsResult;
        }

        [HttpGet]
        [Route("GetRefundDetailsList")]
        // GET: api/TaxReturnAPI/GetRefundDetailsList
        public RefundDetailsListResponse GetRefundDetailsList
            (int ITReturnDetailsID, int? ITHeadMasterID, int? FYAYID)
        {
            var ItReturnObj = _Kernel.Get<IITReturnDetailsBL>();
            return ItReturnObj.GetRefundDetailsList(ITReturnDetailsID, ITHeadMasterID, FYAYID);
        }

        // POST: api/InsertUpdateRefundDetails
        [HttpPost]
        [Route("InsertUpdateRefundDetails")]
        public RefundDetailsResponse InsertUpdateRefundDetails
            ([FromBody]List<RefundDetails> refundDetailsList)
        {
            var ItReturnObj = _Kernel.Get<IITReturnDetailsBL>();
            var result = new RefundDetailsResponse
            {
                IsSuccess = true
            };
            foreach (var item in refundDetailsList)
            {
                if (result.IsSuccess)
                {
                    item.Active = true;
                    result = ItReturnObj.InsertUpdateRefundDetails(item);
                }
            }
            return result;
        }
        #endregion
    }
}
