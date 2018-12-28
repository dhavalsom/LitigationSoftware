using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.Models
{
    public class BusinessLossDetails : BaseEntity
    {
        #region Properties
        public int CompanyId { get; set; }
        public int FYAYId { get; set; }
        public int ITSectionCategoryId { get; set; }
        public decimal? IncomeCapGainsLTCG_BF { get; set; }
        public decimal? IncomeCapGainsSTCG_BF { get; set; }
        public decimal? IncomeBusinessProf_BF { get; set; }
        public decimal? IncomeSpeculativeBusiness_BF { get; set; }
        public decimal? UnabsorbedDepreciation_BF { get; set; }
        public decimal? HousePropIncome_BF { get; set; }
        public decimal? IncomeOtherSources_BF { get; set; }
        public decimal? IncomeCapGainsLTCG_CY { get; set; }
        public decimal? IncomeCapGainsSTCG_CY { get; set; }
        public decimal? IncomeBusinessProf_CY { get; set; }
        public decimal? IncomeSpeculativeBusiness_CY { get; set; }
        public decimal? UnabsorbedDepreciation_CY { get; set; }
        public decimal? HousePropIncome_CY { get; set; }
        public decimal? IncomeOtherSources_CY { get; set; }
        public decimal? IncomeCapGainsLTCG_UL { get; set; }
        public decimal? IncomeCapGainsSTCG_UL { get; set; }
        public decimal? IncomeBusinessProf_UL { get; set; }
        public decimal? IncomeSpeculativeBusiness_UL { get; set; }
        public decimal? UnabsorbedDepreciation_UL { get; set; }
        public decimal? HousePropIncome_UL { get; set; }
        public decimal? IncomeOtherSources_UL { get; set; }
        public decimal? IncomeCapGainsLTCG_UALL { get; set; }
        public decimal? IncomeCapGainsSTCG_UALL { get; set; }
        public decimal? IncomeBusinessProf_UALL { get; set; }
        public decimal? IncomeSpeculativeBusiness_UALL { get; set; }
        public decimal? UnabsorbedDepreciation_UALL { get; set; }
        public decimal? HousePropIncome_UALL { get; set; }
        public decimal? IncomeOtherSources_UALL { get; set; }

        #region Display Properties
        public bool IsCurrentYear { get; set; }
        public string CompanyName { get; set; }
        public string AssessmentYear { get; set; }
        public string FinancialYear { get; set; }
        public string CategoryDesc { get; set; }

        public decimal? IncomeCapGainsLTCG_Total
        {
            get
            {
                return (IncomeCapGainsLTCG_BF.HasValue ? IncomeCapGainsLTCG_BF.Value : 0)
                    + (IncomeCapGainsLTCG_CY.HasValue ? IncomeCapGainsLTCG_CY.Value : 0)
                    - (IncomeCapGainsLTCG_UL.HasValue ? IncomeCapGainsLTCG_UL.Value : 0)
                    - (IncomeCapGainsLTCG_UALL.HasValue ? IncomeCapGainsLTCG_UALL.Value : 0);
            }
        }

        public decimal? IncomeCapGainsSTCG_Total
        {
            get
            {
                return (IncomeCapGainsSTCG_BF.HasValue ? IncomeCapGainsSTCG_BF.Value : 0)
                    + (IncomeCapGainsSTCG_CY.HasValue ? IncomeCapGainsSTCG_CY.Value : 0)
                    - (IncomeCapGainsSTCG_UL.HasValue ? IncomeCapGainsSTCG_UL.Value : 0)
                    - (IncomeCapGainsSTCG_UALL.HasValue ? IncomeCapGainsSTCG_UALL.Value : 0);
            }
        }

        public decimal? IncomeBusinessProf_Total
        {
            get
            {
                return (IncomeBusinessProf_BF.HasValue ? IncomeBusinessProf_BF.Value : 0)
                    + (IncomeBusinessProf_CY.HasValue ? IncomeBusinessProf_CY.Value : 0)
                    - (IncomeBusinessProf_UL.HasValue ? IncomeBusinessProf_UL.Value : 0)
                    - (IncomeBusinessProf_UALL.HasValue ? IncomeBusinessProf_UALL.Value : 0);
            }
        }

        public decimal? IncomeSpeculativeBusiness_Total
        {
            get
            {
                return (IncomeSpeculativeBusiness_BF.HasValue ? IncomeSpeculativeBusiness_BF.Value : 0)
                    + (IncomeSpeculativeBusiness_CY.HasValue ? IncomeSpeculativeBusiness_CY.Value : 0)
                    - (IncomeSpeculativeBusiness_UL.HasValue ? IncomeSpeculativeBusiness_UL.Value : 0)
                    - (IncomeSpeculativeBusiness_UALL.HasValue ? IncomeSpeculativeBusiness_UALL.Value : 0);
            }
        }

        public decimal? UnabsorbedDepreciation_Total
        {
            get
            {
                return (UnabsorbedDepreciation_BF.HasValue ? UnabsorbedDepreciation_BF.Value : 0)
                    + (UnabsorbedDepreciation_CY.HasValue ? UnabsorbedDepreciation_CY.Value : 0)
                    - (UnabsorbedDepreciation_UL.HasValue ? UnabsorbedDepreciation_UL.Value : 0)
                    - (UnabsorbedDepreciation_UALL.HasValue ? UnabsorbedDepreciation_UALL.Value : 0);
            }
        }

        public decimal? HousePropIncome_Total
        {
            get
            {
                return (HousePropIncome_BF.HasValue ? HousePropIncome_BF.Value : 0)
                    + (HousePropIncome_CY.HasValue ? HousePropIncome_CY.Value : 0)
                    - (HousePropIncome_UL.HasValue ? HousePropIncome_UL.Value : 0)
                    - (HousePropIncome_UALL.HasValue ? HousePropIncome_UALL.Value : 0);
            }
        }

        public decimal? IncomeOtherSources_Total
        {
            get
            {
                return (IncomeOtherSources_BF.HasValue ? IncomeOtherSources_BF.Value : 0)
                    + (IncomeOtherSources_CY.HasValue ? IncomeOtherSources_CY.Value : 0)
                    - (IncomeOtherSources_UL.HasValue ? IncomeOtherSources_UL.Value : 0)
                    - (IncomeOtherSources_UALL.HasValue ? IncomeOtherSources_UALL.Value : 0);
            }
        }
        #endregion
        #endregion

        #region Serialization
        public bool ShouldSerializeIncomeCapGainsLTCG_BF()
        {
            return IncomeCapGainsLTCG_BF.HasValue;
        }

        public bool ShouldSerializeIncomeCapGainsSTCG_BF()
        {
            return IncomeCapGainsSTCG_BF.HasValue;
        }

        public bool ShouldSerializeIncomeBusinessProf_BF()
        {
            return IncomeBusinessProf_BF.HasValue;
        }

        public bool ShouldSerializeIncomeSpeculativeBusiness_BF()
        {
            return IncomeSpeculativeBusiness_BF.HasValue;
        }

        public bool ShouldSerializeUnabsorbedDepreciation_BF()
        {
            return UnabsorbedDepreciation_BF.HasValue;
        }

        public bool ShouldSerializeHousePropIncome_BF()
        {
            return HousePropIncome_BF.HasValue;
        }

        public bool ShouldSerializeIncomeOtherSources_BF()
        {
            return IncomeOtherSources_BF.HasValue;
        }

        public bool ShouldSerializeIncomeCapGainsLTCG_CY()
        {
            return IncomeCapGainsLTCG_CY.HasValue;
        }

        public bool ShouldSerializeIncomeCapGainsSTCG_CY()
        {
            return IncomeCapGainsSTCG_CY.HasValue;
        }

        public bool ShouldSerializeIncomeBusinessProf_CY()
        {
            return IncomeBusinessProf_CY.HasValue;
        }

        public bool ShouldSerializeIncomeSpeculativeBusiness_CY()
        {
            return IncomeSpeculativeBusiness_CY.HasValue;
        }

        public bool ShouldSerializeUnabsorbedDepreciation_CY()
        {
            return UnabsorbedDepreciation_CY.HasValue;
        }

        public bool ShouldSerializeHousePropIncome_CY()
        {
            return HousePropIncome_CY.HasValue;
        }

        public bool ShouldSerializeIncomeOtherSources_CY()
        {
            return IncomeOtherSources_CY.HasValue;
        }

        public bool ShouldSerializeIncomeCapGainsLTCG_UL()
        {
            return IncomeCapGainsLTCG_UL.HasValue;
        }

        public bool ShouldSerializeIncomeCapGainsSTCG_UL()
        {
            return IncomeCapGainsSTCG_UL.HasValue;
        }

        public bool ShouldSerializeIncomeBusinessProf_UL()
        {
            return IncomeBusinessProf_UL.HasValue;
        }

        public bool ShouldSerializeIncomeSpeculativeBusiness_UL()
        {
            return IncomeSpeculativeBusiness_UL.HasValue;
        }

        public bool ShouldSerializeUnabsorbedDepreciation_UL()
        {
            return UnabsorbedDepreciation_UL.HasValue;
        }

        public bool ShouldSerializeHousePropIncome_UL()
        {
            return HousePropIncome_UL.HasValue;
        }

        public bool ShouldSerializeIncomeOtherSources_UL()
        {
            return IncomeOtherSources_UL.HasValue;
        }

        public bool ShouldSerializeIncomeCapGainsLTCG_UALL()
        {
            return IncomeCapGainsLTCG_UALL.HasValue;
        }

        public bool ShouldSerializeIncomeCapGainsSTCG_UALL()
        {
            return IncomeCapGainsSTCG_UALL.HasValue;
        }

        public bool ShouldSerializeIncomeBusinessProf_UALL()
        {
            return IncomeBusinessProf_UALL.HasValue;
        }

        public bool ShouldSerializeIncomeSpeculativeBusiness_UALL()
        {
            return IncomeSpeculativeBusiness_UALL.HasValue;
        }

        public bool ShouldSerializeUnabsorbedDepreciation_UALL()
        {
            return UnabsorbedDepreciation_UALL.HasValue;
        }

        public bool ShouldSerializeHousePropIncome_UALL()
        {
            return HousePropIncome_UALL.HasValue;
        }

        public bool ShouldSerializeIncomeOtherSources_UALL()
        {
            return IncomeOtherSources_UALL.HasValue;
        }
        #endregion

        #region Methods
        #endregion
    }

    public class BusinessLossDetailsResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public List<BusinessLossDetails> BusinessLossDetailsList { get; set; }
        public ITReturnDetails ITReturnDetailsObject { get; set; }
        public BusinessLossDetailsResponse()
        {
            BusinessLossDetailsList = new List<BusinessLossDetails>();
        }
    }
}
