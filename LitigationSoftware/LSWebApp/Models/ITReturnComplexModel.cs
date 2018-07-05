using LS.Models;
using System.Collections.Generic;

namespace LSWebApp.Models
{
    public class ITReturnComplexModel
    {
        public ITReturnDetails ITReturnDetailsObject { get; set; }
        public List<ITReturnDetailsExtension> ExtensionList { get; set; }
        public ITReturnComplexModel()
        {
            ITReturnDetailsObject = new ITReturnDetails();
            ExtensionList = new List<ITReturnDetailsExtension>();
        }
    }
}