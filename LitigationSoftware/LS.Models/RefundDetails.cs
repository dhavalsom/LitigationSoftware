using System;
using System.Collections.Generic;

namespace LS.Models
{
    public class RefundDetails : BaseEntity
    {
        #region Properties
        public int ITReturnDetailsID { get; set; }
        public int FYAYID { get; set; }
        public int ITHeadMasterID { get; set; }
        public DateTime? RefDate { get; set; }
        public decimal RefAmount { get; set; }
        #endregion

        #region Serialization
        //public bool ShouldSerializeRefDate()
        //{
        //    return RefDate.HasValue;
        //}

        //public bool ShouldSerializeRefAmount()
        //{
        //    return RefAmount.HasValue;
        //}
        #endregion
    }

    public class RefundDetailsResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public RefundDetails RefundDetailsObject { get; set; }
        public RefundDetailsResponse()
        {
            RefundDetailsObject = new RefundDetails();
        }
    }

    public class RefundDetailsListResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public List<RefundDetails> RefundDetailsList { get; set; }
        public RefundDetailsListResponse()
        {
            RefundDetailsList = new List<RefundDetails>();
        }
    }
}
