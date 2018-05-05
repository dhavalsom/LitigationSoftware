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

        // POST: api/LoginAPI
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
    }
}
