using Ninject.Modules;
using LS.BL.Interface;
using LS.BL.Library;
using LS.DAL.Interface;
using LS.DAL.Library;

namespace LS.Modules
{
    public class SignInModule : NinjectModule
    {
        public override void Load()
        {
            try
            {
                Bind<ISignIn>().To<SignIn>();
                Bind<ISignInDataAccess>().To<SignInDataAccess>();
                Bind<IMaster>().To<Master>();
                Bind<IMasterDataAccess>().To<MasterDataAccess>();
            }
            catch
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
