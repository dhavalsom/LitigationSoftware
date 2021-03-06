﻿using LS.BL.Interface;
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

        public List<CompanyCategory> GetCompanyCategories()
        {
            try
            {
                return this._masterDA.GetCompanyCategories();
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

        public List<FYAY> GetFYAY()
        {
            try
            {
                return this._masterDA.GetFYAY();
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

        public List<ITSection> GetITSection(int categoryId)
        {
            try
            {
                return this._masterDA.GetITSection(categoryId);
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

        public List<ITSectionCategory> GetITSectionCategory()
        {
            try
            {
                return this._masterDA.GetITSectionCategory();
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

        public ITSectionResponse InsertUpdateITSection(ITSection objITSection)
        {
            try
            {
                return this._masterDA.InsertUpdateITSection(objITSection);
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

        public List<ITHeadMaster> GetITHeadMaster(bool? IsTaxComputed)
        {
            try
            {
                return this._masterDA.GetITHeadMaster(IsTaxComputed);
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

        public List<ITSubHeadMaster> GetITSubHeadMaster(int? itHeadId)
        {
            try
            {
                return this._masterDA.GetITSubHeadMaster(itHeadId);
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

        public ITSubHeadMasterResponse InsertUpdateITSubHeadMaster(ITSubHeadMaster objITSubHeadMaster)
        {
            try
            {
                return this._masterDA.InsertUpdateITSubHeadMaster(objITSubHeadMaster);
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

        public List<ComplianceMaster> GetComplianceMaster(int? complianceId)
        {
            try
            {
                return this._masterDA.GetComplianceMaster(complianceId);
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

        public ComplianceMasterResponse InsertUpdateComplianceMaster(ComplianceMaster objComplianceMaster)
        {
            try
            {
                return this._masterDA.InsertUpdateComplianceMaster(objComplianceMaster);
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

        public List<StandardData> GetStandardData(int? FYAYID, int? standarddataId)
        {
            try
            {
                return this._masterDA.GetStandardData(FYAYID,standarddataId);
            }
            catch(Exception ex)
            {
                throw;
            }
            finally
            {

            }
        }

        public List<SurchargeData> GetSurchargeData(int? FYAYID, int? surchargedataId,int? entitycategorytypeid)
        {
            try
            {
                return this._masterDA.GetSurchargeData(FYAYID, surchargedataId, entitycategorytypeid);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {

            }
        }

        public List<DocumentCategoryMaster> GetDocumentCategoryMaster(bool? IsActive)
        {
            try
            {
                return this._masterDA.GetDocumentCategoryMaster(IsActive);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {

            }
        }

        public List<SubDocumentCategoryMaster> GetSubDocumentCategoryMaster(int? documentCategoryId
            , bool? IsActive)
        {
            try
            {
                return this._masterDA.GetSubDocumentCategoryMaster(documentCategoryId, IsActive);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {

            }
        }

        public SubDocumentCategoryMasterResponse InsertUpdateSubDocumentCategoryMaster
            (SubDocumentCategoryMaster objSubDocumentCategoryMaster)
        {
            try
            {
                return this._masterDA.InsertUpdateSubDocumentCategoryMaster(objSubDocumentCategoryMaster);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {

            }
        }

        public List<Implementor> GetImplementors(int? implementorId, bool? isActive)
        {
            try
            {
                return this._masterDA.GetImplementors(implementorId, isActive);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {

            }
        }

        public CompetitorResponse InsertUpdateCompetitorMaster
            (CompetitorMaster competitorMaster)
        {
            try
            {
                return this._masterDA.InsertUpdateCompetitorMaster(competitorMaster);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {

            }
        }

        public CompetitorTaxRateResponse InsertUpdateCompetitorTaxRate
            (CompetitorTaxRate competitorTaxRate)
        {
            try
            {
                return this._masterDA.InsertUpdateCompetitorTaxRate(competitorTaxRate);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {

            }
        }

        public CompetitorTaxRateResponse GetCompetitorTaxRates
            (int companyId, bool? insertDummyRecords, bool? isActive)
        {
            try
            {
                return this._masterDA.GetCompetitorTaxRates(companyId,
                    insertDummyRecords, isActive);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {

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
