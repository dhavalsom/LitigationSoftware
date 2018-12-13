using System.Web;
using System.Collections.Generic;
using LS.Models;

namespace LSWebApp.Models
{
    public class ITHeadSpecialIncomeDetailsModel
    {
        public List<SPIncomeDetails> SPIncomeDetailList { get; set; }
        public ITHeadMaster ITHeadObject { get; set; }
        public ITReturnDetails ObjITReturnDetails { get; set; }

        public ITHeadSpecialIncomeDetailsModel(ITHeadMaster itHeadObject
            , ITReturnDetails objITReturnDetails)
        {
            this.ITHeadObject = itHeadObject;
            this.ObjITReturnDetails = objITReturnDetails;
        }

        public ITHeadSpecialIncomeDetailsModel()
        {

        }
    }
}