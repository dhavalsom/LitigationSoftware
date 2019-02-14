using LS.Models;
using System.Collections.Generic;
using System.Linq;

namespace LSWebApp.Models
{
    public class ITReturnDetailsListModel
    {
        #region Properties
        public Dictionary<string, ITHeadMaster> ITHeadMasterList { get; set; }
        public List<ITReturnDetails> ITReturnDetailsListObject { get; set; }
        public int? CompanyId { get; set; }
        public int? FYAYId { get; set; }
        public int? Id { get; set; }
        public List<string> ItemsWithAmounts_ROI { get; set; }
        public List<string> ItemsWithAmounts_TaxCalc { get; set; }
        public List<SPIncomeDetails> SelfAssessmentList { get; set; }
        public List<SPIncomeDetails> RegularAssessmentList { get; set; }
        public List<BusinessLossDetails> BusinessLossDetailsList { get; set; }
        #endregion

        #region Constructors
        public ITReturnDetailsListModel()
        {
            ITReturnDetailsListObject = new List<ITReturnDetails>();
            SelfAssessmentList = new List<SPIncomeDetails>();
            RegularAssessmentList = new List<SPIncomeDetails>();
            ItemsWithAmounts_ROI = new List<string>
            {
                "IncomefromSalary",
                "HousePropIncome",
                "IncomefromCapGainsLTCG",
                "IncomefromCapGainsSTCG",
                "IncomefromBusinessProf",
                "IncomefromSpeculativeBusiness",
                "IncomeFromOtherSources",
                "DeductChapterVIA",
                "ProfitUS115JB"                
            };

            ItemsWithAmounts_TaxCalc = new List<string>
            {
                "AdvanceTax1installment",
                "AdvanceTax2installment",
                "AdvanceTax3installment",
                "AdvanceTax4installment",
                "TDS",
                "TDS26AS",
                "TDSasperBooks",
                "TCSPaidbyCompany",
                "SelfAssessmentTax",
                "MATCredit",
                "InterestUS234A",
                "InterestUS234B",
                "InterestUS234C"
            };
            BusinessLossDetailsList = new List<BusinessLossDetails>();
        }
        #endregion

        #region Methods
        public void PopulateITHeadMasters(List<ITHeadMaster> headList, List<ITSubHeadMaster> subHeadList)
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
            }
        }
        #endregion
    }

    public class ITReturnDetailsListItemModel
    {
        public ITHeadMaster CurrentItem { get; set; }
        public List<ITReturnDetails> ITReturnDetailsListObject { get; set; }
        public Dictionary<string, ITHeadMaster> ITHeadMasterList { get; set; }
        public List<ITReturnDetailsExtension> ITReturnDetailExtensions { get; set; }
        public List<LAndSComments> LAndSCommentsList { get; set; }
        public List<BusinessLossDetails> BusinessLossDetailsList { get; set; }

        public ITReturnDetailsListItemModel(ITHeadMaster currentItem,
            List<ITReturnDetails> itReturnDetailsListObject,
            Dictionary<string, ITHeadMaster> itHeadMasterList,
            List<BusinessLossDetails> businessLossDetailsList)
        {
            this.CurrentItem = currentItem;
            this.ITReturnDetailsListObject = itReturnDetailsListObject;
            this.ITHeadMasterList = itHeadMasterList;
            this.BusinessLossDetailsList = businessLossDetailsList;
        }

        public ITReturnDetailsListItemModel(ITHeadMaster currentItem,
           List<ITReturnDetails> itReturnDetailsListObject,
           Dictionary<string, ITHeadMaster> itHeadMasterList,
           List<ITReturnDetailsExtension> itReturnDetailExtensions,
           List<LAndSComments> landsCommentsList)
        {
            this.CurrentItem = currentItem;
            this.ITReturnDetailsListObject = itReturnDetailsListObject;
            this.ITHeadMasterList = itHeadMasterList;
            this.ITReturnDetailExtensions = itReturnDetailExtensions;
            this.LAndSCommentsList = landsCommentsList;
        }
    }
}