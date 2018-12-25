using LS.BL.Interface;
using LS.DAL.Interface;
using LS.Models;
using System;
using System.Collections.Generic;

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

        public ITReturnDetailsListResponse GetExistingITReturnDetailsList(int companyId, int fyayId, int? itsectionid, int? itreturnid)
        {
            try
            {
                return this._itReturnDA.GetExistingITReturnDetailsList(companyId, fyayId, itsectionid,itreturnid);
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
        
        public List<ITReturnDetailsExtension> GetExistingITReturnDetailsExtension(int? itreturnid)
        {
            try
            {
                return this._itReturnDA.GetExistingITReturnDetailsExtension(itreturnid);
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

        public ITReturnDocumentsResponse InsertUpdateITReturnDocuments(ITReturnDocuments itReturnDocuments
            , string operation)
        {
            try
            {
                return this._itReturnDA.InsertUpdateITReturnDocuments(itReturnDocuments, operation);
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

        public ITReturnDocumentsResponse GetITReturnDocumentsList(int? companyId,
            int? fyayId, int? itReturnDetailsId, int? itHeadId, int? itReturnDocumentId)
        {
            try
            {
                return this._itReturnDA.GetITReturnDocumentsList(companyId, fyayId,
                    itReturnDetailsId, itHeadId, itReturnDocumentId);
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

        public ITReturnDetailsListResponse GetLitigationAndSimulation(int companyId)
        {
            try
            {
                return this._itReturnDA.GetLitigationAndSimulation(companyId);
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

        public LAndSCommentsResponse GetLAndSCommentList(int? companyId, int? itSubHeadId)
        {
            try
            {
                return this._itReturnDA.GetLAndSCommentList(companyId, itSubHeadId);
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

        public LAndSCommentsResponse InsertUpdateLAndSComments(LAndSComments landsComments
           , string operation)
        {
            try
            {
                return this._itReturnDA.InsertUpdateLAndSComments(landsComments, operation);
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

        public SPIncomeDetailsResponse GetSPIncomeDetailsList(int? itReturnDetailsId, int? itHeadId)
        {
            try
            {
                return this._itReturnDA.GetSPIncomeDetailsList(itReturnDetailsId, itHeadId);
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

        public SPIncomeDetailsResponse InsertUpdateSPIncomeDetails(SPIncomeDetails spIncomeDetails
           , string operation)
        {
            try
            {
                return this._itReturnDA.InsertUpdateSPIncomeDetails(spIncomeDetails, operation);
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

        public BusinessLossDetailsResponse InsertUpdateBusinessLossDetails
            (BusinessLossDetails businessLossDetails, string operation)
        {
            try
            {
                businessLossDetails.Active = true;
                return this._itReturnDA.InsertUpdateBusinessLossDetails(businessLossDetails, operation);
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

        public BusinessLossDetailsResponse GetBusinessLossDetailsList(int? companyId, int? fyayId
            , int? itSectionCategoryId, int? businessLossDetailsId)
        {
            try
            {
                return this._itReturnDA.GetBusinessLossDetailsList(companyId, fyayId
                    , itSectionCategoryId, businessLossDetailsId);
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
