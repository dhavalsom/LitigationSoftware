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
        public bool? SelfassessmentTax { get; set; }
        public int? MATCredit { get; set; }
        public int? InterestUS234A { get; set; }
        public int? InterestUS234B { get; set; }
        public int? InterestUS234C { get; set; }
        public int? InterestUS244A { get; set; }
        public int? RefundReceived { get; set; }
        public bool? RevisedReturnFile { get; set; }
    }
}
