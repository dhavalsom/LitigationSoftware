using LS.Models;
using System.Collections.Generic;
using System.Linq;

namespace LSWebApp.Models
{
    public class ITReturnDetailsModel
    {
        #region Properties
        public ITReturnDetails ITReturnDetailsObject { get; set; }
        public List<FYAY> FYAYList { get; set; }
        public Dictionary<string, ITHeadMaster> ITHeadMasterList { get; set; }
        public List<ITSection> ITSectionList { get; set; }
        public List<ITReturnDetailsExtension> ExtensionList { get; set; }
        public LitigationDDModel ITSectionListSource { get; set; }
        public bool IsReturn { get; set; }
        #endregion

        #region Constructors
        public ITReturnDetailsModel()
        {
            ITReturnDetailsObject = new ITReturnDetails();
            ExtensionList = new List<ITReturnDetailsExtension>();
        }
        #endregion

        #region Methods
        public void PopulateITHeadMasters(List<ITHeadMaster> headList, List<ITSubHeadMaster> subHeadList)
        {
            this.ITHeadMasterList = new Dictionary<string, ITHeadMaster>();
            foreach (var item in headList)
            {
                item.SubHeadList = subHeadList.Where(sh => sh.ITHeadId == item.Id).ToList<ITSubHeadMaster>();
                this.ITHeadMasterList.Add(item.PropertyName, item);
                foreach (var subItem in item.SubHeadList)
                {
                    ExtensionList.Add(new ITReturnDetailsExtension
                    {
                        ITSubHeadId = subItem.Id,
                        SubHeadMasterObject = subItem,
                        HeadMasterObject = item
                    });
                }
            }
        }
        #endregion
    }
}