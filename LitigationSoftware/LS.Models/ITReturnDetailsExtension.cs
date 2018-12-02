using System;

namespace LS.Models
{
    public class ITReturnDetailsExtension : BaseEntity
    {
        public int ITReturnDetailsId { get; set; }
        public int ITSubHeadId { get; set; }
        public decimal? ITSubHeadValue { get; set; }
        public DateTime? ITSubHeadDate { get; set; } 

        #region Display Properties
        public ITSubHeadMaster SubHeadMasterObject { get; set; }
        public ITHeadMaster HeadMasterObject { get; set; }
        public int ITHeadId { get; set; }
        public bool IsAllowance { get; set; }
        #endregion
    }
}
