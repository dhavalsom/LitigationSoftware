﻿using LS.BL.Interface;
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

        public ITReturnComplexAPIModelResponse InsertorUpdateITReturnDetails(ITReturnComplexAPIModel itreturnDetails)
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

        public ComplianceDocumentsResponse InsertUpdateComplianceDocuments(ComplianceDocuments complianceDocuments, string operation)
        {
            try
            {
                return this._itReturnDA.InsertUpdateComplianceDocuments(complianceDocuments, operation);
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

        public ComplianceDocumentsResponse GetComplianceDocumentsList(int companyId, int fyayId, 
            int? complianceId, int? complianceDocumentId)
        {
            try
            {
                return this._itReturnDA.GetComplianceDocumentsList(companyId, fyayId, complianceId, complianceDocumentId);
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

        public ITReturnDetailsListResponse GetExistingITReturnDetailsList(int companyId, int fyayId)
        {
            try
            {
                return this._itReturnDA.GetExistingITReturnDetailsList(companyId, fyayId);
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
