using Ninject;
using LS.BL.Interface;
using LS.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace LSApi.Controllers
{
    [RoutePrefix("api/LoginAPI")]
    public class LoginAPIController : ApiController
    {
        #region Declarations

        private readonly IKernel _Kernel;

        #endregion

        #region Constructor

        public LoginAPIController()
        {
            //_Kernel = new StandardKernel(new OMC.Modules.SignInModule());
            _Kernel = new StandardKernel();
            _Kernel.Load(new LS.Modules.SignInModule());
        }

        #endregion

        // POST: api/LoginAPI
        [HttpPost]
        [Route("PostUserLogin")]
        public SignInResponse PostUserLogin([FromBody]UserLogin user)
        {
            var SignInObj = _Kernel.Get<ISignIn>();
            var SignInResult = SignInObj.InitiateSignInProcess(user);
            return SignInResult;
        }
    }
}
