using LS.Models;
using LSWebApp.Infrastructure;

namespace LSWebApp.Models
{
	public class ChartModel : ViewModelBase
	{
		public Company CompanyObject { get; set; }
		public int ChartId { get; set; }
		public ChartModel() : base(Pages.ITReturnDetailsPage)
        {
		}
	}
}