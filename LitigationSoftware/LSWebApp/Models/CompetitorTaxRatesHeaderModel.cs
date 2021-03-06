﻿using LS.Models;
using LSWebApp.Infrastructure;

namespace LSWebApp.Models
{
    public class CompetitorTaxRatesHeaderModel : ViewModelBase
    {
        #region Properties
        public Company CompanyObject { get; set; }
        #endregion

        #region Constructors
        public CompetitorTaxRatesHeaderModel() : base(Pages.ITReturnDetailsPage)
        {            
        }
        #endregion

        #region Methods
        #endregion
    }
}