using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.Models
{
	public class TaxLiabilityReport
	{
		public int FYAYId { get; set; }
		public string FinancialYear { get; set; }
		public string TaxTypeName { get; set; }
		public decimal Tax { get; set; }
	}

	public class TaxLiabilityReportResponse
	{
		public bool IsSuccess { get; set; }
		public string Message { get; set; }
		public List<TaxLiabilityReport> Taxes { get; set; }
		public TaxLiabilityReportResponse()
		{
			this.Taxes = new List<TaxLiabilityReport>();
		}
	}
}
