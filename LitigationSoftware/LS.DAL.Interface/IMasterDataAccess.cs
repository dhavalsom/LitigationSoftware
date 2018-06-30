using LS.Models;
using System.Collections.Generic;

namespace LS.DAL.Interface
{
    public interface IMasterDataAccess
    {
        List<Company> GetCompanies();
        bool CreateCompany(Company comp);
        List<FYAY> GetFYAY();
        List<ITSection> GetITSection();
        ITSectionResponse InsertUpdateITSection(ITSection objITSection);
        List<ITHeadMaster> GetITHeadMaster();
        List<ITSubHeadMaster> GetITSubHeadMaster(int? itHeadId);
    }
}
