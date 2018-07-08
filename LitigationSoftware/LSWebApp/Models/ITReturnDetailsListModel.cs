using LS.Models;
using System.Collections.Generic;

namespace LSWebApp.Models
{
    public class ITReturnDetailsListModel
    {
        #region Properties

        public List<ITReturnDetails> ITReturnDetailsListObject { get; set; }
        public int? CompanyId { get; set; }
        public int? FYAYId { get; set; }
        #endregion

        #region Constructors
        public ITReturnDetailsListModel()
        {
            ITReturnDetailsListObject = new List<ITReturnDetails>();
        }
        #endregion
    }
}