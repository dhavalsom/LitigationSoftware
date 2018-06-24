using LS.Models;
using System.Collections.Generic;

namespace LSWebApp.Models
{
    public class ITReturnDetailsModel
    {
        public ITReturnDetails ITReturnDetailsObject { get; set; }
        public List<FYAY> FYAYList { get; set; }
        public List<ITSection> ITSectionList { get; set; }
        public LitigationDDModel ITSectionListSource { get; set; }
        public ITReturnDetailsModel()
        {
            ITReturnDetailsObject = new ITReturnDetails();
        }
    }

    //public class ITReturnDetailsResponse
    //{
    //    public string Message { get; set; }
    //    public bool IsSuccess { get; set; }
    //    public ITReturnDetails ITReturnDetailsObject { get; set; }
    //    public ITReturnDetailsResponse()
    //    {
    //        ITReturnDetailsObject = new ITReturnDetails();
    //    }
    //}

  
}