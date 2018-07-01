using Ninject;
using LS.BL.Interface;
using LS.Models;
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
    }
}
