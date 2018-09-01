using System;
using System.Collections.Generic;
using LS.Models;

namespace LSWebApp.Models
{
    public class CompanyModel
    {
        public Company companyObject { get; set; }
        public List<CompanyCategory> CompanyCategoriesList { get; set; }
    }
}