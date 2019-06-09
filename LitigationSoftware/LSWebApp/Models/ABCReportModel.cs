using LS.Models;
using LSWebApp.Infrastructure;

namespace LSWebApp.Models
{
    public class ABCReportModel : ViewModelBase
    {
        public Company CompanyObject { get; set; }
        public bool? IsAllowance { get; set; }

        public ABCReportModel() : base(Pages.ITReturnDetailsPage)
        {
        }
    }
}