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
        public ITReturnComplexAPIModelResponse InsertorUpdateITReturnDetails(ITReturnComplexAPIModel itreturn)
        {
            var ItReturnObj = _Kernel.Get<IITReturnDetailsBL>();
            var ItReturnResult = ItReturnObj.InsertorUpdateITReturnDetails(itreturn);
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
            int? fyayId, int? itReturnDetailsId, int? itHeadId, int? itReturnDocumentId)
        {
            var ItReturnObj = _Kernel.Get<IITReturnDetailsBL>();
            return ItReturnObj.GetITReturnDocumentsList(companyId, fyayId
                , itReturnDetailsId, itHeadId, itReturnDocumentId);
        }

        // POST: api/InsertUpdateITReturnDocuments
        [HttpPost]
        [Route("InsertUpdateITReturnDocuments")]
        public ITReturnDocumentsResponse InsertUpdateITReturnDocuments
            ([FromBody]ITReturnDocuments itReturnDocuments)
        {
            var ItReturnObj = _Kernel.Get<IITReturnDetailsBL>();
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
        #endregion
    }
}
