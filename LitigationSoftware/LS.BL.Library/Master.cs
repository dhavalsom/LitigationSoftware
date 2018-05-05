using LS.BL.Interface;
using LS.DAL.Interface;
using LS.Models;
using System;
using System.Collections.Generic;

namespace LS.BL.Library
{
    public class Master:IMaster
    {
        #region Declarations
        IMasterDataAccess _masterDA;
        #endregion

        #region Constructors

        public Master(IMasterDataAccess MasterDA)
        {
            this._masterDA = MasterDA;
        }

        #endregion

        #region Methods
        public List<Company> GetCompanies()
        {
            try
            {
                return this._masterDA.GetCompanies();
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

        public bool CreateCompany(Company comp)
        {
            try
            {
                return this._masterDA.CreateCompany(comp);
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
        #endregion

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
