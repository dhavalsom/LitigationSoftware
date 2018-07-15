using LS.BL.Interface;
//using LS.BL.Library.Helpers;
using LS.DAL.Interface;
using LS.Models;
using System;

namespace LS.BL.Library
{
    public class SignIn : ISignIn
    {
        #region Declarations
        ISignInDataAccess _signInDA;
        #endregion

        public SignIn(ISignInDataAccess SignInDA)
        {
            this._signInDA = SignInDA;
        }

        public UserLogin InitiateSignInProcess(UserLogin user)
        {
            try
            {
                return this._signInDA.InitiateSignInProcess(user);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                //Log
            }
        }

        #region IDisposable
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            { }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
