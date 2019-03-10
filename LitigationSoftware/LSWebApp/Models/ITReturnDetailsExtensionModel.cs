using LS.Models;
using System.Collections.Generic;
using System.Linq;

namespace LSWebApp.Models
{
    public class ITReturnDetailsExtensionModel
    {
        public int ITHeadId { get; set; }
        public List<ITReturnDetailsExtension> ExtensionList { get; set; }
        public List<ITReturnDetailsExtension> AllowancesExtensionList
        {
            get
            {
                return ExtensionList.Where(l => l.SubHeadMasterObject.IsAllowance.HasValue
                && l.SubHeadMasterObject.IsAllowance.Value).ToList<ITReturnDetailsExtension>();
            }
        }

        public List<ITReturnDetailsExtension> DisAllowancesExtensionList
        {
            get
            {
                return ExtensionList.Where(l => l.SubHeadMasterObject.IsAllowance.HasValue
                && !(l.SubHeadMasterObject.IsAllowance.Value)).ToList<ITReturnDetailsExtension>();
            }
        }

        public ITReturnDetailsExtensionModel(List<ITReturnDetailsExtension> extensionList, int itHeadId)
        {
            this.ExtensionList = extensionList;
            this.ITHeadId = itHeadId;
        }
    }

    public class ITHeadDetailsModel
    {
        public string ITHead { get; set; }
        public Dictionary<string, ITHeadMaster> ITHeadMasterList { get; set; }
        public ITReturnDetails ITReturnDetailsObject { get; set; }
        public List<ITReturnDetailsExtension> ExtensionList { get; set; }
        public Dictionary<string, ITHeadDocumentsUploaderModel> ITHeadDocumentsUploaderModels { get; set; }
        public Dictionary<string, ITHeadSpecialIncomeModel> ITHeadSpecialIncomeModels { get; set; }
        public Dictionary<string, ITReturnExtensionListModel> ITReturnExtensionListModels { get; set; }
        public Dictionary<string, RefundDetailsListModel> RefundDetailsListModels { get; set; }

        public ITHeadDetailsModel(string itHead,
            Dictionary<string, ITHeadMaster> itHeadMasterList,
            ITReturnDetails itReturnDetailsObject,
            List<ITReturnDetailsExtension> extensionList,
            Dictionary<string, ITHeadDocumentsUploaderModel> itHeadDocumentsUploaderModels,
            Dictionary<string, ITHeadSpecialIncomeModel> itHeadSpecialIncomeModels,
            Dictionary<string, ITReturnExtensionListModel> itReturnExtensionListModels,
            Dictionary<string, RefundDetailsListModel> refundDetailsListModels)
        {
            this.ITHead = itHead;
            this.ITHeadMasterList = itHeadMasterList;
            this.ITReturnDetailsObject = itReturnDetailsObject;
            this.ExtensionList = extensionList;
            this.ITHeadDocumentsUploaderModels = itHeadDocumentsUploaderModels;
            this.ITHeadSpecialIncomeModels = itHeadSpecialIncomeModels;
            this.ITReturnExtensionListModels = itReturnExtensionListModels;
            this.RefundDetailsListModels = refundDetailsListModels;
        }
    }
}