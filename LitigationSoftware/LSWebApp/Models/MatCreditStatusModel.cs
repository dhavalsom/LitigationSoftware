using LS.Models;
using LSWebApp.Infrastructure;

namespace LSWebApp.Models
{
    public class MatCreditStatusModel : ViewModelBase
    {
        public Company CompanyObject { get; set; }
        public MatCreditStatusModel() : base(Pages.ITReturnDetailsPage)
        {
        }
    }
}