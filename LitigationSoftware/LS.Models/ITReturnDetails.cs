using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.Models
{
    public class ITReturnDetails : BaseEntity
    {
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
        public int? HousePropIncome { get; set; }
        public int? IncomefromCapGainsNonSTT { get; set; }
        public int? IncomefromCapGainsSTT { get; set; }
        public bool? IncomefromBusinessProf { get; set; }
        public int? UnabsorbedDepreciation { get; set; }
        public int? Broughtforwardlosses { get; set; }
        public int? IncomeFromOtherSources { get; set; }
        public int? DeductChapterVIA { get; set; }
        public int? ProfitUS115JB { get; set; }
        public int? AdvanceTax1installment { get; set; }
        public int? AdvanceTax2installment { get; set; }
        public int? AdvanceTax3installment { get; set; }
        public int? AdvanceTax4installment { get; set; }
        public int? TDS { get; set; }
        public int? TCSPaidbyCompany { get; set; }
        public bool? SelfAssessmentTax { get; set; }
        public int? MATCredit { get; set; }
        public int? InterestUS234A { get; set; }
        public int? InterestUS234B { get; set; }
        public int? InterestUS234C { get; set; }
        public int? InterestUS244A { get; set; }
        public int? RefundReceived { get; set; }
        public bool? RevisedReturnFile { get; set; }

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
}
