using System.Collections.Generic;

namespace LS.Models
{
    public class ITHeadMaster : BaseEntity
    {
        public string ExcelSrNo { get; set; }
        public string Description { get; set; }
        public string PropertyName { get; set; }
        public bool CanAddSubHead { get; set; }
        public bool CanAddDocuments { get; set; }
        public bool IsROI { get; set; }
        public bool HasDate { get; set; }       

        #region Display Properties
        public List<ITSubHeadMaster> SubHeadList { get; set; }
        #endregion
    }
}
