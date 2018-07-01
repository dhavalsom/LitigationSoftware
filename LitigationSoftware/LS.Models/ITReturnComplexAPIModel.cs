using LS.Models;
using System.Collections.Generic;

namespace LS.Models
{
    public class ITReturnComplexAPIModel
    {
        public ITReturnDetails ITReturnDetailsObject { get; set; }
        public List<ITReturnDetailsExtension> ExtensionList { get; set; }
        public ITReturnComplexAPIModel()
        {
            ITReturnDetailsObject = new ITReturnDetails();
            ExtensionList = new List<ITReturnDetailsExtension>();
        }
    }

    public class ITReturnComplexAPIModelResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public ITReturnDetails ITReturnDetailsObject { get; set; }
        public List<ITReturnDetailsExtension> ExtensionList { get; set; }
        public ITReturnComplexAPIModelResponse()
        {
            ITReturnDetailsObject = new ITReturnDetails();
            ExtensionList = new List<ITReturnDetailsExtension>();
        }
    }
}
