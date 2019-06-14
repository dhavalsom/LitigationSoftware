using LS.Models;
using LSWebApp.Infrastructure;
using System.Collections.Generic;

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

	public class ChartDataViewModel
	{
		public int ChartId { get; set; }
		public IDictionary<string, object> ChartParams { get; set; }
	}

}