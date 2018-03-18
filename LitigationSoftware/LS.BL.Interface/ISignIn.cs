using LS.Models;
using System;

namespace LS.BL.Interface
{
    public interface ISignIn : IDisposable
    {
        SignInResponse InitiateSignInProcess(UserLogin user);
    }
}
