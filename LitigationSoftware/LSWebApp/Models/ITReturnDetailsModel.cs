using LS.Models;
using LSWebApp.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LSWebApp.Models
{
    public class ITReturnDetailsModel : ViewModelBase
    {
        #region Properties
        public ITReturnDetails ITReturnDetailsObject { get; set; }
        public List<FYAY> FYAYList { get; set; }
        public Dictionary<string, ITHeadMaster> ITHeadMasterList { get; set; }
        public Dictionary<string, List<ITReturnDocumentsDisplay>> ITReturnDocumentList { get; set; }
        public Dictionary<string, ITHeadDocumentsUploaderModel> ITHeadDocumentsUploaderModels { get; set; }
        public List<ITSectionCategory> ITSectionCategoryList { get; set; }
        public List<ITSection> ITSectionList { get; set; }
        public List<ITReturnDetailsExtension> ExtensionList { get; set; }
        public LitigationDDModel ITSectionListSource { get; set; }
        public List<SelectListItem> ITSectionCategorySelectItems { get; set; }
        #endregion

        #region Constructors
        public ITReturnDetailsModel() : base(Pages.ITReturnDetailsPage)
        {
            ITReturnDetailsObject = new ITReturnDetails();
            ExtensionList = new List<ITReturnDetailsExtension>();
        }
        #endregion

        #region Methods
        public void PopulateITHeadMasters(List<ITHeadMaster> headList, List<ITSubHeadMaster> subHeadList,int? itreturnId)
        {
            this.ITHeadMasterList = new Dictionary<string, ITHeadMaster>();
            foreach (var item in headList)
            {
                item.SubHeadList = subHeadList.Where(sh => sh.ITHeadId == item.Id).ToList<ITSubHeadMaster>();
                
                if(!this.ITHeadMasterList.ContainsKey(item.PropertyName))
                {
                    this.ITHeadMasterList.Add(item.PropertyName, item);
                }
                else
                {
                    var item2 = item;
                }
                foreach (var subItem in item.SubHeadList)
                {
                    ExtensionList.Add(new ITReturnDetailsExtension
                    {
                        ITSubHeadId = subItem.Id,
                        SubHeadMasterObject = subItem,
                        HeadMasterObject = item,
                        ITReturnDetailsId= itreturnId.HasValue ? itreturnId.Value : 0
                    });
                }
            }
        }
        #endregion
    }
}