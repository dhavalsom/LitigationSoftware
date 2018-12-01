using LS.Models;
using LSWebApp.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LSWebApp.Models
{
    public class ITReturnDetailsHeaderModel : ViewModelBase
    {
        #region Properties
        public Company CompanyObject { get; set; }
        public List<FYAY> FYAYList { get; set; }
        public int? FYAYId { get; set; }
        public int? ITSectionCategoryId { get; set; }
        public int? ITSectionId { get; set; }
        public List<ITSectionCategory> ITSectionCategories { get; set; }
        public LitigationDDModel ITSectionListSource { get; set; }
        public List<ITSection> ITSectionList { get; set; }
        #endregion

        #region Constructors
        public ITReturnDetailsHeaderModel() : base(Pages.ITReturnDetailsPage)
        {
        }
        #endregion

        #region Methods
        #endregion
    }

    public class ITReturnDetailsDataModel
    {
        #region Properties
        public ITReturnDetails ITReturnDetailsObject { get; set; }
        public Dictionary<string, ITHeadMaster> ITHeadMasterList { get; set; }
        public Dictionary<string, List<ITReturnDocumentsDisplay>> ITReturnDocumentList { get; set; }
        public Dictionary<string, ITHeadDocumentsUploaderModel> ITHeadDocumentsUploaderModels { get; set; }
        public List<ITReturnDetailsExtension> ExtensionList { get; set; }
        #endregion

        #region Constructors
        public ITReturnDetailsDataModel()
        {
            ITReturnDetailsObject = new ITReturnDetails();
            ExtensionList = new List<ITReturnDetailsExtension>();
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