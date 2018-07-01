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
}