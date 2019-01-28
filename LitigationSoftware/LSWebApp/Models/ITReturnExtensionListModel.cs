using LS.Models;
using System.Collections.Generic;

namespace LSWebApp.Models
{
    public class ITReturnExtensionListModel
    {
        public List<ITReturnDetailsExtension> ITReturnDetailsExtensionList { get; set; }
        public ITHeadMaster ITHeadObject { get; set; }
        public ITReturnDetailsExtension ObjITReturnDetailsExtension { get; set; }
        public ITReturnDetails ObjITReturnDetails { get; set; }
        public List<ITSubHeadMaster> ITSubHeadList { get; set; }

        public ITReturnExtensionListModel(List<ITReturnDetailsExtension> itReturnDetailsExtensionList
            , ITHeadMaster itHeadObject, ITReturnDetails objITReturnDetails
            , List<ITSubHeadMaster> itSubHeadList)
        {
            ObjITReturnDetailsExtension = new ITReturnDetailsExtension();
            ObjITReturnDetailsExtension.ITHeadId = itHeadObject.Id;
            ObjITReturnDetailsExtension.ITReturnDetailsId = objITReturnDetails.Id;
            this.ITReturnDetailsExtensionList = itReturnDetailsExtensionList;
            this.ITHeadObject = itHeadObject;
            this.ObjITReturnDetails = objITReturnDetails;
            this.ITSubHeadList = itSubHeadList;
        }

        public ITReturnExtensionListModel()
        {

        }
    }
}