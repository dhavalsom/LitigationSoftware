using System.Collections.Generic;

namespace LS.Models
{
    public class CompetitorTaxRate : BaseEntity
    {
        public int CompetitorId { get; set; }
        public int FYAYId { get; set; }
        public decimal TaxRate { get; set; }

        #region Display Properties
        public string Description { get; set; }
        public string CompanyName { get; set; }
        public string AssessmentYear { get; set; }
        public string FinancialYear { get; set; }
        #endregion

        #region Serialization
        public bool ShouldSerializeDescription()
        {
            return false;
        }

        public bool ShouldSerializeCompanyName()
        {
            return false;
        }

        public bool ShouldSerializeAssessmentYear()
        {
            return false;
        }

        public bool ShouldSerializeFinancialYear()
        {
            return false;
        }
        #endregion
    }

    public class CompetitorTaxRateResponse
    {
        public int Id { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<CompetitorTaxRate> CompetitorTaxRates { get; set; }
    }
}

