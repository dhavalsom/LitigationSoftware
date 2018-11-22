using LS.Models;
using LSWebApp.Infrastructure;

namespace LSWebApp.Models
{
    public class FlowChartModel : ViewModelBase
    {
        public Company CompanyObject { get; set; }
        public FlowChartModel() : base(Pages.ITReturnDetailsPage)
        {
        }
    }
}