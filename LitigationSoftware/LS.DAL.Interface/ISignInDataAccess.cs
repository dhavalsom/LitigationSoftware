using LS.Models;

namespace LS.DAL.Interface
{
    public interface ISignInDataAccess
    {
        UserLogin InitiateSignInProcess(UserLogin user);
    }
}
