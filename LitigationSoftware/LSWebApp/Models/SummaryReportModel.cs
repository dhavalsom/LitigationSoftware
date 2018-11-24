using LS.Models;
using LSWebApp.Infrastructure;

namespace LSWebApp.Models
{
    public class SummaryReportModel : ViewModelBase
    {
        public Company CompanyObject { get; set; }
        public SummaryReportModel() : base(Pages.ITReturnDetailsPage)
        {
        }
    }
}