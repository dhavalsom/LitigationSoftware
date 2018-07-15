using System;
using System.Collections.Generic;

namespace LS.Models
{
    public class ITReturnDetails : BaseEntity
    {
        #region Properties
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string PANNumber { get; set; }
        public int FYAYID { get; set; }
        public string FinancialYear { get; set; }
        public string AssessmentYear { get; set; }
        public int ITSectionID { get; set; }
        public string ITSectionDescription { get; set; }
        public DateTime? ITReturnFillingDate { get; set; }
        public DateTime? ITReturnDueDate { get; set; }
        public decimal? HousePropIncome { get; set; }
        public decimal? IncomefromCapGainsNonSTT { get; set; }
        public decimal? IncomefromCapGainsSTT { get; set; }
        public bool? IncomefromBusinessProf { get; set; }
        public decimal? UnabsorbedDepreciation { get; set; }
        public decimal? Broughtforwardlosses { get; set; }
        public decimal? IncomeFromOtherSources { get; set; }
        public decimal? DeductChapterVIA { get; set; }
        public decimal? ProfitUS115JB { get; set; }
        public decimal? AdvanceTax1installment { get; set; }
        public decimal? AdvanceTax2installment { get; set; }
        public decimal? AdvanceTax3installment { get; set; }
        public decimal? AdvanceTax4installment { get; set; }
        public decimal? TDS { get; set; }
        public decimal? TCSPaidbyCompany { get; set; }
        public decimal? SelfAssessmentTax { get; set; }
        public decimal? MATCredit { get; set; }
        public decimal? InterestUS234A { get; set; }
        public decimal? InterestUS234B { get; set; }
        public decimal? InterestUS234C { get; set; }
        public decimal? InterestUS244A { get; set; }
        public decimal? RefundReceived { get; set; }
        public bool? RevisedReturnFile { get; set; }
        public bool IsReturn { get; set; }

        #endregion

        #region Serialization
        public bool ShouldSerializeITReturnFillingDate()
        {
            return ITReturnFillingDate.HasValue;
        }

        public bool ShouldSerializeITReturnDueDate()
        {
            return ITReturnDueDate.HasValue;
        }

        public bool ShouldSerializeHousePropIncome()
        {
            return HousePropIncome.HasValue;
        }

        public bool ShouldSerializeIncomefromCapGainsNonSTT()
        {
            return IncomefromCapGainsNonSTT.HasValue;
        }

        public bool ShouldSerializeIncomefromCapGainsSTT()
        {
            return IncomefromCapGainsSTT.HasValue;
        }

        public bool ShouldSerializeIncomefromBusinessProf()
        {
            return IncomefromBusinessProf.HasValue;
        }

        public bool ShouldSerializeUnabsorbedDepreciation()
        {
            return UnabsorbedDepreciation.HasValue;
        }

        public bool ShouldSerializeBroughtforwardlosses()
        {
            return Broughtforwardlosses.HasValue;
        }

        public bool ShouldSerializeIncomeFromOtherSources()
        {
            return IncomeFromOtherSources.HasValue;
        }

        public bool ShouldSerializeDeductChapterVIA()
        {
            return DeductChapterVIA.HasValue;
        }

        public bool ShouldSerializeProfitUS115JB()
        {
            return ProfitUS115JB.HasValue;
        }

        public bool ShouldSerializeAdvanceTax1installment()
        {
            return AdvanceTax1installment.HasValue;
        }

        public bool ShouldSerializeAdvanceTax2installment()
        {
            return AdvanceTax2installment.HasValue;
        }

        public bool ShouldSerializeAdvanceTax3installment()
        {
            return AdvanceTax3installment.HasValue;
        }

        public bool ShouldSerializeAdvanceTax4installment()
        {
            return AdvanceTax4installment.HasValue;
        }

        public bool ShouldSerializeTDS()
        {
            return TDS.HasValue;
        }


        public bool ShouldSerializeTCSPaidbyCompany()
        {
            return TCSPaidbyCompany.HasValue;
        }

        public bool ShouldSerializeSelfAssessmentTax()
        {
            return SelfAssessmentTax.HasValue;
        }

        public bool ShouldSerializeMATCredit()
        {
            return MATCredit.HasValue;
        }

        public bool ShouldSerializeInterestUS234A()
        {
            return InterestUS234A.HasValue;
        }

        public bool ShouldSerializeInterestUS234B()
        {
            return InterestUS234B.HasValue;
        }

        public bool ShouldSerializeInterestUS234C()
        {
            return InterestUS234C.HasValue;
        }

        public bool ShouldSerializeInterestUS244A()
        {
            return InterestUS244A.HasValue;
        }

        public bool ShouldSerializeRefundReceived()
        {
            return RefundReceived.HasValue;
        }

        public bool ShouldSerializeRevisedReturnFile()
        {
            return RevisedReturnFile.HasValue;
        }


        #endregion

        #region Methods
        #endregion
    }

    public class ITReturnDetailsResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public ITReturnDetails ITReturnDetailsObject { get; set; }
        public ITReturnDetailsResponse()
        {
            ITReturnDetailsObject = new ITReturnDetails();
        }
    }

    public class ITReturnDetailsListResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public List<ITReturnDetails> ITReturnDetailsListObject { get; set; }
        public ITReturnDetailsListResponse()
        {
            ITReturnDetailsListObject = new List<ITReturnDetails>();
        }
    }
}
