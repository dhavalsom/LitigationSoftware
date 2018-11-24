using Ninject;
using LS.BL.Interface;
using LS.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace LSApi.Controllers
{
    [RoutePrefix("api/MasterAPI")]
    public class MasterAPIController : ApiController
    {
        #region Declarations

        private readonly IKernel _Kernel;

        #endregion

        #region Constructor

        public MasterAPIController()
        {
            _Kernel = new StandardKernel();
            _Kernel.Load(new LS.Modules.SignInModule());
        }

        #endregion

        // POST: api/PostCreateCompany
        [HttpPost]
        [Route("PostCreateCompany")]
        public bool PostCreateCompany([FromBody]Company comp)
        {
            var CompObj = _Kernel.Get<IMaster>();
            var CompResult = CompObj.CreateCompany(comp);
            return CompResult;
        }

        [HttpGet]
        [Route("GetCompanyList")]
        public List<Company> GetCompanyList()
        {
            var CompObj = _Kernel.Get<IMaster>();
            var CompResult = CompObj.GetCompanies();
            return CompResult;
        }

        [HttpGet]
        [Route("GetCompanyCategoryList")]
        public List<CompanyCategory> GetCompanyCategoryList()
        {
            var CompObj = _Kernel.Get<IMaster>();
            var CompResult = CompObj.GetCompanyCategories();
            return CompResult;
        }

        [HttpGet]
        [Route("GetFYAYList")]
        public List<FYAY> GetFYAYList()
        {
            var CompObj = _Kernel.Get<IMaster>();
            var CompResult = CompObj.GetFYAY();
            return CompResult;
        }

        [HttpGet]
        [Route("GetITSectionList")]
        public List<ITSection> GetITSectionList(int categoryId)
        {
            var CompObj = _Kernel.Get<IMaster>();
            var CompResult = CompObj.GetITSection(categoryId);
            return CompResult;
        }

        [HttpGet]
        [Route("GetITSectionCategoryList")]
        public List<ITSectionCategory> GetITSectionCategoryList()
        {
            var CompObj = _Kernel.Get<IMaster>();
            var CompResult = CompObj.GetITSectionCategory();
            return CompResult;
        }


        // POST: api/MasterAPI/InsertUpdateITSection
        [HttpPost]
        [Route("InsertUpdateITSection")]
        public ITSectionResponse InsertUpdateITSection([FromBody]ITSection objITSection)
        {
            var objMaster = _Kernel.Get<IMaster>();
            var objITSectionResponse = objMaster.InsertUpdateITSection(objITSection);
            return objITSectionResponse;
        }

        [HttpGet]
        [Route("GetITHeadMaster")]
        // GET: api/MasterAPI/GetITHeadMaster
        public List<ITHeadMaster> GetITHeadMaster()
        {
            var CompObj = _Kernel.Get<IMaster>();
            return CompObj.GetITHeadMaster();
        }

        [HttpGet]
        [Route("GetITSubHeadMaster")]
        // GET: api/MasterAPI/GetITSubHeadMaster
        public List<ITSubHeadMaster> GetITSubHeadMaster(int? itHeadId)
        {
            var CompObj = _Kernel.Get<IMaster>();
            return CompObj.GetITSubHeadMaster(itHeadId);
        }

        // POST: api/MasterAPI/InsertUpdateITSubHeadMaster
        [HttpPost]
        [Route("InsertUpdateITSubHeadMaster")]
        public ITSubHeadMasterResponse InsertUpdateITSubHeadMaster([FromBody]ITSubHeadMaster objITSubHeadMaster)
        {
            var objMaster = _Kernel.Get<IMaster>();
            var objITSubHeadMasterResponse = objMaster.InsertUpdateITSubHeadMaster(objITSubHeadMaster);
            return objITSubHeadMasterResponse;
        }

        [HttpGet]
        [Route("GetComplianceMaster")]
        // GET: api/MasterAPI/GetComplianceMaster
        public List<ComplianceMaster> GetComplianceMaster(int? complianceId)
        {
            var CompObj = _Kernel.Get<IMaster>();
            return CompObj.GetComplianceMaster(complianceId);
        }

        // POST: api/MasterAPI/InsertUpdateComplianceMaster
        [HttpPost]
        [Route("InsertUpdateComplianceMaster")]
        public ComplianceMasterResponse InsertUpdateComplianceMaster([FromBody]ComplianceMaster objComplianceMaster)
        {
            var objMaster = _Kernel.Get<IMaster>();
            var objComplianceMasterResponse = objMaster.InsertUpdateComplianceMaster(objComplianceMaster);
            return objComplianceMasterResponse;
        }

        [HttpGet]
        [Route("GetStandardData")]
        // GET: api/MasterAPI/GetComplianceMaster
        public List<StandardData> GetStandardData(int? standarddataId)
        {
            var CompObj = _Kernel.Get<IMaster>();
            return CompObj.GetStandardData(standarddataId);
        }

        [HttpGet]
        [Route("GetSurchargeData")]
        // GET: api/MasterAPI/GetComplianceMaster
        public List<SurchargeData> GetSurchargeData(int? surchargedataId)
        {
            var CompObj = _Kernel.Get<IMaster>();
            return CompObj.GetSurchargeData(surchargedataId);
        }
    }
}
