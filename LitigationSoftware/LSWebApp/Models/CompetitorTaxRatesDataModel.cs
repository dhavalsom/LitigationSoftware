using LS.Models;
using System.Collections.Generic;

namespace LSWebApp.Models
{
    public class CompetitorTaxRatesDataModel
    {
        #region Properties
        public List<CompetitorTaxRate> CompetitorTaxRates { get; set; }
        public List<CompetitorMaster> Competitors { get; set; }
        public List<FYAY> FYAYList { get; set; }
        #endregion

        #region Constructors
        public CompetitorTaxRatesDataModel()
        {
            CompetitorTaxRates = new List<CompetitorTaxRate>();
            Competitors = new List<CompetitorMaster>();
            FYAYList = new List<FYAY>();
        }
        #endregion

        #region Methods
        #endregion
    }
}