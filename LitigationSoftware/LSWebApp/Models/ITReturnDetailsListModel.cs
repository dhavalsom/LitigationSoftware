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
        #endregion

        #region Constructors
        public ITReturnDetailsListModel()
        {
            ITReturnDetailsListObject = new List<ITReturnDetails>();
        }
        #endregion

        #region Methods
        public void PopulateITHeadMasters(List<ITHeadMaster> headList, List<ITSubHeadMaster> subHeadList)
        {
            this.ITHeadMasterList = new Dictionary<string, ITHeadMaster>();
            foreach (var item in headList)
            {
                item.SubHeadList = subHeadList.Where(sh => sh.ITHeadId == item.Id).ToList<ITSubHeadMaster>();

                if (!this.ITHeadMasterList.ContainsKey(item.PropertyName))
                {
                    this.ITHeadMasterList.Add(item.PropertyName, item);
                }
                else
                {
                    var item2 = item;
                }
            }
        }
        #endregion
    }
}