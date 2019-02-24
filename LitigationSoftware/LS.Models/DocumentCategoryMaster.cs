using System.Collections.Generic;

namespace LS.Models
{
    public class DocumentCategoryMaster : BaseEntity
    {
        public string Description { get; set; }

        #region Display Properties
        public List<ITSubHeadMaster> SubHeadList { get; set; }
        #endregion
    }
}
