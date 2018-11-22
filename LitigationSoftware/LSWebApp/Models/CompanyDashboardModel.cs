using LS.Models;
using LSWebApp.Infrastructure;

namespace LSWebApp.Models
{
    public class CompanyDashboardModel : ViewModelBase
    {
        public Company CompanyObject { get; set; }
        public CompanyDashboardModel() : base(Pages.ITReturnDetailsPage)
        {
        }
    }
}