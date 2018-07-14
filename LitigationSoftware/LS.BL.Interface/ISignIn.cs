using LS.Models;
using System;

namespace LS.BL.Interface
{
    public interface ISignIn : IDisposable
    {
        UserLogin InitiateSignInProcess(UserLogin user);
    }
}
