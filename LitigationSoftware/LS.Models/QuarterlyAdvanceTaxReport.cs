using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.Models
{
	public class QuarterlyAdvanceTaxReport
	{
		public int FYAYId { get; set; }
		public string FinancialYear { get; set; }
		public string Quarter { get; set; }
		public decimal AdvanceTax { get; set; }
	}

	public class QuarterlyAdvanceTaxReportResponse
	{
		public bool IsSuccess { get; set; }
		public string Message { get; set; }
		public List<QuarterlyAdvanceTaxReport> AdvanceTaxes { get; set; }
		public QuarterlyAdvanceTaxReportResponse()
		{
			this.AdvanceTaxes = new List<QuarterlyAdvanceTaxReport>();
		}
	}
}
