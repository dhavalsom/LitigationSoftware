using LS.Models;
using LSWebApp.Infrastructure;
using System.Collections.Generic;

namespace LSWebApp.Models
{
    public class CompanyList : ViewModelBase
    {
        public int UserId { get; set; }
        public List<Company> Companies;

        public CompanyList(): base(Pages.Dashboard)
        {
            Companies = new List<Company>();
        }
    }

    public class CompanyCategoryModel
    {
        public List<CompanyCategory> CompanyCategories;

        public CompanyCategoryModel()
        {
            CompanyCategories = new List<CompanyCategory>();
        }
    }

}