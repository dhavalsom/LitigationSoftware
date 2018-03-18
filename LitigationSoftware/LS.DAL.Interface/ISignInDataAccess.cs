using LS.Models;

namespace LS.DAL.Interface
{
    public interface ISignInDataAccess
    {
        SignInResponse InitiateSignInProcess(UserLogin user);
    }
}
