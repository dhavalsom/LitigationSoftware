using LS.Models;
using System;
using System.Collections.Generic;

namespace LS.BL.Interface
{
    public interface IMaster : IDisposable
    {
        List<Company> GetCompanies();
        bool CreateCompany(Company comp);
        List<FYAY> GetFYAY();
        List<ITSection> GetITSection();
        ITSectionResponse InsertUpdateITSection(ITSection objITSection);
    }
}
