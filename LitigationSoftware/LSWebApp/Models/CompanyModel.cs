using System;
using System.Collections.Generic;
using LS.Models;
using LSWebApp.Infrastructure;

namespace LSWebApp.Models
{
    public class CompanyModel : ViewModelBase
    {
        public Company companyObject { get; set; }
        public List<CompanyCategory> CompanyCategoriesList { get; set; }

        public CompanyModel() : base(Pages.Dashboard)
        {
        }
    }
}