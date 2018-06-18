using LS.BL.Interface;
using LS.DAL.Interface;
using LS.Models;
using System;

namespace LS.BL.Library
{
    public class ITReturnDetailsBL:IITReturnDetailsBL
    {
        #region Declarations
        IITReturnDetailsDataAccess _itReturnDA;
        #endregion

        #region Constructors

        public ITReturnDetailsBL(IITReturnDetailsDataAccess ITReturnDA)
        {
            this._itReturnDA = ITReturnDA;
        }

        #endregion

        #region Methods

        public ITReturnDetailsResponse InsertorUpdateITReturnDetails(ITReturnDetails itreturnDetails)
        {
            try
            {
                return this._itReturnDA.InsertorUpdateITReturnDetails(itreturnDetails);
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
