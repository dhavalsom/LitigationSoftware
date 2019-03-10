using LS.Models;
using System.Collections.Generic;

namespace LSWebApp.Models
{
    public class RefundDetailsListModel
    {
        public List<RefundDetails> RefundDetailsList { get; set; }
        public ITHeadMaster ITHeadObject { get; set; }
        public RefundDetails ObjRefundDetails { get; set; }
        public ITReturnDetails ObjITReturnDetails { get; set; }
        public List<FYAY> FYAYList { get; set; }

        public RefundDetailsListModel(List<RefundDetails> refundDetails
            , ITHeadMaster itHeadObject
            , ITReturnDetails objITReturnDetails
            , List<FYAY> fyayList)
        {
            ObjRefundDetails = new RefundDetails();
            ObjRefundDetails.ITHeadMasterID = itHeadObject.Id;
            ObjRefundDetails.ITReturnDetailsID = objITReturnDetails.Id;
            this.RefundDetailsList = refundDetails;
            this.ITHeadObject = itHeadObject;
            this.ObjITReturnDetails = objITReturnDetails;
            this.FYAYList = fyayList;
        }

        public RefundDetailsListModel()
        {

        }
    }
}