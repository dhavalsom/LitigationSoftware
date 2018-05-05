using LS.Models;
using System.Collections.Generic;

namespace LSWebApp.Models
{
    public class ITReturnDetailsModel
    {
        public ITReturnDetails ITReturnDetailsObject { get; set; }
        public List<FYAY> FYAYList { get; set; }
        public List<ITSection> ITSectionList { get; set; }
        public ITReturnDetailsModel()
        {
            ITReturnDetailsObject = new ITReturnDetails();
        }
    }
}