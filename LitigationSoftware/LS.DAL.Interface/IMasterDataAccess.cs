using LS.Models;
using System.Collections.Generic;

namespace LS.DAL.Interface
{
    public interface IMasterDataAccess
    {
        List<Company> GetCompanies();
        bool CreateCompany(Company comp);
    }
}
