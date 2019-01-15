using LS.Models;
using LSWebApp.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace LSWebApp.Models
{
    public class BusinessLossAnalysisModel : ViewModelBase
    {
        public Company CompanyObject { get; set; }
        public List<BusinessLossDetails> BusinessLossDetailsList { get; set; }
        public List<FYAY> FYAYList { get; set; }

        public BusinessLossAnalysisModel() : base(Pages.ITReturnDetailsPage)
        {
            BusinessLossDetailsList = new List<BusinessLossDetails>();
            FYAYList = new List<FYAY>();
        }
    }
}