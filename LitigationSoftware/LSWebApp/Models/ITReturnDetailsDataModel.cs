using LS.Models;
using System.Collections.Generic;
using System.Linq;

namespace LSWebApp.Models
{

    public class ITReturnDetailsDataModel
    {
        #region Properties
        public ITReturnDetails ITReturnDetailsObject { get; set; }
        public Dictionary<string, ITHeadMaster> ITHeadMasterList { get; set; }
        public Dictionary<string, List<ITReturnDocumentsDisplay>> ITReturnDocumentList { get; set; }
        public Dictionary<string, ITHeadDocumentsUploaderModel> ITHeadDocumentsUploaderModels { get; set; }
        public List<ITReturnDetailsExtension> ExtensionList { get; set; }
        public List<string> ItemsWithAmounts { get; set; }
        #endregion

        #region Constructors
        public ITReturnDetailsDataModel()
        {
            ITReturnDetailsObject = new ITReturnDetails();
            ExtensionList = new List<ITReturnDetailsExtension>();
            ItemsWithAmounts = new List<string>
            {
                "IncomefromSalary",
                "HousePropIncome",
                "IncomefromCapGainsLTCG",
                "IncomefromCapGainsSTCG",
                "IncomefromBusinessProf",
                "IncomefromSpeculativeBusiness",
                "IncomeFromOtherSources",
                "DeductChapterVIA",
                "ProfitUS115JB",
                "AdvanceTax1installment",
                "AdvanceTax2installment",
                "AdvanceTax3installment",
                "AdvanceTax4installment",
                "TDS",
                "TDS26AS",
                "TDSasperBooks",
                "TCSPaidbyCompany",
                "TaxCollectedAtSource",
                "SelfAssessmentTax",
                "ForeignTaxCredit",
                "MATCredit",
                "InterestUS234A",
                "InterestUS234B",
                "InterestUS234C",
                "InterestUS234D",
                "InterestUS220",
                "RefundAdjusted",
                "RegularAssessment"
            };
        }
        #endregion

        #region Methods
        public void PopulateITHeadMasters(List<ITHeadMaster> headList, List<ITSubHeadMaster> subHeadList, int? itreturnId)
        {
            this.ITHeadMasterList = new Dictionary<string, ITHeadMaster>();
            foreach (var item in headList)
            {
                item.SubHeadList = subHeadList.Where(sh => sh.ITHeadId == item.Id)
                    .ToList<ITSubHeadMaster>();

                if (!this.ITHeadMasterList.ContainsKey(item.PropertyName))
                {
                    this.ITHeadMasterList.Add(item.PropertyName, item);
                }
                foreach (var subItem in item.SubHeadList)
                {
                    ExtensionList.Add(new ITReturnDetailsExtension
                    {
                        ITSubHeadId = subItem.Id,
                        SubHeadMasterObject = subItem,
                        HeadMasterObject = item,
                        ITReturnDetailsId = itreturnId.HasValue ? itreturnId.Value : 0
                    });
                }
            }
        }
        #endregion
    }
}