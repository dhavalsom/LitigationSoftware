using LS.Models;
using LSWebApp.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace LSWebApp.Models
{
    public class LitigationAndSimulationModel : ViewModelBase
    {
        #region Properties
        public Dictionary<string, ITHeadMaster> ITHeadMasterList { get; set; }
        public Company CompanyObject { get; set; }
        public List<ITReturnDetails> ITReturnDetailsListObject { get; set; }
        public List<ITReturnDetailsExtension> ITReturnDetailExtensions { get; set; }
        public List<LAndSComments> LAndSCommentList { get; set; }
        public List<string> ItemsWithAmounts { get; set; }
        #endregion

        #region Constructors
        public LitigationAndSimulationModel() : base(Pages.ITReturnDetailsPage)
        {
            ITReturnDetailsListObject = new List<ITReturnDetails>();
            ITReturnDetailExtensions = new List<ITReturnDetailsExtension>();
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
                "SelfAssessmentTax",
                "MATCredit",
                "InterestUS234A",
                "InterestUS234B",
                "InterestUS234C"
            };
        }
        #endregion

        #region Methods
        public void PopulateITHeadMasters(List<ITHeadMaster> headList
            , List<ITSubHeadMaster> subHeadList)
        {
            this.ITHeadMasterList = new Dictionary<string, ITHeadMaster>();
            foreach (var item in headList)
            {
                item.SubHeadList = subHeadList.Where(sh => sh.ITHeadId == item.Id).ToList<ITSubHeadMaster>();
                
                if(!this.ITHeadMasterList.ContainsKey(item.PropertyName))
                {
                    this.ITHeadMasterList.Add(item.PropertyName, item);
                }
            }
        }
        #endregion
    }
}