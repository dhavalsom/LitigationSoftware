﻿namespace LS.Models
{
    public class ITSubHeadMaster : BaseEntity
    {
        public int ITHeadId { get; set; }
        public string Description { get; set; }
        public bool? IsAllowance { get; set; }
        public bool HasDate { get; set; }

        #region Display Properties
        public string ITHeadName { get; set; }
        #endregion
    }

    public class ITSubHeadMasterResponse
    {
        public int Id { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
