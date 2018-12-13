using LS.Models;
using System.Collections.Generic;

namespace LSWebApp.Models
{
    public class ITHeadSpecialIncomeModel
    {
        public List<SPIncomeDetails> SPIncomeDetailsList { get; set; }
        public ITHeadMaster ITHeadObject { get; set; }
        public SPIncomeDetails ObjSPIncomeDetails { get; set; }
        public ITReturnDetails ObjITReturnDetails { get; set; }

        public ITHeadSpecialIncomeModel(List<SPIncomeDetails> spIncomeDetailsList
            , ITHeadMaster itHeadObject, ITReturnDetails objITReturnDetails)
        {
            ObjSPIncomeDetails = new SPIncomeDetails();
            ObjSPIncomeDetails.ITHeadId = itHeadObject.Id;
            ObjSPIncomeDetails.ITReturnDetailsId = objITReturnDetails.Id;
            this.SPIncomeDetailsList = spIncomeDetailsList;
            this.ITHeadObject = itHeadObject;
            this.ObjITReturnDetails = objITReturnDetails;
        }

        public ITHeadSpecialIncomeModel()
        {

        }
    }
}