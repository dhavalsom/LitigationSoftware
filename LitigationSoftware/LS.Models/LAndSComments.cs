using System.Collections.Generic;

namespace LS.Models
{
    public class LAndSComments : BaseEntity
    {
        public int CompanyId { get; set; }
        public int ITSubHeadId { get; set; }
        public string Comment { get; set; }

        #region Display Properties
        public string ITSubHeadDescription { get; set; }
        public string CompanyName { get; set; }
        #endregion
    }

    public class LAndSCommentsResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<LAndSComments> LAndSCommentsList { get; set; }
    }
}
