using System.Collections.Generic;

namespace LS.Models
{
    public class MATCreditDetails : BaseEntity
    {
        #region Properties
        public int CompanyId { get; set; }
        public int FYAYId { get; set; }
        public int ITSectionCategoryId { get; set; }
        public decimal? BusinessLosses_BF { get; set; }
        public decimal? UnabsorbedDepreciation_BF { get; set; }
        public decimal? BusinessLosses_CY { get; set; }
        public decimal? UnabsorbedDepreciation_CY { get; set; }
        public decimal? BusinessLosses_UL { get; set; }
        public decimal? UnabsorbedDepreciation_UL { get; set; }

        #region Display Properties
        public string CompanyName { get; set; }
        public string AssessmentYear { get; set; }
        public string FinancialYear { get; set; }
        public string CategoryDesc { get; set; }

        public decimal? BusinessLosses_Total
        {
            get
            {
                return (BusinessLosses_BF.HasValue ? BusinessLosses_BF.Value : 0)
                    + (BusinessLosses_CY.HasValue ? BusinessLosses_CY.Value : 0)
                    - (BusinessLosses_UL.HasValue ? BusinessLosses_UL.Value : 0);
            }
        }

        public decimal? UnabsorbedDepreciation_Total
        {
            get
            {
                return (UnabsorbedDepreciation_BF.HasValue ? UnabsorbedDepreciation_BF.Value : 0)
                    + (UnabsorbedDepreciation_CY.HasValue ? UnabsorbedDepreciation_CY.Value : 0)
                    - (UnabsorbedDepreciation_UL.HasValue ? UnabsorbedDepreciation_UL.Value : 0);
            }
        }
        
        #endregion
        
        #endregion

        #region Serialization
        public bool ShouldSerializeBusinessLosses_BF()
        {
            return BusinessLosses_BF.HasValue;
        }        

        public bool ShouldSerializeUnabsorbedDepreciation_BF()
        {
            return UnabsorbedDepreciation_BF.HasValue;
        }
        
        public bool ShouldSerializeBusinessLosses_CY()
        {
            return BusinessLosses_CY.HasValue;
        }
        
        public bool ShouldSerializeUnabsorbedDepreciation_CY()
        {
            return UnabsorbedDepreciation_CY.HasValue;
        }
        
        public bool ShouldSerializeBusinessLosses_UL()
        {
            return BusinessLosses_UL.HasValue;
        }

        public bool ShouldSerializeUnabsorbedDepreciation_UL()
        {
            return UnabsorbedDepreciation_UL.HasValue;
        }

        #endregion

        #region Methods
        #endregion
    }

    public class MATCreditDetailsResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public List<MATCreditDetails> MATCreditDetailsList { get; set; }
        public MATCreditDetailsResponse()
        {
            MATCreditDetailsList = new List<MATCreditDetails>();
        }
    }
}
