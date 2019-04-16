using LS.Models;

namespace LS.DAL.Interface
{
    public interface ISignInDataAccess : IDataAccessBase
    {
        UserLogin InitiateSignInProcess(UserLogin user);
    }
}
