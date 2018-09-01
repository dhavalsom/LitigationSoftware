using LS.Models;
using System.Collections.Generic;


namespace LSWebApp.Models
{
    public class CompanyList
    {
        public int UserId { get; set; }
        public List<Company> Companies;

        public CompanyList()
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