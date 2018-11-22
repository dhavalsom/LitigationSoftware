using LS.Models;
using LSWebApp.Infrastructure;

namespace LSWebApp.Models
{
    public class BusinessLossAnalysisModel : ViewModelBase
    {
        public Company CompanyObject { get; set; }
        public BusinessLossAnalysisModel() : base(Pages.ITReturnDetailsPage)
        {
        }
    }
}