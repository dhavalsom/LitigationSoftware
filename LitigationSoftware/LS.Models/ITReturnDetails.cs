﻿using System;
using System.Collections.Generic;
using System.Linq;

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
        public int ITSectionCategoryID { get; set; }
        public string ITSectionCategoryDesc { get; set; }
        public int ITSectionID { get; set; }
        public string ITSectionDescription { get; set; }
        public DateTime? ITReturnFillingDate { get; set; }
        public DateTime? ITReturnDueDate { get; set; }
        public decimal? IncomefromSalary { get; set; }
        public decimal? HousePropIncome { get; set; }
        public decimal? IncomefromCapGainsLTCG { get; set; }
        public decimal? IncomefromCapGainsSTCG { get; set; }
        public decimal? IncomefromBusinessProf { get; set; }
        public decimal? IncomefromSpeculativeBusiness { get; set; }
        public bool? Broughtforwardlosses { get; set; }
        public decimal? IncomeFromOtherSources { get; set; }
        public decimal? DeductChapterVIA { get; set; }
        public decimal? ProfitUS115JB { get; set; }
        public decimal? AdvanceTax1installment { get; set; }
        public decimal? AdvanceTax2installment { get; set; }
        public decimal? AdvanceTax3installment { get; set; }
        public decimal? AdvanceTax4installment { get; set; }
        public decimal? TDS { get; set; }
        public decimal? TDS26AS { get; set; }
        public decimal? TDSasperBooks { get; set; }
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
        public List<ITReturnDetailsExtension> Extensions { get; set; }
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

        public bool ShouldSerializeIncomefromCapGainsLTCG()
        {
            return IncomefromCapGainsLTCG.HasValue;
        }

        public bool ShouldSerializeIncomefromCapGainsSTCG()
        {
            return IncomefromCapGainsSTCG.HasValue;
        }

        public bool ShouldSerializeIncomefromBusinessProf()
        {
            return IncomefromBusinessProf.HasValue;
        }

        public bool ShouldSerializeIncomefromSpeculativeBusiness()
        {
            return IncomefromSpeculativeBusiness.HasValue;
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

        public bool ShouldSerializeIncomefromSalary()
        {
            return IncomefromSalary.HasValue;
        }

        public bool ShouldSerializeTDS26AS()
        {
            return TDS26AS.HasValue;
        }

        public bool ShouldSerializeTDSasperBooks()
        {
            return TDSasperBooks.HasValue;
        }

        #endregion

        #region Methods
        public decimal? GetTotalComputedValue(ITHeadMaster itHeadMaster)
        {
            decimal? result = 0;
            switch(itHeadMaster.PropertyName)
            {
                case "IncomefromSalary":
                    result += this.IncomefromSalary.HasValue ? this.IncomefromSalary.Value : 0;
                    break;
                case "HousePropIncome":
                    result += this.HousePropIncome.HasValue ? this.HousePropIncome.Value : 0;
                    break;
                case "IncomefromCapGainsLTCG":
                    result += this.IncomefromCapGainsLTCG.HasValue ? this.IncomefromCapGainsLTCG.Value : 0;
                    break;
                case "IncomefromCapGainsSTCG":
                    result += this.IncomefromCapGainsSTCG.HasValue ? this.IncomefromCapGainsSTCG.Value : 0;
                    break;
                case "IncomefromBusinessProf":
                    result += this.IncomefromBusinessProf.HasValue ? this.IncomefromBusinessProf.Value : 0;
                    break;
                case "IncomefromSpeculativeBusiness":
                    result += this.IncomefromSpeculativeBusiness.HasValue ? this.IncomefromSpeculativeBusiness.Value : 0;
                    break;
                case "IncomeFromOtherSources":
                    result += this.IncomeFromOtherSources.HasValue ? this.IncomeFromOtherSources.Value : 0;
                    break;
                case "DeductChapterVIA":
                    result += this.DeductChapterVIA.HasValue ? this.DeductChapterVIA.Value : 0;
                    break;
                case "ProfitUS115JB":
                    result += this.ProfitUS115JB.HasValue ? this.ProfitUS115JB.Value : 0;
                    break;
                case "AdvanceTax1installment":
                    result += this.AdvanceTax1installment.HasValue ? this.AdvanceTax1installment.Value : 0;
                    break;
                case "AdvanceTax2installment":
                    result += this.AdvanceTax2installment.HasValue ? this.AdvanceTax2installment.Value : 0;
                    break;
                case "AdvanceTax3installment":
                    result += this.AdvanceTax3installment.HasValue ? this.AdvanceTax3installment.Value : 0;
                    break;
                case "AdvanceTax4installment":
                    result += this.AdvanceTax4installment.HasValue ? this.AdvanceTax4installment.Value : 0;
                    break;
                case "TDS":
                    result += this.TDS.HasValue ? this.TDS.Value : 0;
                    break;
                case "TDS26AS":
                    result += this.TDS26AS.HasValue ? this.TDS26AS.Value : 0;
                    break;
                case "TDSasperBooks":
                    result += this.TDSasperBooks.HasValue ? this.TDSasperBooks.Value : 0;
                    break;
                case "TCSPaidbyCompany":
                    result += this.TCSPaidbyCompany.HasValue ? this.TCSPaidbyCompany.Value : 0;
                    break;
                case "SelfAssessmentTax":
                    result += this.SelfAssessmentTax.HasValue ? this.SelfAssessmentTax.Value : 0;
                    break;
                case "MATCredit":
                    result += this.MATCredit.HasValue ? this.MATCredit.Value : 0;
                    break;
                case "InterestUS234A":
                    result += this.InterestUS234A.HasValue ? this.InterestUS234A.Value : 0;
                    break;
                case "InterestUS234B":
                    result += this.InterestUS234B.HasValue ? this.InterestUS234B.Value : 0;
                    break;
                case "InterestUS234C":
                    result += this.InterestUS234C.HasValue ? this.InterestUS234C.Value : 0;
                    break;
                default:
                    break; 
            }
            result += this.Extensions
                    .Where(e => e.ITHeadId == itHeadMaster.Id && !e.IsAllowance)
                    .Sum(e => e.ITSubHeadValue)
                - this.Extensions
                    .Where(e => e.ITHeadId == itHeadMaster.Id && e.IsAllowance)
                    .Sum(e => e.ITSubHeadValue);
            return result;
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
