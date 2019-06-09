using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.Models
{

	public class CompetitorTaxRateReport
	{
		public int CompetitorId { get; set; }
		public string CompetitorName { get; set; }
		public string FinancialYear { get; set; }
		public decimal TaxRate { get; set; }
	}

	public class CompetitorTaxRateReportResponse
	{
		public bool IsSuccess { get; set; }
		public string Message { get; set; }
		public List<CompetitorTaxRateReport> CompetitorTaxRates { get; set; }
		public CompetitorTaxRateReportResponse()
		{
			this.CompetitorTaxRates = new List<CompetitorTaxRateReport>();
		}
	}
	
}
