using LS.Models;
using System;
using System.Collections.Generic;

namespace LS.BL.Interface
{
    public interface IMaster : IDisposable
    {
        List<Company> GetCompanies();
        List<CompanyCategory> GetCompanyCategories();
        bool CreateCompany(Company comp);
        List<FYAY> GetFYAY();
        List<ITSection> GetITSection(int categoryId);
        List<ITSectionCategory> GetITSectionCategory();
        ITSectionResponse InsertUpdateITSection(ITSection objITSection);
        List<ITHeadMaster> GetITHeadMaster(bool? IsTaxComputed);
        List<ITSubHeadMaster> GetITSubHeadMaster(int? itHeadId);
        ITSubHeadMasterResponse InsertUpdateITSubHeadMaster(ITSubHeadMaster objITSubHeadMaster);
        List<ComplianceMaster> GetComplianceMaster(int? complianceId);
        ComplianceMasterResponse InsertUpdateComplianceMaster(ComplianceMaster objComplianceMaster);
        List<StandardData> GetStandardData(int? FYAYID, int? standarddataId);
        List<SurchargeData> GetSurchargeData(int? FYAYID, int? surchargedataId,int? entitycategorytypeid);
        List<DocumentCategoryMaster> GetDocumentCategoryMaster(bool? IsActive);
        List<SubDocumentCategoryMaster> GetSubDocumentCategoryMaster(int? documentCategoryId
            , bool? IsActive);
        SubDocumentCategoryMasterResponse InsertUpdateSubDocumentCategoryMaster
            (SubDocumentCategoryMaster objSubDocumentCategoryMaster);
        List<Implementor> GetImplementors(int? implementorId, bool? isActive);
    }
}
