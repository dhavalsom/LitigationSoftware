using LS.Models;
using System.Collections.Generic;

namespace LSWebApp.Models
{
    public class CompetitorTaxRatesDataModel
    {
        #region Properties
        public List<CompetitorTaxRate> CompetitorTaxRates { get; set; }
        #endregion

        #region Constructors
        public CompetitorTaxRatesDataModel()
        {
            CompetitorTaxRates = new List<CompetitorTaxRate>();
        }
        #endregion

        #region Methods
        #endregion
    }
}