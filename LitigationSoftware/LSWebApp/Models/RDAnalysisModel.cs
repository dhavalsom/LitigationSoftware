using LS.Models;
using LSWebApp.Infrastructure;

namespace LSWebApp.Models
{
    public class RDAnalysisModel : ViewModelBase
    {
        public Company CompanyObject { get; set; }
        public RDAnalysisModel() : base(Pages.ITReturnDetailsPage)
        {
        }
    }
}