using LS.BL.Interface;
using LS.Models;
using Ninject;
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

        // POST: api/InsertorUpdateITReturnDetails
        //[HttpPost]
        //[Route("InsertorUpdateITReturnDetails")]
        //public ITReturnDetailsResponse InsertorUpdateITReturnDetails([FromBody]ITReturnDetails itreturn)
        //{
        //    var ItReturnObj = _Kernel.Get<IITReturnDetailsBL>();
        //    var ItReturnResult = ItReturnObj.InsertorUpdateITReturnDetails(itreturn);
        //    return ItReturnResult;
        //}

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
        [Route("GetExistingITReturnDetailsList")]
        // GET: api/TaxReturnAPI/GetExistingITReturnDetailsList
        public ITReturnDetailsListResponse GetExistingITReturnDetailsList(int companyId, int fyayId)
        {
            var ItReturnObj = _Kernel.Get<IITReturnDetailsBL>();
            return ItReturnObj.GetExistingITReturnDetailsList(companyId, fyayId);
        }

        #endregion
    }
}
